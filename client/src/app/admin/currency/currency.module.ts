import { NgModule } from '@angular/core';
import { CurrencyComponent } from './currency.component';
import { CurrencyListComponent } from './currency-list/currency-list.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';

export const routes:Routes=[
  {
    path:"",
    component:CurrencyComponent
  }
]


@NgModule({
  declarations: [CurrencyComponent, CurrencyListComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  exports:[
    CurrencyComponent,
    CurrencyListComponent,
    RouterModule
  ]
})
export class CurrencyModule { }
