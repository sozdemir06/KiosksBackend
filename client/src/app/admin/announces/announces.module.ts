import { NgModule } from '@angular/core';
import { AnnouncesComponent } from './announces.component';
import { AnnouncesListComponent } from './announces-list/announces-list.component';
import { AnnouncesDetailComponent } from './announces-detail/announces-detail.component';
import { EditAnnouncesDialogComponent } from './edit-announces-dialog/edit-announces-dialog.component';
import { AnnouncesPhotoListComponent } from './announces-photo-list/announces-photo-list.component';
import { AnnouncesSubscreensComponent } from './announces-subscreens/announces-subscreens.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';


export const routes: Routes = [
  {
    path: '',
    component: AnnouncesComponent,
  },
  {
    path: 'detail/:id',
    component: AnnouncesDetailComponent,
  },
];

@NgModule({
  declarations: [
    AnnouncesComponent,
    AnnouncesListComponent,
    AnnouncesDetailComponent,
    EditAnnouncesDialogComponent,
    AnnouncesPhotoListComponent,
    AnnouncesSubscreensComponent,
  ],
  imports: [SharedModule, RouterModule.forChild(routes)],

  exports: [
    RouterModule,
    AnnouncesComponent,
    AnnouncesListComponent,
    AnnouncesDetailComponent,
    EditAnnouncesDialogComponent,
    AnnouncesPhotoListComponent,
    AnnouncesSubscreensComponent,
  ],
})
export class AnnouncesModule {}
