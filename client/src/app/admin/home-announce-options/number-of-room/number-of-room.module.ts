import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NumberOfRoomsComponent } from './number-of-rooms.component';
import { NumberOfRoomsListComponent } from './number-of-rooms-list/number-of-rooms-list.component';
import { EditNumberOfRoomsDialogComponent } from './edit-number-of-rooms-dialog/edit-number-of-rooms-dialog.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { Routes, RouterModule } from '@angular/router';

export const routes:Routes=[
  {
    path:"",
    component:NumberOfRoomsComponent
  }
]


@NgModule({
  declarations: [
    NumberOfRoomsComponent,
    NumberOfRoomsListComponent,
    EditNumberOfRoomsDialogComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],

  exports:[
    RouterModule,
    NumberOfRoomsComponent,
    NumberOfRoomsListComponent,
    EditNumberOfRoomsDialogComponent,
  ]
})
export class NumberOfRoomModule {}
