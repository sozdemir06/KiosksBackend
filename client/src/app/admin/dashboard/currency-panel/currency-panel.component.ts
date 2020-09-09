import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-currency-panel',
  templateUrl: './currency-panel.component.html',
  styleUrls: ['./currency-panel.component.scss']
})
export class CurrencyPanelComponent implements OnInit {
  allowedRolesForList:string[]=['Sudo','Currencies.List','Currencies.All']
  constructor() { }

  ngOnInit(): void {
  }

}
