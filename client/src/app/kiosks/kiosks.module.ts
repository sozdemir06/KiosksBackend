import { NgModule } from '@angular/core';
import { KiosksComponent } from './kiosks.component';
import { Routes, RouterModule } from '@angular/router';
import { ScreenIsfullComponent } from './screen-isfull/screen-isfull.component';
import { SharedModule } from '../shared/shared.module';
import { ScreenHeaderComponent } from './screen-header/screen-header.component';
import { ScreenFooterComponent } from './screen-footer/screen-footer.component';
import { ScreenVerticalComponent } from './screen-vertical/screen-vertical.component';
import { ScreenHorizontalComponent } from './screen-horizontal/screen-horizontal.component';
import { ScreenTopComponent } from './screen-vertical/screen-top/screen-top.component';
import { ScreenMiddleComponent } from './screen-vertical/screen-middle/screen-middle.component';
import { ScreenBottomComponent } from './screen-vertical/screen-bottom/screen-bottom.component';
import { AnnounceComponent } from './details/announce/announce.component';
import { HomeannounceComponent } from './details/homeannounce/homeannounce.component';
import { VehicleannounceComponent } from './details/vehicleannounce/vehicleannounce.component';
import { NewsComponent } from './details/news/news.component';
import { FoodMenuComponent } from './details/food-menu/food-menu.component';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { KiosksUserCardComponent } from './kiosks-user-card/kiosks-user-card.component';
import { ImageComponent } from './details/announce/image/image.component';
import { VideoComponent } from './details/announce/video/video.component';
import { DeathannounceComponent } from './details/announce/deathannounce/deathannounce.component';
import { BloodannounceComponent } from './details/announce/bloodannounce/bloodannounce.component';
import { GeneralannounceComponent } from './details/announce/generalannounce/generalannounce.component';
import { CarouselService } from 'ngx-owl-carousel-o/lib/services/carousel.service';
export const routes: Routes = [
  {
    path: '',
    component: KiosksComponent,
  },
];

@NgModule({
  declarations: [
    KiosksComponent,
    ScreenIsfullComponent,
    ScreenHeaderComponent,
    ScreenFooterComponent,
    ScreenVerticalComponent,
    ScreenHorizontalComponent,
    ScreenTopComponent,
    ScreenMiddleComponent,
    ScreenBottomComponent,
    AnnounceComponent,
    HomeannounceComponent,
    VehicleannounceComponent,
    NewsComponent,
    FoodMenuComponent,
    KiosksUserCardComponent,
    ImageComponent,
    VideoComponent,
    DeathannounceComponent,
    BloodannounceComponent,
    GeneralannounceComponent,
  ],
  imports: [SharedModule, RouterModule.forChild(routes),CarouselModule],
  exports: [
    KiosksComponent,
    RouterModule,
    ScreenTopComponent,
    ScreenMiddleComponent,
    ScreenBottomComponent,
   
  ],
})
export class KiosksModule {}
