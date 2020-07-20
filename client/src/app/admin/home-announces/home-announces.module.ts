import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeAnnounceComponent } from './home-announce.component';
import { HomeAnnounceListComponent } from './home-announce-list/home-announce-list.component';
import { EditHomeAnnounceDialogComponent } from './edit-home-announce-dialog/edit-home-announce-dialog.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { Routes, RouterModule } from '@angular/router';

export const routes:Routes=[
  {
    path:"",
    component:HomeAnnounceComponent
  }
]

@NgModule({
  declarations: [
    HomeAnnounceComponent,
    HomeAnnounceListComponent,
    EditHomeAnnounceDialogComponent,
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
  ]
})
export class HomeAnnouncesModule {}
