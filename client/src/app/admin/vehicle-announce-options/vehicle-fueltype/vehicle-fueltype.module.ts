import { NgModule } from '@angular/core';
import { VehicleFueltypeComponent } from './vehicle-fueltype.component';
import { VehicleFueltypeListComponent } from './vehicle-fueltype-list/vehicle-fueltype-list.component';
import { EditVehicleFueltypeDialogComponent } from './edit-vehicle-fueltype-dialog/edit-vehicle-fueltype-dialog.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';

export const routes: Routes = [
  {
    path: '',
    component: VehicleFueltypeComponent,
  },
];
@NgModule({
  declarations: [
    VehicleFueltypeComponent,
    VehicleFueltypeListComponent,
    EditVehicleFueltypeDialogComponent,
  ],
  imports: [
    SharedModule,
     RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule,
    VehicleFueltypeComponent,
    VehicleFueltypeListComponent,
    EditVehicleFueltypeDialogComponent,
  ],
})
export class VehicleFueltypeModule {}
