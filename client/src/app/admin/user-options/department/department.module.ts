import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DepartmentComponent } from './department.component';
import { DepartmentListComponent } from './department-list/department-list.component';
import { EditDepartmentDialogComponent } from './edit-department-dialog/edit-department-dialog.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { RouterModule, Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    component: DepartmentComponent,
  },
];

@NgModule({
  declarations: [
    DepartmentComponent,
    DepartmentListComponent,
    EditDepartmentDialogComponent,
  ],
  imports: [
    SharedModule, 
    RouterModule.forChild(routes)
  ],
  exports: [
    DepartmentComponent,
    DepartmentListComponent,
    EditDepartmentDialogComponent,
    RouterModule,
  ],
})
export class DepartmentModule {}
