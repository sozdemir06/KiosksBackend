import { NgModule } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { SectionHeaderComponent } from './section-header/section-header.component';
import { MainHomeComponent } from './main-home/main-home.component';
import { AllAnnounceComponent } from './all-announce/all-announce.component';
import { PublicAnnouncesListComponent } from './lists/public-announces-list/public-announces-list.component';
import { PublicVehicleAnnouncesListComponent } from './lists/public-vehicle-announces-list/public-vehicle-announces-list.component';
import { PublicHomeAnnouncesListComponent } from './lists/public-home-announces-list/public-home-announces-list.component';
import { PublicNewsListComponent } from './lists/public-news-list/public-news-list.component';
import { PublicFoodmenuListComponent } from './lists/public-foodmenu-list/public-foodmenu-list.component';
import { PublicAnnounceDetailComponent } from './details/public-announce-detail/public-announce-detail.component';
import { PublicHomeAnnounceDetailComponent } from './details/public-home-announce-detail/public-home-announce-detail.component';
import { PublicVehicleAnnounceDetailComponent } from './details/public-vehicle-announce-detail/public-vehicle-announce-detail.component';
import { PublicNewsDetailComponent } from './details/public-news-detail/public-news-detail.component';
import { PublicFoodMenuDetailComponent } from './details/public-food-menu-detail/public-food-menu-detail.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    children: [
      {
        path: '',
        component: MainHomeComponent,
        children: [
          {
            path: '',
            component: AllAnnounceComponent,
          },
          {
            path: 'announces',
            component: PublicAnnouncesListComponent,
          },
          {
            path: 'home-announces',
            component: PublicHomeAnnouncesListComponent,
          },
          {
            path: 'vehicle-announces',
            component: PublicVehicleAnnouncesListComponent,
          },
          {
            path: 'news',
            component: PublicNewsListComponent,
          },
          {
            path: 'food-menu',
            component: PublicFoodmenuListComponent,
          },
        ],
        
      },

      {
        path:"user-profile/me",
        loadChildren:()=>import("./user-profile/user-profile.module").then(m=>m.UserProfileModule),

      }
    ],
  },
];

@NgModule({
  declarations: [
    HomeComponent,
    SectionHeaderComponent,
    MainHomeComponent,
    AllAnnounceComponent,
    PublicAnnouncesListComponent,
    PublicVehicleAnnouncesListComponent,
    PublicHomeAnnouncesListComponent,
    PublicNewsListComponent,
    PublicFoodmenuListComponent,
    PublicAnnounceDetailComponent,
    PublicHomeAnnounceDetailComponent,
    PublicVehicleAnnounceDetailComponent,
    PublicNewsDetailComponent,
    PublicFoodMenuDetailComponent,
  ],
  imports: [SharedModule, RouterModule.forChild(routes),NgbModule],
  exports: [
    HomeComponent,
    RouterModule,
    MainHomeComponent,
    AllAnnounceComponent,
    PublicAnnouncesListComponent,
    PublicVehicleAnnouncesListComponent,
    PublicHomeAnnouncesListComponent,
    PublicNewsListComponent,
    PublicFoodmenuListComponent,
    PublicAnnounceDetailComponent,
    PublicHomeAnnounceDetailComponent,
    PublicVehicleAnnounceDetailComponent,
    PublicNewsDetailComponent,
    PublicFoodMenuDetailComponent,
    NgbModule
  ],
})
export class PublicModule {}
