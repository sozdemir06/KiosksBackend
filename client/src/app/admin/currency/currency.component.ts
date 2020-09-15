import { Component, OnInit } from '@angular/core';
import { CurrencyStore } from 'src/app/core/services/stores/currency-store';

@Component({
  selector: 'app-currency',
  templateUrl: './currency.component.html',
  styleUrls: ['./currency.component.scss']
})
export class CurrencyComponent implements OnInit {
  toolbarTitle:string="Para Listesi"
  toolbarSearchInputPlaceholder:string="Para adına göre ara..";
  allowedRoles:string[]=["Sudo","AddMoneyForExchangeRate"];
  
  constructor(
    public currencyStore$:CurrencyStore
  ) { }

  ngOnInit(): void {
  }

}
