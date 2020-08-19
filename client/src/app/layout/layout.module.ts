import { NgModule } from '@angular/core';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { LayoutComponent } from './layout.component';
import { Routes, RouterModule } from '@angular/router';
import { NotFoundComponent } from '../shared/not-found/not-found.component';
import { UserPanelComponent } from '../admin/dashboard/panels/user-panel/user-panel.component';
import { RolePanelComponent } from '../admin/dashboard/panels/role-panel/role-panel.component';
import { SharedModule } from '../shared/shared.module';
import { HomeAnnounceOptionListComponent } from '../admin/dashboard/panels/home-announce-option-list/home-announce-option-list.component';
import { VehicleAnnounceOptionListComponent } from '../admin/dashboard/panels/vehicle-announce-option-list/vehicle-announce-option-list.component';
import { ScreenPanelComponent } from '../admin/dashboard/panels/screen-panel/screen-panel.component';
import { HomeAnnouncesPanelComponent } from '../admin/dashboard/panels/home-announces-panel/home-announces-panel.component';
import { VehicleAnnouncesPanelComponent } from '../admin/dashboard/vehicle-announces-panel/vehicle-announces-panel.component';
import { AnnouncePanelComponent } from '../admin/dashboard/announce-panel/announce-panel.component';
import { AnnounceOptionsPanelComponent } from '../admin/dashboard/announce-options-panel/announce-options-panel.component';
import { NewsPanelComponent } from '../admin/dashboard/news-panel/news-panel.component';
import { FoodMenuPanelComponent } from '../admin/dashboard/food-menu-panel/food-menu-panel.component';

export const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      {
        path: '',
        loadChildren: () =>
          import('../public/public.module').then((m) => m.PublicModule),
      },

      {
        path: 'admin',
        loadChildren: () =>
          import('../admin/admin.module').then((m) => m.AdminModule),
      },
      {
        path: 'auth',
        loadChildren: () =>
          import('../auth/auth.module').then((m) => m.AuthModule),
      },
      {
        path: 'not-found',
        component: NotFoundComponent,
        pathMatch:"full"
      },
      {
        path: '**',
        component: NotFoundComponent,
      },
    ],
  },
];

@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    LayoutComponent,
    UserPanelComponent,
    RolePanelComponent,
    HomeAnnounceOptionListComponent,
    VehicleAnnounceOptionListComponent,
    ScreenPanelComponent,
    HomeAnnouncesPanelComponent,
    VehicleAnnouncesPanelComponent,
    AnnouncePanelComponent,
    AnnounceOptionsPanelComponent,
    NewsPanelComponent,
    FoodMenuPanelComponent,
  ],
  imports: [SharedModule, RouterModule.forChild(routes)],

  exports: [
    HeaderComponent,
    FooterComponent,
    LayoutComponent,
    RouterModule,
    UserPanelComponent,
    RolePanelComponent,
    HomeAnnounceOptionListComponent,
    VehicleAnnounceOptionListComponent,
    ScreenPanelComponent,
    HomeAnnouncesPanelComponent,
    VehicleAnnouncesPanelComponent,
    AnnouncePanelComponent,
    AnnounceOptionsPanelComponent,
    NewsPanelComponent,
    FoodMenuPanelComponent,
  ],
})
export class LayoutModule {}
