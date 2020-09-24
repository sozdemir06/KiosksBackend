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
import { KiosksUserCardComponent } from './kiosks-user-card/kiosks-user-card.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ScreenLeftComponent } from './screen-horizontal/screen-left/screen-left.component';
import { ScreenHmiddleComponent } from './screen-horizontal/screen-hmiddle/screen-hmiddle.component';
import { ScreenRightComponent } from './screen-horizontal/screen-right/screen-right.component';
import {YouTubePlayerModule} from '@angular/youtube-player';

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
    ScreenLeftComponent,
    ScreenHmiddleComponent,
    ScreenRightComponent,
  
  ],
  imports: [
    SharedModule, 
    RouterModule.forChild(routes),
    NgbModule,
    YouTubePlayerModule
    
  ],
  exports: [
    KiosksComponent,
    RouterModule,
    ScreenTopComponent,
    ScreenMiddleComponent,
    ScreenBottomComponent,
    YouTubePlayerModule
  ],
})
export class KiosksModule {}
