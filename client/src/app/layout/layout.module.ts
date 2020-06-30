import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { LayoutComponent } from './layout.component';
import { Routes, RouterModule } from '@angular/router';
import { MaterialModule } from '../material/material.module';
import { NotFoundComponent } from '../shared/not-found/not-found.component';
import { UserPanelComponent } from '../admin/dashboard/panels/user-panel/user-panel.component';
import { RolePanelComponent } from '../admin/dashboard/panels/role-panel/role-panel.component';
import { SharedModule } from '../shared/shared.module';
import { HomeAnnounceOptionListComponent } from '../admin/dashboard/panels/home-announce-option-list/home-announce-option-list.component';


const routes:Routes=[
  {
     path:"",
     component:LayoutComponent,
     children:[
        {
          path:"",
          loadChildren:()=>import("../public/public.module").then(m=>m.PublicModule)
        },

        {
          path:"admin",
          loadChildren:()=>import("../admin/admin.module").then(m=>m.AdminModule)
        },
        {
          path:"auth",
          loadChildren:()=>import("../auth/auth.module").then(m=>m.AuthModule)
        },
        {
          path:"not-found",
          component:NotFoundComponent
        },
        {
          path:"**",
          component:NotFoundComponent
        }

       

     ]
  }
]

@NgModule({
  declarations: [
    HeaderComponent, 
    FooterComponent,
    LayoutComponent,
    UserPanelComponent,
    RolePanelComponent,
    HomeAnnounceOptionListComponent

  
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes),
    MaterialModule
  ],

  exports:[
    HeaderComponent,
    FooterComponent,
    LayoutComponent,
    RouterModule,
    MaterialModule,
    UserPanelComponent,
    RolePanelComponent,
    HomeAnnounceOptionListComponent
  ]
})
export class LayoutModule { }
