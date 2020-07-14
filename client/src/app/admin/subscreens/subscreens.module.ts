import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SubscreensComponent } from './subscreens.component';
import { SubscreensListComponent } from './subscreens-list/subscreens-list.component';
import { EditSubscreensDialogComponent } from './edit-subscreens-dialog/edit-subscreens-dialog.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { Routes, RouterModule } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    component: SubscreensComponent,
  },
];

@NgModule({
  declarations: [
    SubscreensComponent,
    SubscreensListComponent,
    EditSubscreensDialogComponent,
  ],
  imports: [SharedModule, RouterModule.forChild(routes)],

  exports: [
    RouterModule,
    SubscreensComponent,
    SubscreensListComponent,
    EditSubscreensDialogComponent,
  ],
})
export class SubscreensModule {}
