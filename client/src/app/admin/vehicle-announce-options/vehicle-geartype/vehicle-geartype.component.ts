import { Component, OnInit } from '@angular/core';
import { VehicleGearTypeStore } from 'src/app/core/services/stores/vehicle-gear-type-store';
import { MatDialog } from '@angular/material/dialog';
import { EditVehicleGeartypeDialogComponent } from './edit-vehicle-geartype-dialog/edit-vehicle-geartype-dialog.component';

@Component({
  selector: 'app-vehicle-geartype',
  templateUrl: './vehicle-geartype.component.html',
  styleUrls: ['./vehicle-geartype.component.scss']
})
export class VehicleGeartypeComponent implements OnInit {
  toolbarTitle:string="Yakıt Tipi";
  allowedRoles:string[]=["Sudo","VehicleAnnounceOptions.All"];
    constructor(
      public vehicleGearTypeStore:VehicleGearTypeStore,
      private dialog:MatDialog
    ) { }
  
    ngOnInit(): void {
    }
  
    onCreate(){
      this.dialog.open(EditVehicleGeartypeDialogComponent,{
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
