import { Component, OnInit, Input } from '@angular/core';
import { IVehicleModel } from 'src/app/shared/models/IVehicleModel';

@Component({
  selector: 'app-vehicle-model-list',
  templateUrl: './vehicle-model-list.component.html',
  styleUrls: ['./vehicle-model-list.component.scss'],
})
export class VehicleModelListComponent implements OnInit {
  displayedColumns: string[] = ['Id', 'VehicleModelName', 'Actions'];
  @Input() dataSource: IVehicleModel[];
  allowedRolesForUpdate: string[] = ['Sudo', 'VehicleModels.Update'];
  allowedRolesForDelete: string[] = ['Sudo', 'VehicleModels.Delete'];
  constructor() {}

  ngOnInit(): void {}

  onUpdate(element: IVehicleModel) {}

  onDelete(element: IVehicleModel) {}
}
