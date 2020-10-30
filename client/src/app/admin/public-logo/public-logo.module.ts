import { NgModule } from '@angular/core';
import { PublicLogoComponent } from './public-logo.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { RouterModule, Routes } from '@angular/router';

export const routes:Routes=[
  {
    path:"",
    component:PublicLogoComponent
  }
]


@NgModule({
  declarations: [PublicLogoComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  exports:[
    PublicLogoComponent,
    RouterModule
  ]
})
export class PublicLogoModule { }
