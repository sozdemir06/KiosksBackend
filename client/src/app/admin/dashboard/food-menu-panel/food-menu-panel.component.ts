import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-food-menu-panel',
  templateUrl: './food-menu-panel.component.html',
  styleUrls: ['./food-menu-panel.component.scss']
})
export class FoodMenuPanelComponent implements OnInit {
  allowedRolesForList:string[]=['Sudo','FoodMenu.List','FoodMenu.All']
  constructor() { }

  ngOnInit(): void {
  }

}
