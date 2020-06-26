import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RolesListCardComponent } from './roles-list-card/roles-list-card.component';
import { EditRolesDialogComponent } from './edit-roles-dialog/edit-roles-dialog.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { Routes, RouterModule } from '@angular/router';
import { RolesComponent } from './roles.component';

export const routes:Routes=[
  {
    path:"",
    component:RolesComponent
  }
]


@NgModule({
  declarations: [
    RolesListCardComponent, 
    EditRolesDialogComponent,
    RolesComponent
  
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  exports:[
    RouterModule,
    RolesComponent,
    RolesListCardComponent,
    EditRolesDialogComponent
   
  ]
})
export class RolesModule { }
