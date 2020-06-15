import { NgModule } from '@angular/core';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AdminRoutingModule } from './admin-routing/admin-routing.module';
import { UserPanelComponent } from './dashboard/panels/user-panel/user-panel.component';
import {LayoutModule} from '@angular/cdk/layout';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    DashboardComponent,
    UserPanelComponent,
  ],
  imports: [
    SharedModule,
    AdminRoutingModule,
    LayoutModule
  ],

  exports:[
    AdminRoutingModule,
    LayoutModule,
  ]
})
export class AdminModule { }
