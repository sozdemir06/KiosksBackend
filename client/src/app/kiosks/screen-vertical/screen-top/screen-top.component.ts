import {
  Component,
  OnInit,
  Input,
  ElementRef,
  ViewChildren,
  QueryList,
} from '@angular/core';
import { IKiosks } from '../../models/IKiosks';
import { HelperService } from 'src/app/core/services/helper-service';
import { KiosksStore } from '../../store/kiosks-store';
import { Observable, combineLatest} from 'rxjs';
import { map } from 'rxjs/operators';
import { NgbSlideEvent} from '@ng-bootstrap/ng-bootstrap';
import { IKiosksSubScreenData } from 'src/app/shared/models/IKiosksSubScreenData';
import { DomSanitizer } from '@angular/platform-browser';




@Component({
  selector: 'app-screen-top',
  templateUrl: './screen-top.component.html',
  styleUrls: ['./screen-top.component.scss'],
})
export class ScreenTopComponent implements OnInit{
  @Input() kiosks: IKiosks;
  @Input() subscreenid: number;
  @Input() screenHeight: number = 30;
  interval: any = 2500;

  //@ViewChild('ngcarousel') ngCarousel: NgbCarousel;
  @ViewChildren('video') videoInput: QueryList<ElementRef>;
  video:HTMLVideoElement; 

  data$: Observable<IKiosksSubScreenData>;


  constructor(
    public helperService: HelperService,
    private kiosksStore: KiosksStore,
    private sanitizer: DomSanitizer
  ) {

  }

  safeURL(youtubeId:string){
    return this.sanitizer.bypassSecurityTrustResourceUrl (`https://www.youtube.com/embed/${youtubeId}?rel=0`);
  }

  ngOnInit(): void {
    const announces$ = this.kiosksStore.kiosks$.pipe(
      map((announces) =>
        announces.announces.filter((x) =>
          x.announceSubScreens.find((s) => s.subScreenId == this.subscreenid)
        )
      )
    );
    const vehicleAnnounces$ = this.kiosksStore.kiosks$.pipe(
      map((vehicleannounces) =>
        vehicleannounces.vehicleAnnounces.filter((x) =>
          x.vehicleAnnounceSubScreens.find(
            (x) => x.subScreenId == this.subscreenid
          )
        )
      )
    );
    const homeAnnounces$ = this.kiosksStore.kiosks$.pipe(
      map((homeannounces) =>
        homeannounces.homeAnnounces.filter((x) =>
          x.homeAnnounceSubScreens.find(
            (x) => x.subScreenId == this.subscreenid
          )
        )
      )
    );
    const news$ = this.kiosksStore.kiosks$.pipe(
      map((news) =>
        news.news.filter((x) =>
          x.newsSubScreens.find((x) => x.subScreenId == this.subscreenid)
        )
      )
    );
    const foodsMenu$ = this.kiosksStore.kiosks$.pipe(
      map((foodsMenu) =>
        foodsMenu.foodsMenu.filter((x) =>
          x.foodMenuSubScreens.find((x) => x.subScreenId == this.subscreenid)
        )
      )
    );
    const liveTvBroadCasts$ = this.kiosksStore.kiosks$.pipe(
      map((liveTvBroadCasts) =>
      liveTvBroadCasts.liveTvBroadCasts.filter((x) =>
          x.liveTvBroadCastSubScreens.find((x) => x.subScreenId == this.subscreenid)
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

  }
}
