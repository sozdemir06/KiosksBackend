import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VehicleGeartypeComponent } from './vehicle-geartype.component';
import { VehicleGeartypeListComponent } from './vehicle-geartype-list/vehicle-geartype-list.component';
import { EditVehicleGeartypeDialogComponent } from './edit-vehicle-geartype-dialog/edit-vehicle-geartype-dialog.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';

export const routes:Routes=[
  {
    path:"",
    component:VehicleGeartypeComponent
  }
]

@NgModule({
  declarations: [
    VehicleGeartypeComponent,
    VehicleGeartypeListComponent,
    EditVehicleGeartypeDialogComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  
  ],
  exports:[
    RouterModule,
    VehicleGeartypeComponent,
    VehicleGeartypeListComponent,
    EditVehicleGeartypeDialogComponent,
  ]
})
export class VehicleGeartypeModule {}
