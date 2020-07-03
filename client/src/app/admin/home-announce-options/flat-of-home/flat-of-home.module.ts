import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlatsOfHomeComponent } from './flats-of-home.component';
import { FlatsOfHomeListComponent } from './flats-of-home-list/flats-of-home-list.component';
import { EditFlatsOfHomeDialogComponent } from './edit-flats-of-home-dialog/edit-flats-of-home-dialog.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';


export const routes:Routes=[
    {
      path:"",
      component:FlatsOfHomeComponent
    }
]

@NgModule({
  declarations: [
    FlatsOfHomeComponent,
    FlatsOfHomeListComponent,
    EditFlatsOfHomeDialogComponent,
  ],
  imports: [
    RouterModule.forChild(routes),
    SharedModule
  ],

  exports:[
    RouterModule,
    FlatsOfHomeComponent,
    FlatsOfHomeListComponent,
    EditFlatsOfHomeDialogComponent,
  ]
})
export class FlatOfHomeModule {}
