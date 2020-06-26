import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RolesCategoriesComponent } from './roles-categories.component';
import { RoleCategoryListCardComponent } from './role-category-list-card/role-category-list-card.component';
import { EditRoleCategoryDialogComponent } from './edit-role-category-dialog/edit-role-category-dialog.component';
import { Route } from '@angular/compiler/src/core';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';

export const routes:Routes=[
  {
    path:"",
    component:RolesCategoriesComponent
  }
]

@NgModule({
  declarations: [
    RolesCategoriesComponent,
    RoleCategoryListCardComponent,
    EditRoleCategoryDialogComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  
  ],

  exports:[
    RouterModule,
    RolesCategoriesComponent,
    RoleCategoryListCardComponent,
    EditRoleCategoryDialogComponent,
  ]
})
export class RoleCategoryModule {}
