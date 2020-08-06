import { NgModule } from '@angular/core';
import { VehicleAnnounceComponent } from './vehicle-announce.component';
import { VehicleAnnounceListComponent } from './vehicle-announce-list/vehicle-announce-list.component';
import { EditVehicleAnnounceDialogComponent } from './edit-vehicle-announce-dialog/edit-vehicle-announce-dialog.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { Routes, RouterModule } from '@angular/router';
import { VehicleAnnounceDetailComponent } from './vehicle-announce-detail/vehicle-announce-detail.component';
import { VehicleAnnouncePhotoListComponent } from './vehicle-announce-photo-list/vehicle-announce-photo-list.component';
import { VehicleAnnounceSubscreensComponent } from './vehicle-announce-subscreens/vehicle-announce-subscreens.component';

export const routes:Routes=[
  {
    path:"",
    component:VehicleAnnounceComponent
  },
  {
    path:"detail/:id",
    component:VehicleAnnounceDetailComponent
  }
]

@NgModule({
  declarations: [
    VehicleAnnounceComponent,
    VehicleAnnounceListComponent,
    EditVehicleAnnounceDialogComponent,
    VehicleAnnounceDetailComponent,
    VehicleAnnouncePhotoListComponent,
    VehicleAnnounceSubscreensComponent
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  exports:[
    VehicleAnnounceComponent,
    VehicleAnnounceListComponent,
    EditVehicleAnnounceDialogComponent,
    VehicleAnnounceDetailComponent,
    VehicleAnnouncePhotoListComponent,
    VehicleAnnounceSubscreensComponent,
    RouterModule
  ]
})
export class VehicleAnnouncesModule {}
