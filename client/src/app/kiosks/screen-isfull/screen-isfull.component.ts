import { Component, OnInit, Input, ViewChildren, QueryList, ElementRef, OnDestroy } from '@angular/core';
import { IKiosksSubScreenData } from 'src/app/shared/models/IKiosksSubScreenData';
import { IKiosks } from '../models/IKiosks';
import { HelperService } from 'src/app/core/services/helper-service';
import { KiosksStore } from '../store/kiosks-store';
import { map } from 'rxjs/operators';
import { combineLatest, Observable, Subscription } from 'rxjs';
import { NgbSlideEvent } from '@ng-bootstrap/ng-bootstrap';
import { FoodMenuBgPhotoStore } from 'src/app/core/services/stores/food-menu-bg-photo-store';

@Component({
  selector: 'app-screen-isfull',
  templateUrl: './screen-isfull.component.html',
  styleUrls: ['./screen-isfull.component.scss']
})
export class ScreenIsfullComponent implements OnInit,OnDestroy {
@Input() kiosks: IKiosks;
interval: any = 2500;

//@ViewChild('ngcarousel') ngCarousel: NgbCarousel;
@ViewChildren('video') videoInput: QueryList<ElementRef>;
video:HTMLVideoElement; 
bgPhotoUrl:string;
subscription:Subscription=Subscription.EMPTY;

data$: Observable<IKiosksSubScreenData>;

  constructor(
    public helperService: HelperService,
    private kiosksStore: KiosksStore,
    private foodMenuBgPhotoStore:FoodMenuBgPhotoStore
  ) { }

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

    
    const announces$ = this.kiosksStore.kiosks$.pipe(
      map((announces) =>
        announces.announces.filter((x) =>
          x.announceSubScreens.filter((s) => s.screenId == this.kiosks.screen?.id)
        )
      )
    )
    
    const vehicleAnnounces$ = this.kiosksStore.kiosks$.pipe(
      map((vehicleannounces) =>
        vehicleannounces.vehicleAnnounces.filter((x) =>
          x.vehicleAnnounceSubScreens.filter(
            (x) => x.screenId == this.kiosks.screen?.id
          )
        )
      )
    );
    const homeAnnounces$ = this.kiosksStore.kiosks$.pipe(
      map((homeannounces) =>
        homeannounces.homeAnnounces.filter((x) =>
          x.homeAnnounceSubScreens.filter(
            (x) => x.screenId == this.kiosks.screen?.id
          )
        )
      )
    );
    const news$ = this.kiosksStore.kiosks$.pipe(
      map((news) =>
        news.news.filter((x) =>
          x.newsSubScreens.filter((x) => x.screenId == this.kiosks.screen?.id)
        )
      )
    );
    const foodsMenu$ = this.kiosksStore.kiosks$.pipe(
      map((foodsMenu) =>
        foodsMenu.foodsMenu.filter((x) =>
          x.foodMenuSubScreens.filter((x) => x.screenId == this.kiosks.screen?.id)
        )
      )
    );

    this.data$ = combineLatest([
      announces$,
      vehicleAnnounces$,
      homeAnnounces$,
      news$,
      foodsMenu$,
    ]).pipe(
      map(([announces, vehicleAnnounces, homeAnnounces, news, foodsMenu]) => {
        return {
          announces,
          vehicleAnnounces,
          homeAnnounces,
          news,
          foodsMenu,
        };
      })
    );

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

  }

  ngOnDestroy(){
    this.subscription.unsubscribe();
  }

}
