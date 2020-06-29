import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { AuthGuard } from 'src/app/core/guards/auth-guard';

const routes:Routes=[
  {
    path:"",
    component:DashboardComponent,
    runGuardsAndResolvers:"always",
    canActivate:[AuthGuard],
    children:[
        {
          path:"users",
          loadChildren:()=>import("../users/users.module").then(m=>m.UsersModule),
          data:{roles:['Sudo']}
        },
        {
          path:"roles",
          loadChildren:()=>import("../roles/roles.module").then(m=>m.RolesModule)
        },
        {
          path:"roles-category",
          loadChildren:()=>import("../role-category/role-category.module").then(m=>m.RoleCategoryModule)
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
