import { NgModule } from '@angular/core';
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
import { VehicleAnnounceOptionListComponent } from '../admin/dashboard/panels/vehicle-announce-option-list/vehicle-announce-option-list.component';
import { ScreenPanelComponent } from '../admin/dashboard/panels/screen-panel/screen-panel.component';


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
    HomeAnnounceOptionListComponent,
    VehicleAnnounceOptionListComponent,
    ScreenPanelComponent

  
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes),
  ],

  exports:[
    HeaderComponent,
    FooterComponent,
    LayoutComponent,
    RouterModule,
    UserPanelComponent,
    RolePanelComponent,
    HomeAnnounceOptionListComponent,
    VehicleAnnounceOptionListComponent,
    ScreenPanelComponent
  ]
})
export class LayoutModule { }
