import { NgModule } from '@angular/core';
import { UsersComponent } from './users.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { UsersListCardComponent } from './users-list-card/users-list-card.component';
import { Routes, RouterModule } from '@angular/router';
import { MaterialModule } from 'src/app/material/material.module';
import { UserEditDialogComponent } from './user-edit-dialog/user-edit-dialog.component';
import { EditUserRolesComponent } from './edit-user-roles/edit-user-roles.component';
import { EditUserNotfyGroupsComponent } from './edit-user-notfy-groups/edit-user-notfy-groups.component';





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
    UserEditDialogComponent,
    EditUserRolesComponent,
    EditUserNotfyGroupsComponent,

   
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
    UserEditDialogComponent,
    EditUserNotfyGroupsComponent

  
  ],

})
export class UsersModule { }
