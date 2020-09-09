import { NgModule } from '@angular/core';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AdminRoutingModule } from './admin-routing/admin-routing.module';
import { LayoutModule } from '@angular/cdk/layout';
import { SharedModule } from '../shared/shared.module';






@NgModule({
  declarations: [DashboardComponent],
  imports: [SharedModule, AdminRoutingModule, LayoutModule],

  exports: [AdminRoutingModule, LayoutModule],
})
export class AdminModule {}
