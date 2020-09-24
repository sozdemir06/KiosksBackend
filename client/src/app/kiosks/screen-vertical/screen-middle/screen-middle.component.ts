import {
  Component,
  OnInit,
  Input,
  ElementRef,
  ViewChildren,
  QueryList,OnDestroy
} from '@angular/core';
import { IKiosks } from '../../models/IKiosks';
import { HelperService } from 'src/app/core/services/helper-service';
import { KiosksStore } from '../../store/kiosks-store';
import { Observable, combineLatest, Subscription, timer} from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { NgbSlideEvent } from '@ng-bootstrap/ng-bootstrap';
import { IKiosksSubScreenData } from 'src/app/shared/models/IKiosksSubScreenData';



@Component({
  selector: 'app-screen-middle',
  templateUrl: './screen-middle.component.html',
  styleUrls: ['./screen-middle.component.scss']
})
export class ScreenMiddleComponent implements OnInit,OnDestroy {
  @Input() kiosks: IKiosks;
  @Input() subscreenid: number;
  @Input() screenHeight: number = 30;
  interval: any = 2500;

  //@ViewChild('ngcarousel') ngCarousel: NgbCarousel;
  @ViewChildren('video') videoInput: QueryList<ElementRef>;
  video:HTMLVideoElement; 
  subscription:Subscription=Subscription.EMPTY;
   
  data$: Observable<IKiosksSubScreenData>;


  constructor(
    public helperService: HelperService,
    private kiosksStore: KiosksStore,
  ) {
     
  }
  ngOnInit(): void {

    this.subscription = timer(1000,1*60*1000).subscribe((thick) => {
      const dateNow = new Date();
      const announces$ = this.kiosksStore.kiosks$.pipe(
        map((announces) =>
          announces.announces.filter((x) =>
            x.announceSubScreens.find(
              (s) =>
                s.subScreenId == this.subscreenid &&
                new Date(x.publishStartDate) <= dateNow &&
                new Date(x.publishFinishDate) >= dateNow
            )
          )
        )
      );
      const vehicleAnnounces$ = this.kiosksStore.kiosks$.pipe(
        map((vehicleannounces) =>
          vehicleannounces.vehicleAnnounces.filter((x) =>
            x.vehicleAnnounceSubScreens.find(
              (s) =>
                s.subScreenId == this.subscreenid &&
                new Date(x.publishStartDate) <= dateNow &&
                new Date(x.publishFinishDate) >= dateNow
            )
          )
        )
      );
      const homeAnnounces$ = this.kiosksStore.kiosks$.pipe(
        map((homeannounces) =>
          homeannounces.homeAnnounces.filter((x) =>
            x.homeAnnounceSubScreens.find(
              (s) =>
                s.subScreenId == this.subscreenid &&
                new Date(x.publishStartDate) <= dateNow &&
                new Date(x.publishFinishDate) >= dateNow
            )
          )
        )
      );
      const news$ = this.kiosksStore.kiosks$.pipe(
        map((news) =>
          news.news.filter((x) =>
            x.newsSubScreens.find(
              (s) =>
                s.subScreenId == this.subscreenid &&
                new Date(x.publishStartDate) <= dateNow &&
                new Date(x.publishFinishDate) >= dateNow
            )
          )
        )
      );
      const foodsMenu$ = this.kiosksStore.kiosks$.pipe(
        map((foodsMenu) =>
          foodsMenu.foodsMenu.filter((x) =>
            x.foodMenuSubScreens.find(
              (s) =>
                s.subScreenId == this.subscreenid &&
                new Date(x.publishStartDate) <= dateNow &&
                new Date(x.publishFinishDate) >= dateNow
            )
          )
        )
      );
      const liveTvBroadCasts$ = this.kiosksStore.kiosks$.pipe(
        map((liveTvBroadCasts) =>
          liveTvBroadCasts.liveTvBroadCasts.filter((x) =>
            x.liveTvBroadCastSubScreens.find(
              (s) =>
                s.subScreenId == this.subscreenid &&
                new Date(x.publishStartDate) <= dateNow &&
                new Date(x.publishFinishDate) >= dateNow
            )
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
        map(
          ([
            announces,
            vehicleAnnounces,
            homeAnnounces,
            news,
            foodsMenu,
            liveTvBroadCasts,
          ]) => {
            return {
              announces,
              vehicleAnnounces,
              homeAnnounces,
              news,
              foodsMenu,
              liveTvBroadCasts,
            };
          }
        )
      );
    });
    
  } //End Of ngOnInit()


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
  }
}
