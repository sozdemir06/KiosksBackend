import { Component, OnInit, Input, ViewChildren, QueryList, ElementRef } from '@angular/core';
import { IKiosks } from '../../models/IKiosks';
import { Observable, combineLatest } from 'rxjs';
import { HelperService } from 'src/app/core/services/helper-service';
import { KiosksStore } from '../../store/kiosks-store';
import { IKiosksSubScreenData } from 'src/app/shared/models/IKiosksSubScreenData';
import { NgbSlideEvent } from '@ng-bootstrap/ng-bootstrap';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-screen-left',
  templateUrl: './screen-left.component.html',
  styleUrls: ['./screen-left.component.scss']
})
export class ScreenLeftComponent implements OnInit {
  @Input() kiosks: IKiosks;
  @Input() subscreenid: number;
  @Input() screenWidth: number = 30;
  interval: any = 2500;

  //@ViewChild('ngcarousel') ngCarousel: NgbCarousel;
  @ViewChildren('video') videoInput: QueryList<ElementRef>;
  video:HTMLVideoElement; 

  data$: Observable<IKiosksSubScreenData>;


  constructor(
    public helperService: HelperService,
    private kiosksStore: KiosksStore,
  ) {
    
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
