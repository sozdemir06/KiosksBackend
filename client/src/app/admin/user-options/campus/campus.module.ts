import { NgModule } from '@angular/core';
import { CampusComponent } from './campus.component';
import { CampusListComponent } from './campus-list/campus-list.component';
import { EditCampusDialogComponent } from './edit-campus-dialog/edit-campus-dialog.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { RouterModule, Routes } from '@angular/router';
export const routes: Routes = [
  {
    path: '',
    component: CampusComponent,
  },
];

@NgModule({
  declarations: [
    CampusComponent,
    CampusListComponent,
    EditCampusDialogComponent,
  ],
  imports: [SharedModule, RouterModule.forChild(routes)],

  exports: [
    CampusComponent,
    CampusListComponent,
    EditCampusDialogComponent,
    RouterModule,
  ],
})
export class CampusModule {}
