import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EditVehicleEngineSizeDialogComponent } from './edit-vehicle-engine-size-dialog/edit-vehicle-engine-size-dialog.component';
import { VehicleEngineSizeStore } from 'src/app/core/services/stores/vehicle-engine-size-store';

@Component({
  selector: 'app-vehicle-engine-size',
  templateUrl: './vehicle-engine-size.component.html',
  styleUrls: ['./vehicle-engine-size.component.scss']
})
export class VehicleEngineSizeComponent implements OnInit {
toolbarTitle:string="Motor Hacmi";
allowedRolesForCreate:string[]=['Sudo','VehicleEngineSizes.Create'];

  constructor(
    private dialog:MatDialog,
    public vehicleEngineSizeStore:VehicleEngineSizeStore
  ) { }

  ngOnInit(): void {
  }


  onCreate(){
    this.dialog.open(EditVehicleEngineSizeDialogComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Vites tipi opsiyon ekle",
        mode:'create',
        item:null
      }
    })
  }


}
