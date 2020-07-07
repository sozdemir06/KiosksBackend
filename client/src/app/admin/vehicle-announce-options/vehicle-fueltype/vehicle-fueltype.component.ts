import { Component, OnInit } from '@angular/core';
import { VehicleFuelTypeStore } from 'src/app/core/services/stores/vehicle-fuel-type-store';
import { MatDialog } from '@angular/material/dialog';
import { EditVehicleFueltypeDialogComponent } from './edit-vehicle-fueltype-dialog/edit-vehicle-fueltype-dialog.component';

@Component({
  selector: 'app-vehicle-fueltype',
  templateUrl: './vehicle-fueltype.component.html',
  styleUrls: ['./vehicle-fueltype.component.scss']
})
export class VehicleFueltypeComponent implements OnInit {
toolbarTitle:string="Yakıt Tipi";
allowedRolesForCreate:string[]=['Sudo','VehicleFuelTypes.Create']
  constructor(
    public vehicleFuelTypeStore:VehicleFuelTypeStore,
    private dialog:MatDialog
  ) { }

  ngOnInit(): void {
  }

  onCreate(){
    this.dialog.open(EditVehicleFueltypeDialogComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Yakıt tipi opsiyon ekle",
        mode:'create',
        item:null
      }
    })
  }

}
