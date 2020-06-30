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
          loadChildren:()=>import("../roles/roles.module").then(m=>m.RolesModule),
          data:{roles:['Sudo']}
        },
        {
          path:"roles-category",
          loadChildren:()=>import("../role-category/role-category.module").then(m=>m.RoleCategoryModule),
          data:{roles:['Sudo']}
        },
        {
          path:"number-of-rooms",
          loadChildren:()=>import("../home-announce-options/number-of-room/number-of-room.module").then(m=>m.NumberOfRoomModule),
          data:{roles:['Sudo']}
        },
        {
          path:"buildings-age",
          loadChildren:()=>import("../home-announce-options/building-age/building-age.module").then(m=>m.BuildingAgeModule),
          data:{roles:['Sudo']}
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
