import { NgModule } from '@angular/core';
import { VehicleCategoriesComponent } from './vehicle-categories.component';
import { VehicleCategoryListComponent } from './vehicle-category-list/vehicle-category-list.component';
import { EditVehicleCategoryDialogComponent } from './edit-vehicle-category-dialog/edit-vehicle-category-dialog.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { Routes, RouterModule } from '@angular/router';
export const routes:Routes=[
  {
    path:"",
    component:VehicleCategoriesComponent
  }
]

@NgModule({
  declarations: [
    VehicleCategoriesComponent,
    VehicleCategoryListComponent,
    EditVehicleCategoryDialogComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],

  exports:[
    RouterModule,
    VehicleCategoriesComponent,
    VehicleCategoryListComponent,
    EditVehicleCategoryDialogComponent,
  ]
})
export class VehicleCategoriesModule {}
