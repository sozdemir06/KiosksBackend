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
        data: { roles: ['Sudo'] },
      },
      {
        path: 'roles',
        loadChildren: () =>
          import('../roles/roles.module').then((m) => m.RolesModule),
        data: { roles: ['Sudo'] },
      },
      {
        path: 'roles-category',
        loadChildren: () =>
          import('../role-category/role-category.module').then(
            (m) => m.RoleCategoryModule
          ),
        data: { roles: ['Sudo'] },
      },
      {
        path: 'number-of-rooms',
        loadChildren: () =>
          import(
            '../home-announce-options/number-of-room/number-of-room.module'
          ).then((m) => m.NumberOfRoomModule),
        data: { roles: ['Sudo', 'NumberOfRoom.List'] },
      },
      {
        path: 'buildings-age',
        loadChildren: () =>
          import(
            '../home-announce-options/building-age/building-age.module'
          ).then((m) => m.BuildingAgeModule),
        data: { roles: ['Sudo', 'BuildingsAge.List'] },
      },
      {
        path: 'flat-of-home',
        loadChildren: () =>
          import(
            '../home-announce-options/flat-of-home/flat-of-home.module'
          ).then((m) => m.FlatOfHomeModule),
        data: { roles: ['Sudo', 'FlatsOfHome.List'] },
      },
      {
        path: 'heating-types',
        loadChildren: () =>
          import(
            '../home-announce-options/heating-types/heating-types.module'
          ).then((m) => m.HeatingTypesModule),
        data: { roles: ['Sudo', 'HeatingTypes.List'] },
      },
      {
        path: 'vehicle-categories',
        loadChildren: () =>
          import(
            '../vehicle-announce-options/vehicle-categories/vehicle-categories.module'
          ).then((m) => m.VehicleCategoriesModule),
        data: { roles: ['Sudo', 'VehicleCategories.List'] },
      },
      {
        path: 'vehicle-brands',
        loadChildren: () =>
          import(
            '../vehicle-announce-options/vehicle-brands/vehicle-brands.module'
          ).then((m) => m.VehicleBrandsModule),
        data: { roles: ['Sudo', 'VehicleBrands.List'] },
      },
      {
        path: 'vehicle-models',
        loadChildren: () =>
          import(
            '../vehicle-announce-options/vehicle-model/vehicle-model.module'
          ).then((m) => m.VehicleModelModule),
        data: { roles: ['Sudo', 'VehicleModels.List'] },
      },
      {
        path: 'vehicle-fuel-types',
        loadChildren: () =>
          import(
            '../vehicle-announce-options/vehicle-fueltype/vehicle-fueltype.module'
          ).then((m) => m.VehicleFueltypeModule),
        data: { roles: ['Sudo', 'VehicleFuelTypes.List'] },
      },
      {
        path: 'vehicle-gear-types',
        loadChildren: () =>
          import(
            '../vehicle-announce-options/vehicle-geartype/vehicle-geartype.module'
          ).then((m) => m.VehicleGeartypeModule),
        data: { roles: ['Sudo', 'VehicleGearTypes.List'] },
      },
      {
        path: 'vehicle-engine-sizes',
        loadChildren: () =>
          import(
            '../vehicle-announce-options/vehicle-engine-size/vehicle-engine-size.module'
          ).then((m) => m.VehicleEngineSizeModule),
        data: { roles: ['Sudo', 'VehicleEngineSizes.List'] },
      },
      {
        path: 'screens',
        loadChildren: () =>
          import('../screens/screens.module').then((m) => m.ScreensModule),
        data: { roles: ['Sudo', 'Screens.List'] },
      },
      {
        path: 'subscreens/:id',
        loadChildren: () =>
          import('../subscreens/subscreens.module').then(
            (m) => m.SubscreensModule
          ),
        data: { roles: ['Sudo', 'SubScreens.List'] },
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
          data: { roles: ['Sudo', 'AnnounceContentTypes.List', 'Announces.All'] },
      },
      {
        path:"news",
        loadChildren:()=>import("../news/news.module").then(m=>m.NewsModule),
        data:{roles:['Sudo','News.List','News.All']}
      },
      {
        path:"foods-menu",
        loadChildren:()=>import("../food-menu/food-menu.module").then(m=>m.FoodMenuModule),
        data:{roles:['Sudo','FoodMenu.List','FoodMenu.All']}
      }
    ],
  },
];

@NgModule({
  imports: [CommonModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
