import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeAnnounceComponent } from './home-announce.component';
import { HomeAnnounceListComponent } from './home-announce-list/home-announce-list.component';
import { EditHomeAnnounceDialogComponent } from './edit-home-announce-dialog/edit-home-announce-dialog.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { Routes, RouterModule } from '@angular/router';
import { HomeAnnounceDetailComponent } from './home-announce-detail/home-announce-detail.component';
import { HomeAnnouncePhotoListComponent } from './home-announce-photo-list/home-announce-photo-list.component';
import { HomeAnnounceSubscreensComponent } from './home-announce-subscreens/home-announce-subscreens.component';

export const routes:Routes=[
  {
    path:"",
    component:HomeAnnounceComponent,
  },
  {
    path:"detail/:id",
    component:HomeAnnounceDetailComponent
  }
]

@NgModule({
  declarations: [
    HomeAnnounceComponent,
    HomeAnnounceListComponent,
    EditHomeAnnounceDialogComponent,
    HomeAnnounceDetailComponent,
    HomeAnnouncePhotoListComponent,
    HomeAnnounceSubscreensComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  exports:[
    RouterModule,
    HomeAnnounceComponent,
    HomeAnnounceListComponent,
    EditHomeAnnounceDialogComponent,
    HomeAnnounceDetailComponent,
    HomeAnnounceSubscreensComponent
  ]
})
export class HomeAnnouncesModule {}
