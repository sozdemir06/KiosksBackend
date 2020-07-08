import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VehicleEngineSizeComponent } from './vehicle-engine-size.component';
import { VehicleEngineSizeListComponent } from './vehicle-engine-size-list/vehicle-engine-size-list.component';
import { EditVehicleEngineSizeDialogComponent } from './edit-vehicle-engine-size-dialog/edit-vehicle-engine-size-dialog.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { Routes, RouterModule } from '@angular/router';

export const routes:Routes=[
  {
    path:"",
    component:VehicleEngineSizeComponent
  }
]

@NgModule({
  declarations: [
    VehicleEngineSizeComponent,
    VehicleEngineSizeListComponent,
    EditVehicleEngineSizeDialogComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],

  exports:[
    RouterModule,
    VehicleEngineSizeComponent,
    VehicleEngineSizeListComponent,
    EditVehicleEngineSizeDialogComponent,
  ]
})
export class VehicleEngineSizeModule {}
