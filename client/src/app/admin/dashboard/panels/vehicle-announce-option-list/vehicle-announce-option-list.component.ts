import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-announce-option-list',
  templateUrl: './vehicle-announce-option-list.component.html',
  styleUrls: ['./vehicle-announce-option-list.component.scss']
})
export class VehicleAnnounceOptionListComponent implements OnInit {
alloweRolesVehicleCategoriesForList:string[]=['Sudo','VehicleCategories.List'];
alloweRolesVehicleBrandsForList:string[]=['Sudo','VehicleCategories.List'];
alloweRolesVehicleModelsForList:string[]=['Sudo','VehicleCategories.List'];
  constructor() { }

  ngOnInit(): void {
  }

}
