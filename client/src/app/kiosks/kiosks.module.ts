import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { KiosksComponent } from './kiosks.component';
import { Routes, RouterModule } from '@angular/router';


export const routes:Routes=[
  {
    path:"",
    component:KiosksComponent,
    
  }
]

@NgModule({
  declarations: [KiosksComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports:[
    KiosksComponent,
    RouterModule
  ]
})
export class KiosksModule { }
