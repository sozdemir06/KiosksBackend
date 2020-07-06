import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VehicleModelsComponent } from './vehicle-models.component';
import { VehicleModelListComponent } from './vehicle-model-list/vehicle-model-list.component';
import { EditVehicleModelDialogComponent } from './edit-vehicle-model-dialog/edit-vehicle-model-dialog.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';
export const routes:Routes=[
  {
    path:"",
    component:VehicleModelsComponent
  }
]
@NgModule({
  declarations: [
    VehicleModelsComponent,
    VehicleModelListComponent,
    EditVehicleModelDialogComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],

  exports:[
    RouterModule,
    VehicleModelsComponent,
    VehicleModelListComponent,
    EditVehicleModelDialogComponent,
  ]
})
export class VehicleModelModule {}
