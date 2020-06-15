import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from '../dashboard/dashboard.component';

const routes:Routes=[
  {
    path:"",
    component:DashboardComponent,
    children:[
        {
          path:"",
          loadChildren:()=>import("../users/users.module").then(m=>m.UsersModule)
        }
    ]
  }
]


@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports:[
    RouterModule
  ]
})
export class AdminRoutingModule { }
