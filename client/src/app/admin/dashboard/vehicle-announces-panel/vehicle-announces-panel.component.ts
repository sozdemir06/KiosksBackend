import { Component, OnInit } from '@angular/core';
import { VehilceAnnounceStore } from 'src/app/core/services/stores/vehicle-announce-store';

@Component({
  selector: 'app-vehicle-announces-panel',
  templateUrl: './vehicle-announces-panel.component.html',
  styleUrls: ['./vehicle-announces-panel.component.scss'],
})
export class VehicleAnnouncesPanelComponent implements OnInit {
  allowedRolesForList: string[] = [
    'Sudo',
    'VehicleAnnounces.All',
    'VehicleAnnounces.Create',
    'VehicleAnnounces.Update'
  ];
  constructor(
    public vehicleStore:VehilceAnnounceStore
  ) {}

  ngOnInit(): void {}
}
