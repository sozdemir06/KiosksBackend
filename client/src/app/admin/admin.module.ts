import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AdminRoutingModule } from './admin-routing/admin-routing.module';
import { MaterialModule } from '../material/material.module';
import { UserPanelComponent } from './dashboard/panels/user-panel/user-panel.component';
import {LayoutModule} from '@angular/cdk/layout';


@NgModule({
  declarations: [
    DashboardComponent,
    UserPanelComponent,
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    MaterialModule,
    LayoutModule
  ],

  exports:[
    AdminRoutingModule,
    LayoutModule
  ]
})
export class AdminModule { }
