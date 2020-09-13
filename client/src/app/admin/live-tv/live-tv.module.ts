import { NgModule } from '@angular/core';
import { LiveTvComponent } from './live-tv.component';
import { LiveTvListComponent } from './live-tv-list/live-tv-list.component';
import { LiveTvDetailComponent } from './live-tv-detail/live-tv-detail.component';
import { EditLiveTvDialogComponent } from './edit-live-tv-dialog/edit-live-tv-dialog.component';
import { EditLiveTvSubscreensComponent } from './edit-live-tv-subscreens/edit-live-tv-subscreens.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';


export const routes:Routes=[
  {
    path:"",
    component:LiveTvComponent
  },
  {
    path:"detail/:id",
    component:LiveTvDetailComponent
  }
]

@NgModule({
  declarations: [
    LiveTvComponent,
    LiveTvListComponent,
    LiveTvDetailComponent,
    EditLiveTvDialogComponent,
    EditLiveTvSubscreensComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes),


  ],
  exports:[
    LiveTvComponent,
    LiveTvListComponent,
    LiveTvDetailComponent,
    EditLiveTvDialogComponent,
    EditLiveTvSubscreensComponent,
    RouterModule,

  ]
})
export class LiveTvModule {}
