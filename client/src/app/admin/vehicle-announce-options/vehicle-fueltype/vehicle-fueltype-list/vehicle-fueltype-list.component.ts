import { Component, OnInit, Input } from '@angular/core';
import { IVehicleFuelType } from 'src/app/shared/models/IVehicleFuelType';
import { MatDialog } from '@angular/material/dialog';
import { VehicleFuelTypeStore } from 'src/app/core/services/stores/vehicle-fuel-type-store';
import { EditVehicleFueltypeDialogComponent } from '../edit-vehicle-fueltype-dialog/edit-vehicle-fueltype-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-vehicle-fueltype-list',
  templateUrl: './vehicle-fueltype-list.component.html',
  styleUrls: ['./vehicle-fueltype-list.component.scss']
})
export class VehicleFueltypeListComponent implements OnInit {
  displayedColumns:string[]=['Id','FuelTypeName','Actions'];

@Input() dataSource:IVehicleFuelType[];
allowedRolesVehicleCatgeoriesForUpdate:string[]=['Sudo','VehicleFuelTypes.Update'];
allowedRolesVehicleCatgeoriesForDelete:string[]=['Sudo','VehicleFuelTypes.Delete'];

  constructor(
    private dialog:MatDialog,
    private vehicleFuelTypeStore:VehicleFuelTypeStore

  ) { }

  ngOnInit(): void {
  }

  onUpdate(element:IVehicleFuelType){
    this.dialog.open(EditVehicleFueltypeDialogComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Yakıt tipi opsiyon güncelle",
        mode:'update',
        item:element
      }
    })
  }

  onDelete(element:IVehicleFuelType){
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
