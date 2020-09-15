import { Component, OnInit, Input } from '@angular/core';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { IVehicleAnnounceSubScreen } from 'src/app/shared/models/IVehicleAnnounceSubScreen';
import { MatDialog } from '@angular/material/dialog';
import { VehilceAnnounceStore } from 'src/app/core/services/stores/vehicle-announce-store';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-vehicle-announce-subscreens',
  templateUrl: './vehicle-announce-subscreens.component.html',
  styleUrls: ['./vehicle-announce-subscreens.component.scss']
})
export class VehicleAnnounceSubscreensComponent implements OnInit {
  @Input() subscreens:IVehicleAnnounceSubScreen[];
  displayedColumns:string[]=["Name","Actions"];
  roleForRemove:string[]=['Sudo','VehicleAnnounces.Delete','VehicleAnnounces.All']
  constructor(
    private dialog:MatDialog,
    private vehicleStore:VehilceAnnounceStore
  ) { }

  ngOnInit(): void {
  }

  onDelete(element:IVehicleAnnounceSubScreen){
    const dialogRef=this.dialog.open(ConfirmDialogComponent,{
      width:"45rem",
      data:{
        message:`Araç ilanını ${element.subScreenName} adlı ekrandan kaldırmak istoyormusunuz?`
      }
    });
    dialogRef.afterClosed().subscribe(result=>{
      if(result){
        this.vehicleStore.removeSubScreen(element);
      }
    })
  }

}
