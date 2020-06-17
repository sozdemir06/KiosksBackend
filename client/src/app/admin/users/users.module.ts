import { NgModule } from '@angular/core';
import { UsersComponent } from './users.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { UsersListCardComponent } from './users-list-card/users-list-card.component';
import { Routes, RouterModule } from '@angular/router';
import { MaterialModule } from 'src/app/material/material.module';
import { UserEditDialogComponent } from './user-edit-dialog/user-edit-dialog.component';

const routes:Routes=[
  {
    path:"",
    component:UsersComponent
  }
]


@NgModule({
  declarations: [
    UsersComponent,
    UsersListCardComponent,
    UserEditDialogComponent
  ],
  imports: [
    SharedModule,
    MaterialModule,
    RouterModule.forChild(routes)
    
  ],
  exports:[
    UsersComponent,
    UsersListCardComponent,
    RouterModule,
    UserEditDialogComponent
  ]
})
export class UsersModule { }
