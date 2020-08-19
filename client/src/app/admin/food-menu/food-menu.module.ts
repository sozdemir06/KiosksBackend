import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FoodMenuComponent } from './food-menu.component';
import { FoodMenuListComponent } from './food-menu-list/food-menu-list.component';
import { EditFoodMenuDialogComponent } from './edit-food-menu-dialog/edit-food-menu-dialog.component';
import { FoodMenuDetailComponent } from './food-menu-detail/food-menu-detail.component';
import { FoodMenuPhotoListComponent } from './food-menu-photo-list/food-menu-photo-list.component';
import { FoodMenuSubscreenListComponent } from './food-menu-subscreen-list/food-menu-subscreen-list.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { Routes, RouterModule } from '@angular/router';

export const routes:Routes=[
  {
    path:"",
    component:FoodMenuComponent
  },
  {
    path:"detail/:id",
    component:FoodMenuDetailComponent
  }
]

@NgModule({
  declarations: [
    FoodMenuComponent,
    FoodMenuListComponent,
    EditFoodMenuDialogComponent,
    FoodMenuDetailComponent,
    FoodMenuPhotoListComponent,
    FoodMenuSubscreenListComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],

  exports:[
    RouterModule,
    FoodMenuComponent,
    FoodMenuListComponent,
    EditFoodMenuDialogComponent,
    FoodMenuDetailComponent,
    FoodMenuPhotoListComponent,
    FoodMenuSubscreenListComponent,
  ]
})
export class FoodMenuModule {}
