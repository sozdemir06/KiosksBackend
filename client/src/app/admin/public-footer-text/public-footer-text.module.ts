import { NgModule } from '@angular/core';
import { PublicFooterTextComponent } from './public-footer-text.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { RouterModule, Routes } from '@angular/router';

export const routes:Routes=[
  {
    path:"",
    component:PublicFooterTextComponent
  }
]


@NgModule({
  declarations: [PublicFooterTextComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  exports:[
    PublicFooterTextComponent,
    RouterModule
  ]
})
export class PublicFooterTextModule { }
