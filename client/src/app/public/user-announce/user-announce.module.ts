import { NgModule } from '@angular/core';
import { UserAnnouncesComponent } from './user-announces.component';
import { PublicUserAnnounceComponent } from './public-user-announce/public-user-announce.component';
import { PublicUserHomeannounceComponent } from './public-user-homeannounce/public-user-homeannounce.component';
import { PublicUserVehicleannounceComponent } from './public-user-vehicleannounce/public-user-vehicleannounce.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';
import { EditUserAnnounceDialogComponent } from './edit-user-announce-dialog/edit-user-announce-dialog.component';
import { EditUserHomeAnnounceDialogComponent } from './edit-user-home-announce-dialog/edit-user-home-announce-dialog.component';
import { EditUserVehicleAnnounceDialogComponent } from './edit-user-vehicle-announce-dialog/edit-user-vehicle-announce-dialog.component';
import { EditUserAnnouncePhotoDialogComponent } from './edit-user-announce-photo-dialog/edit-user-announce-photo-dialog.component';
import { EditUserHomeAnnouncePhotoDialogComponent } from './edit-user-home-announce-photo-dialog/edit-user-home-announce-photo-dialog.component';
import { EditUserVehicleAnnouncePhotoDialogComponent } from './edit-user-vehicle-announce-photo-dialog/edit-user-vehicle-announce-photo-dialog.component';

export const routes:Routes=[
  {
    path:"",
    component:UserAnnouncesComponent,
    children:[
      {
        path:"",
        component:PublicUserAnnounceComponent
      },
      {
        path:"vehicle-announces",
        component:PublicUserVehicleannounceComponent
      },
      {
        path:"home-announces",
        component:PublicUserHomeannounceComponent
      }
    ]
  }
]


@NgModule({
  declarations: [
    UserAnnouncesComponent,
    PublicUserAnnounceComponent,
    PublicUserHomeannounceComponent,
    PublicUserVehicleannounceComponent,
    EditUserAnnounceDialogComponent,
    EditUserHomeAnnounceDialogComponent,
    EditUserVehicleAnnounceDialogComponent,
    EditUserAnnouncePhotoDialogComponent,
    EditUserHomeAnnouncePhotoDialogComponent,
    EditUserVehicleAnnouncePhotoDialogComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],

  exports:[
    UserAnnouncesComponent,
    PublicUserAnnounceComponent,
    PublicUserHomeannounceComponent,
    PublicUserVehicleannounceComponent,
    EditUserAnnounceDialogComponent,
    EditUserHomeAnnounceDialogComponent,
    EditUserVehicleAnnounceDialogComponent,
    RouterModule
  ]
})
export class UserAnnounceModule {}
