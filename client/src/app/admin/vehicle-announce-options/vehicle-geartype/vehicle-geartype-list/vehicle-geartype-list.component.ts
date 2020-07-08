import { Component, OnInit, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { IVehicleGearType } from 'src/app/shared/models/IVehicleGearType';
import { VehicleGearTypeStore } from 'src/app/core/services/stores/vehicle-gear-type-store';
import { EditVehicleGeartypeDialogComponent } from '../edit-vehicle-geartype-dialog/edit-vehicle-geartype-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-vehicle-geartype-list',
  templateUrl: './vehicle-geartype-list.component.html',
  styleUrls: ['./vehicle-geartype-list.component.scss']
})
export class VehicleGeartypeListComponent implements OnInit {

  displayedColumns:string[]=['Id','FuelTypeName','Actions'];

@Input() dataSource:IVehicleGearType[];
allowedRolesVehicleGearTypeForUpdate:string[]=['Sudo','VehicleGearTypes.Update'];
allowedRolesVehicleGearTypeForDelete:string[]=['Sudo','VehicleGearTypes.Delete'];

  constructor(
    private dialog:MatDialog,
    private vehicleFuelTypeStore:VehicleGearTypeStore

  ) { }

  ngOnInit(): void {
  }

  onUpdate(element:IVehicleGearType){
    this.dialog.open(EditVehicleGeartypeDialogComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Yakıt tipi opsiyon güncelle",
        mode:'update',
        item:element
      }
    })
  }

  onDelete(element:IVehicleGearType){
    const dialogRef=this.dialog.open(ConfirmDialogComponent,{
      width:"45rem"
    });
    dialogRef.afterClosed().subscribe(result=>{
      if(result){
        this.vehicleFuelTypeStore.delete(element);
      }
    })
  }
}
