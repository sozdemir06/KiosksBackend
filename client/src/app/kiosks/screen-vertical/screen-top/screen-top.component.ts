import { Component, OnInit, Input, ElementRef, ChangeDetectionStrategy, ChangeDetectorRef, ViewChild } from '@angular/core';
import { IKiosks } from '../../models/IKiosks';
import { SlidesOutputData, CarouselComponent } from 'ngx-owl-carousel-o';
import { HelperService } from 'src/app/core/services/helper-service';
import { KiosksStore } from '../../store/kiosks-store';
import { IAnnounceForKiosks } from '../../models/IAnnounceForKiosks';
import { IVehicleAnnounceForKiosks } from '../../models/IVehicleAnnounceForKiosks';
import { IHomeAnnounceForKiosks } from '../../models/IHomeAnnounceForKiosks';
import { INewsForKiosks } from '../../models/INewsForKiosks';
import { IFoodMenuForKiosks } from '../../models/IFoodMenuForKiosks';
import { Observable, combineLatest } from 'rxjs';
import { map } from 'rxjs/operators';
import { CarouselService } from 'ngx-owl-carousel-o/lib/services/carousel.service';

export interface ITopScreenData {
  announces: IAnnounceForKiosks[];
  vehicleAnnounces: IVehicleAnnounceForKiosks[];
  homeAnnounces: IHomeAnnounceForKiosks[];
  news: INewsForKiosks[];
  foodsMenu: IFoodMenuForKiosks[];
}

@Component({
  selector: 'app-screen-top',
  templateUrl: './screen-top.component.html',
  styleUrls: ['./screen-top.component.scss'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class ScreenTopComponent implements OnInit {
  @Input() kiosks: IKiosks;
  @Input() subscreenid: number;
  @Input() screenHeight: number = 30;

  data$: Observable<ITopScreenData>;
  @ViewChild('owlElement') owlElement: CarouselComponent

  constructor(
    public helperService: HelperService,
    private kiosksStore: KiosksStore,
    private elRef:ElementRef,
    private cd:ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    const announces$ = this.kiosksStore.kiosks$.pipe(
      map((announces) =>
        announces.announces.filter((x) =>
          x.announceSubScreens.filter((s) => s.subScreenId == this.subscreenid)
        )
      )
    );
    const vehicleAnnounces$ = this.kiosksStore.kiosks$.pipe(
      map((vehicleannounces) =>
        vehicleannounces.vehicleAnnounces.filter((x) =>
          x.vehicleAnnounceSubScreens.filter(
            (x) => x.subScreenId == this.subscreenid
          )
        )
      )
    );
    const homeAnnounces$ = this.kiosksStore.kiosks$.pipe(
      map((homeannounces) =>
      homeannounces.homeAnnounces.filter((x) =>
          x.homeAnnounceSubScreens.filter(
            (x) => x.subScreenId == this.subscreenid
          )
        )
      )
    );
    const news$ = this.kiosksStore.kiosks$.pipe(
      map((news) =>
      news.news.filter((x) =>
          x.newsSubScreens.filter(
            (x) => x.subScreenId == this.subscreenid
          )
        )
      )
    );
    const foodsMenu$ = this.kiosksStore.kiosks$.pipe(
      map((foodsMenu) =>
      foodsMenu.foodsMenu.filter((x) =>
          x.foodMenuSubScreens.filter(
            (x) => x.subScreenId == this.subscreenid
          )
        )
      )
    );

    this.data$=combineLatest([announces$,vehicleAnnounces$,homeAnnounces$,news$,foodsMenu$])
                .pipe(
                  map(([announces,vehicleAnnounces,homeAnnounces,news,foodsMenu])=>{
                    return{
                      announces,
                      vehicleAnnounces,
                      homeAnnounces,
                      news,
                      foodsMenu
                    }
                  })
                )
  }//End Of ngOnInit()


  translate(event: SlidesOutputData) {
    const slideId=event.slides[0].id;
    const splitId=slideId.split('_');

    const [id,type,intervalTime]=splitId;
    if(type.toLowerCase()==='video'){
      //this.helperService.setSliderAutoPlayTimeout(25000);
      this.owlElement.options.autoplayTimeout=28000;
      this.cd.detectChanges();
    }

    
    
  }

  onChange(event:SlidesOutputData){
    console.log(event)
  }
}
