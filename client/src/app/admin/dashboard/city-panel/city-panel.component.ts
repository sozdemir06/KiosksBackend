import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-city-panel',
  templateUrl: './city-panel.component.html',
  styleUrls: ['./city-panel.component.scss']
})
export class CityPanelComponent implements OnInit {
  allowedRolesForList:string[]=['Sudo','AddCityForWheatherForeCast']
  constructor() { }

  ngOnInit(): void {
  }

}
