import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LiveTvOptionsListComponent } from './live-tv-options-list/live-tv-options-list.component';
import { EditLiveTvDialogComponent } from '../live-tv/edit-live-tv-dialog/edit-live-tv-dialog.component';
import { LiveTvOptionsComponent } from './live-tv-options.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { Routes, RouterModule } from '@angular/router';
import { EditTvOptionsDialogComponent } from './edit-tv-options-dialog/edit-tv-options-dialog.component';

export const routes:Routes=[
  {
    path:"",
    component:LiveTvOptionsComponent
  }
]

@NgModule({
  declarations: [
    EditTvOptionsDialogComponent,
    LiveTvOptionsListComponent,
    LiveTvOptionsComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  exports:[
    EditTvOptionsDialogComponent,
    LiveTvOptionsListComponent,
    LiveTvOptionsComponent,
    RouterModule
  ]
})
export class LiveTvOptionsModule {}
