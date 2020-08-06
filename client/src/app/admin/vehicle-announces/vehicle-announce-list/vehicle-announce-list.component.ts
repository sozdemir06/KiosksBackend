import { Component, OnInit, Input } from '@angular/core';
import { IVehicleAnnounceList } from 'src/app/shared/models/IVehicleAnnounceList';
import { MatDialog } from '@angular/material/dialog';
import { EditVehicleAnnounceDialogComponent } from '../edit-vehicle-announce-dialog/edit-vehicle-announce-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { VehilceAnnounceStore } from 'src/app/core/services/stores/vehicle-announce-store';

@Component({
  selector: 'app-vehicle-announce-list',
  templateUrl: './vehicle-announce-list.component.html',
  styleUrls: ['./vehicle-announce-list.component.scss']
})
export class VehicleAnnounceListComponent implements OnInit {
@Input() dataSource:IVehicleAnnounceList[];
displayedColumns:string[]=['Image','Header','Created','PublishDates','Price','PublishStatus','Actions'];

roleForUpdate:string[]=["Sudo","VehicleAnnounces.Update","VehicleAnnounces.All"]
roleForPublish:string[]=["Sudo","VehicleAnnounces.Publish","VehicleAnnounces.All"]
  constructor(
    private dialog:MatDialog,
    private vehicleStore:VehilceAnnounceStore
  ) { }

  ngOnInit(): void {
  }

  onUpdate(element:IVehicleAnnounceList){
    this.dialog.open(EditVehicleAnnounceDialogComponent,{
      width:"55rem",
      maxHeight:"100vh",
      autoFocus:false,
      data:{
        title:"Araç ilanını güncelle",
        mode:"update",
        item:element
      }
    })    
  }

  onPublish(announce:IVehicleAnnounceList){
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'İlanı yayınlamak  istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: IVehicleAnnounceList = {
          ...announce,
          isNew: false,
          reject: false,
          isPublish: true,
        };
        this.vehicleStore.publish(model);
      }
    });
  }
  unPublish(announce:IVehicleAnnounceList){
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'İlanı yayından kaldırmak istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: IVehicleAnnounceList = {
          ...announce,
          isNew: false,
          reject: false,
          isPublish: false
        };
        this.vehicleStore.publish(model);
      }
    });
  }

  onReject(announce:IVehicleAnnounceList){
     const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'İlanı Reddetmek istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: IVehicleAnnounceList = {
          ...announce,
          isNew: false,
          reject: true,
          isPublish: false
        };
        this.vehicleStore.publish(model);
      }
    });
  }

  onDelete(element:IVehicleAnnounceList){
    console.log("deleted");
  }

}
