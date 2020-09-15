import { Component, OnInit, Input } from '@angular/core';
import { IVehicleEngineSize } from 'src/app/shared/models/IVehicleEngineSize';
import { VehicleEngineSizeStore } from 'src/app/core/services/stores/vehicle-engine-size-store';
import { MatDialog } from '@angular/material/dialog';
import { EditVehicleEngineSizeDialogComponent } from '../edit-vehicle-engine-size-dialog/edit-vehicle-engine-size-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-vehicle-engine-size-list',
  templateUrl: './vehicle-engine-size-list.component.html',
  styleUrls: ['./vehicle-engine-size-list.component.scss']
})
export class VehicleEngineSizeListComponent implements OnInit {
@Input() dataSource:IVehicleEngineSize[];

displayedColumns:string[]=['Id','Name','Actions'];
allowedRoles:string[]=["Sudo","VehicleAnnounceOptions.All"];


  constructor(
    private dialog:MatDialog,
    private vehicleEngineSizeStore:VehicleEngineSizeStore

  ) { }

  ngOnInit(): void {
  }

  onUpdate(element:IVehicleEngineSize){
    this.dialog.open(EditVehicleEngineSizeDialogComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Yakıt tipi opsiyon güncelle",
        mode:'update',
        item:element
      }
    })
  }

  onDelete(element:IVehicleEngineSize){
    const dialogRef=this.dialog.open(ConfirmDialogComponent,{
      width:"45rem"
    });
    dialogRef.afterClosed().subscribe(result=>{
      if(result){
        this.vehicleEngineSizeStore.delete(element);
      }
    })
  }

}
