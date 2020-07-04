import { NgModule } from '@angular/core';
import { HeatingTypesComponent } from './heating-types.component';
import { HeatingTypesListComponent } from './heating-types-list/heating-types-list.component';
import { EditHeatingTypesDialogComponent } from './edit-heating-types-dialog/edit-heating-types-dialog.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';

export const routes: Routes = [
  {
    path: '',
    component: HeatingTypesComponent,
  },
];

@NgModule({
  declarations: [
    HeatingTypesComponent,
    HeatingTypesListComponent,
    EditHeatingTypesDialogComponent,
  ],
  imports: [SharedModule, RouterModule.forChild(routes)],

  exports: [
    RouterModule,
    HeatingTypesComponent,
    HeatingTypesListComponent,
    EditHeatingTypesDialogComponent,
  ],
})
export class HeatingTypesModule {}
