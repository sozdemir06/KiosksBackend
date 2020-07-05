import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VehicleBrandsComponent } from './vehicle-brands.component';
import { VehicleBrandsListComponent } from './vehicle-brands-list/vehicle-brands-list.component';
import { EditVehicleBrandsDialogComponent } from './edit-vehicle-brands-dialog/edit-vehicle-brands-dialog.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';

export const routes:Routes=[
  {
    path:"",
    component:VehicleBrandsComponent
  }
]
@NgModule({
  declarations: [
    VehicleBrandsComponent,
    VehicleBrandsListComponent,
    EditVehicleBrandsDialogComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  
  ],
  exports:[
    RouterModule,
    VehicleBrandsComponent,
    VehicleBrandsListComponent,
    EditVehicleBrandsDialogComponent,
  ]
})
export class VehicleBrandsModule {}
