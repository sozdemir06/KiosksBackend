import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { LayoutComponent } from './layout.component';
import { Routes, RouterModule } from '@angular/router';
import { MaterialModule } from '../material/material.module';

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
        }
       

     ]
  }
]

@NgModule({
  declarations: [
    HeaderComponent, 
    FooterComponent,
    LayoutComponent
  
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MaterialModule
  ],

  exports:[
    HeaderComponent,
    FooterComponent,
    LayoutComponent,
    RouterModule,
    MaterialModule
  ]
})
export class LayoutModule { }
