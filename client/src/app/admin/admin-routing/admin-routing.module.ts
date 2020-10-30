import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { AuthGuard } from 'src/app/core/guards/auth-guard';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'users',
        loadChildren: () =>
          import('../users/users.module').then((m) => m.UsersModule),
        data: { roles: ['Sudo', 'User.List', 'User.All'] },
      },
      {
        path: 'roles',
        loadChildren: () =>
          import('../roles/roles.module').then((m) => m.RolesModule),
        data: { roles: ['Sudo', 'Roles.List', 'Roles.All'] },
      },
      {
        path: 'roles-category',
        loadChildren: () =>
          import('../role-category/role-category.module').then(
            (m) => m.RoleCategoryModule
          ),
        data: { roles: ['Sudo', 'Roles.List', 'Roles.All'] },
      },
      {
        path: 'number-of-rooms',
        loadChildren: () =>
          import(
            '../home-announce-options/number-of-room/number-of-room.module'
          ).then((m) => m.NumberOfRoomModule),
        data: { roles: ['Sudo', 'HomeAnnounceOptions.All'] },
      },
      {
        path: 'buildings-age',
        loadChildren: () =>
          import(
            '../home-announce-options/building-age/building-age.module'
          ).then((m) => m.BuildingAgeModule),
        data: { roles: ['Sudo', 'HomeAnnounceOptions.All'] },
      },
      {
        path: 'flat-of-home',
        loadChildren: () =>
          import(
            '../home-announce-options/flat-of-home/flat-of-home.module'
          ).then((m) => m.FlatOfHomeModule),
        data: { roles: ['Sudo', 'HomeAnnounceOptions.All'] },
      },
      {
        path: 'heating-types',
        loadChildren: () =>
          import(
            '../home-announce-options/heating-types/heating-types.module'
          ).then((m) => m.HeatingTypesModule),
        data: { roles: ['Sudo', 'HomeAnnounceOptions.All'] },
      },
      {
        path: 'vehicle-categories',
        loadChildren: () =>
          import(
            '../vehicle-announce-options/vehicle-categories/vehicle-categories.module'
          ).then((m) => m.VehicleCategoriesModule),
        data: { roles: ['Sudo', 'VehicleAnnounceOptions.All'] },
      },
      {
        path: 'vehicle-brands',
        loadChildren: () =>
          import(
            '../vehicle-announce-options/vehicle-brands/vehicle-brands.module'
          ).then((m) => m.VehicleBrandsModule),
        data: { roles: ['Sudo', 'VehicleAnnounceOptions.All'] },
      },
      {
        path: 'vehicle-models',
        loadChildren: () =>
          import(
            '../vehicle-announce-options/vehicle-model/vehicle-model.module'
          ).then((m) => m.VehicleModelModule),
        data: { roles: ['Sudo', 'VehicleAnnounceOptions.All'] },
      },
      {
        path: 'vehicle-fuel-types',
        loadChildren: () =>
          import(
            '../vehicle-announce-options/vehicle-fueltype/vehicle-fueltype.module'
          ).then((m) => m.VehicleFueltypeModule),
        data: { roles: ['Sudo', 'VehicleAnnounceOptions.All'] },
      },
      {
        path: 'vehicle-gear-types',
        loadChildren: () =>
          import(
            '../vehicle-announce-options/vehicle-geartype/vehicle-geartype.module'
          ).then((m) => m.VehicleGeartypeModule),
        data: { roles: ['Sudo', 'VehicleAnnounceOptions.All'] },
      },
      {
        path: 'vehicle-engine-sizes',
        loadChildren: () =>
          import(
            '../vehicle-announce-options/vehicle-engine-size/vehicle-engine-size.module'
          ).then((m) => m.VehicleEngineSizeModule),
        data: { roles: ['Sudo', 'VehicleAnnounceOptions.All'] },
      },
      {
        path: 'screens',
        loadChildren: () =>
          import('../screens/screens.module').then((m) => m.ScreensModule),
        data: { roles: ['Sudo', 'Screens.List', 'Screens.All'] },
      },
      {
        path: 'subscreens/:id',
        loadChildren: () =>
          import('../subscreens/subscreens.module').then(
            (m) => m.SubscreensModule
          ),
        data: { roles: ['Sudo', 'SubScreens.List', 'SubScreens.All'] },
      },
      {
        path: 'home-announces',
        loadChildren: () =>
          import('../home-announces/home-announces.module').then(
            (m) => m.HomeAnnouncesModule
          ),
        data: { roles: ['Sudo', 'HomeAnnounces.List,HomeAnnounces.All'] },
      },
      {
        path: 'vehicle-announces',
        loadChildren: () =>
          import('../vehicle-announces/vehicle-announces.module').then(
            (m) => m.VehicleAnnouncesModule
          ),
        data: { roles: ['Sudo', 'VehicleAnnounces.List,VehicleAnnounces.All'] },
      },
      {
        path: 'announces',
        loadChildren: () =>
          import('../announces/announces.module').then(
            (m) => m.AnnouncesModule
          ),
        data: { roles: ['Sudo', 'Announces.List', 'Announces.All'] },
      },
      {
        path: 'announce-content-types',
        loadChildren: () =>
          import(
            '../announces-options/announce-content-types/announce-content-types.module'
          ).then((m) => m.AnnounceContentTypesModule),
        data: { roles: ['Sudo', 'Announces.List', 'Announces.All'] },
      },
      {
        path: 'news',
        loadChildren: () =>
          import('../news/news.module').then((m) => m.NewsModule),
        data: { roles: ['Sudo', 'News.List', 'News.All'] },
      },
      {
        path: 'foods-menu',
        loadChildren: () =>
          import('../food-menu/food-menu.module').then((m) => m.FoodMenuModule),
        data: { roles: ['Sudo', 'FoodMenu.List', 'FoodMenu.All'] },
      },
      {
        path: 'cities',
        loadChildren: () =>
          import('../city/city.module').then((m) => m.CityModule),
        data: { roles: ['Sudo', 'AddCityForWheatherForeCast'] },
      },
      {
        path: 'currencies',
        loadChildren: () =>
          import('../currency/currency.module').then((m) => m.CurrencyModule),
        data: { roles: ['Sudo', 'AddMoneyForExchangeRate'] },
      },
      {
        path: 'live-tv-broadcast',
        loadChildren: () =>
          import('../live-tv/live-tv.module').then((m) => m.LiveTvModule),
        data: {
          roles: ['Sudo', 'LiveTvBroadCasts.List', 'LiveTvBroadCasts.All'],
        },
      },
      {
        path: 'live-tv-list',
        loadChildren: () =>
          import('../live-tv-options/live-tv-options.module').then(
            (m) => m.LiveTvOptionsModule
          ),
        data: { roles: ['Sudo', 'LiveTvBroadCastsOptions.All'] },
      },
      {
        path: 'campuses',
        loadChildren: () =>
          import('../user-options/campus/campus.module').then(
            (m) => m.CampusModule
          ),
        data: { roles: ['Sudo', 'UserOptions.All'] },
      },
      {
        path: 'departments',
        loadChildren: () =>
          import('../user-options/department/department.module').then(
            (m) => m.DepartmentModule
          ),
        data: { roles: ['Sudo', 'UserOptions.All'] },
      },
      {
        path: 'degrees',
        loadChildren: () =>
          import('../user-options/degree/degree.module').then(
            (m) => m.DegreeModule
          ),
        data: { roles: ['Sudo', 'UserOptions.All'] },
      },
      {
        path:"logo",
        loadChildren:()=>import("../public-logo/public-logo.module").then(m=>m.PublicLogoModule),
        data:{roles:['Sudo']}
      },
      {
        path:"footer-text",
        loadChildren:()=>import("../public-footer-text/public-footer-text.module").then(m=>m.PublicFooterTextModule),
        data:{roles:['Sudo']}
      }
    ],
  },
];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
