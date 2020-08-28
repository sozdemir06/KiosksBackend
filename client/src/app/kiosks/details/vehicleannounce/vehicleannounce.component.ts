import { Component, OnInit, Input } from '@angular/core';
import { IVehicleAnnounceForKiosks } from '../../models/IVehicleAnnounceForKiosks';

@Component({
  selector: 'app-vehicleannounce',
  templateUrl: './vehicleannounce.component.html',
  styleUrls: ['./vehicleannounce.component.scss']
})
export class VehicleannounceComponent implements OnInit {
  @Input() vehicleAnnounce:IVehicleAnnounceForKiosks;
  constructor() { }

  ngOnInit(): void {
  }

}
