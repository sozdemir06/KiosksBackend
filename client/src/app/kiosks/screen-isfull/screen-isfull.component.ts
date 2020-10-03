import { Component, OnInit, Input, ViewChildren, QueryList, ElementRef, OnDestroy, ViewChild } from '@angular/core';
import { IKiosksSubScreenData } from 'src/app/shared/models/IKiosksSubScreenData';
import { IKiosks } from '../models/IKiosks';
import { HelperService } from 'src/app/core/services/helper-service';
import { KiosksStore } from '../store/kiosks-store';
import { map } from 'rxjs/operators';
import { combineLatest, Observable, Subscription, timer } from 'rxjs';
import { NgbCarousel, NgbSlideEvent } from '@ng-bootstrap/ng-bootstrap';
import { FoodMenuBgPhotoStore } from 'src/app/core/services/stores/food-menu-bg-photo-store';

@Component({
  selector: 'app-screen-isfull',
  templateUrl: './screen-isfull.component.html',
  styleUrls: ['./screen-isfull.component.scss']
})
export class ScreenIsfullComponent implements OnInit,OnDestroy {
//@Input() kiosks: IKiosks;
@Input() kiosksId:number;
@Input() showNavigationArrows:boolean=false;
@Input() showNavigationIndicators:boolean=false;
interval: any = 2500;

@ViewChild('carousel') carousel: NgbCarousel;
@ViewChildren('video') videoInput: QueryList<ElementRef>;
video:HTMLVideoElement; 
bgPhotoUrl:string;
subscription:Subscription=Subscription.EMPTY;
timerSubs:Subscription=Subscription.EMPTY;

data$: Observable<IKiosksSubScreenData>;

  constructor(
    public helperService: HelperService,
    private kiosksStore: KiosksStore,
    private foodMenuBgPhotoStore:FoodMenuBgPhotoStore
  ) { }
  onEnded(event) {
    const numberSlide = this.carousel.slides.length;
    if(numberSlide==1){
      this.video.play();
    }
  }

  onLoadedloaded(event) {
    const numberSlide = this.carousel?.slides?.length;
    if (numberSlide == 1) {
      this.carousel.slides.forEach((x) => {
        const parseId = x.id.split('_');
        const [
          id,
          index,
          announceType,
          contentType,
          intervalTime,
          fullPath,
        ] = parseId;

        if (contentType.toLowerCase() == 'video') {
          this.video = this.videoInput.find(
            (x) => x.nativeElement.id == index
          ).nativeElement;
          this.video.play();
        }
        this.kiosksStore.checkDateIfExpireAndRemoveFromStore(id, announceType);
      });
    }
  }
  ngOnInit(): void {
    //Bg Photo
    this.subscription=  this.foodMenuBgPhotoStore.bgphotos$.pipe(
      map(photos=>photos.find(x=>x.isSetBackground===true))
    ).subscribe(result=>{
      if(result){
        this.bgPhotoUrl=result?.fullPath;
      }
    })
    //Bg Photo End

    this.timerSubs=timer(1000,1*60*1000).subscribe(thick=>{
      const dateNow=new Date();
    const announces$ = this.kiosksStore.kiosks$.pipe(
      map((announces) =>
        announces.announces.filter((x) =>
          x.announceSubScreens.filter((s) => s.screenId ==this.kiosksId &&
          new Date(x.publishStartDate) <= dateNow &&
          new Date(x.publishFinishDate) >= dateNow)
        )
      )
    )
    
    const vehicleAnnounces$ = this.kiosksStore.kiosks$.pipe(
      map((vehicleannounces) =>
        vehicleannounces.vehicleAnnounces.filter((x) =>
          x.vehicleAnnounceSubScreens.filter(
            (s) => s.screenId == this.kiosksId &&
            new Date(x.publishStartDate) <= dateNow &&
            new Date(x.publishFinishDate) >= dateNow
          )
        )
      )
    );
    const homeAnnounces$ = this.kiosksStore.kiosks$.pipe(
      map((homeannounces) =>
        homeannounces.homeAnnounces.filter((x) =>
          x.homeAnnounceSubScreens.filter(
            (s) => s.screenId == this.kiosksId&&
            new Date(x.publishStartDate) <= dateNow &&
            new Date(x.publishFinishDate) >= dateNow
          )
        )
      )
    );
    const news$ = this.kiosksStore.kiosks$.pipe(
      map((news) =>
        news.news.filter((x) =>
          x.newsSubScreens.filter( (s) => s.screenId == this.kiosksId &&
          new Date(x.publishStartDate) <= dateNow &&
          new Date(x.publishFinishDate) >= dateNow)
        )
      )
    );
    const foodsMenu$ = this.kiosksStore.kiosks$.pipe(
      map((foodsMenu) =>
        foodsMenu.foodsMenu.filter((x) =>
          x.foodMenuSubScreens.filter( (s) => s.screenId == this.kiosksId &&
          new Date(x.publishStartDate) <= dateNow &&
          new Date(x.publishFinishDate) >= dateNow)
        )
      )
    );

    const liveTvBroadCasts$ = this.kiosksStore.kiosks$.pipe(
      map((liveTvBroadCasts) =>
      liveTvBroadCasts.liveTvBroadCasts.filter((x) =>
          x.liveTvBroadCastSubScreens.find( (s) => s.subScreenId == this.kiosksId &&
          new Date(x.publishStartDate) <= dateNow &&
          new Date(x.publishFinishDate) >= dateNow)
        )
      )
    );

    this.data$ = combineLatest([
      announces$,
      vehicleAnnounces$,
      homeAnnounces$,
      news$,
      foodsMenu$,
      liveTvBroadCasts$,
    ]).pipe(
      map(([announces, vehicleAnnounces, homeAnnounces, news, foodsMenu,liveTvBroadCasts]) => {
        return {
          announces,
          vehicleAnnounces,
          homeAnnounces,
          news,
          foodsMenu,
          liveTvBroadCasts
        };
      })
    );
    })
    

  }

  onSlideChange(event: NgbSlideEvent) {
    const parseId = event.current.split('_');
    const [id, index, announceType, contentType, intervalTime,fullPath] = parseId;
    let intervalTimeToMiliSecond = parseInt(intervalTime) * 1000;
    
    if(this.video?.played){
      this.video.pause();
      this.video.load();
    }
    if (contentType.toLowerCase() == 'video') {
       this.video=this.videoInput.find(x=>x.nativeElement.id==index).nativeElement;
       this.video.play();
    }

    this.interval = intervalTimeToMiliSecond;
     //Remove if Publish Date is Expire
     this.kiosksStore.checkDateIfExpireAndRemoveFromStore(id, announceType);


  }

  ngOnDestroy(){
    this.subscription.unsubscribe();
    this.timerSubs.unsubscribe();
  }

}
