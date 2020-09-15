import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DegreeComponent } from './degree.component';
import { DegreeListComponent } from './degree-list/degree-list.component';
import { EditDegreeDialogComponent } from './edit-degree-dialog/edit-degree-dialog.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';

export const routes: Routes = [
  {
    path: '',
    component: DegreeComponent,
  },
];

@NgModule({
  declarations: [
    DegreeComponent,
    DegreeListComponent,
    EditDegreeDialogComponent,
  ],
  imports: [SharedModule, RouterModule.forChild(routes)],

  exports: [
    DegreeComponent,
    DegreeListComponent,
    EditDegreeDialogComponent,
    RouterModule,
  ],
})
export class DegreeModule {}
