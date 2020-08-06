import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-announces-panel',
  templateUrl: './vehicle-announces-panel.component.html',
  styleUrls: ['./vehicle-announces-panel.component.scss']
})
export class VehicleAnnouncesPanelComponent implements OnInit {
  allowedRolesForList:string[]=['Sudo','VehicleAnnounces.List','VehicleAnnounces.All']
  constructor() { }

  ngOnInit(): void {
  }

}
