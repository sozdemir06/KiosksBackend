import { NgModule } from '@angular/core';
import { CityComponent } from './city.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { Routes, RouterModule } from '@angular/router';
import { CityListComponent } from './city-list/city-list.component';

export const routes:Routes=[
  {
    path:"",
    component:CityComponent
  }
]

@NgModule({
  declarations: [CityComponent, CityListComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  exports:[
    CityComponent,
    RouterModule
  ]
})
export class CityModule { }
