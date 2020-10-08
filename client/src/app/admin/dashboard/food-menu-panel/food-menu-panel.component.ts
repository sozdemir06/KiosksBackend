import { Component, OnInit } from '@angular/core';
import { FoodMenuStore } from 'src/app/core/services/stores/food-menu-store';

@Component({
  selector: 'app-food-menu-panel',
  templateUrl: './food-menu-panel.component.html',
  styleUrls: ['./food-menu-panel.component.scss']
})
export class FoodMenuPanelComponent implements OnInit {
  allowedRolesForList:string[]=['Sudo','FoodMenu.List','FoodMenu.All']
  constructor(
    public foodMenuStore:FoodMenuStore
  ) { }

  ngOnInit(): void {
  }

}
