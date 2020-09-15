import { Component, OnInit, Input } from '@angular/core';
import { IVehicleModel } from 'src/app/shared/models/IVehicleModel';
import { VehicleModelStore } from 'src/app/core/services/stores/vehicle-model-store';
import { MatDialog } from '@angular/material/dialog';
import { EditVehicleModelDialogComponent } from '../edit-vehicle-model-dialog/edit-vehicle-model-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-vehicle-model-list',
  templateUrl: './vehicle-model-list.component.html',
  styleUrls: ['./vehicle-model-list.component.scss'],
})
export class VehicleModelListComponent implements OnInit {
  displayedColumns: string[] = ['Id', 'VehicleModelName','BrandName',"CategoryName", 'Actions'];
  @Input() dataSource: IVehicleModel[];
  allowedRoles:string[]=["Sudo","VehicleAnnounceOptions.All"];



  constructor(
    private dialog:MatDialog,
    private vehicleModelStore:VehicleModelStore
  ) {}

  ngOnInit(): void {}

  onUpdate(element: IVehicleModel) {
    this.dialog.open(EditVehicleModelDialogComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Araç model güncelle",
        mode:"update",
        item:element
      }
    })
  }

  onDelete(element: IVehicleModel) {
    const dialogRef=this.dialog.open(ConfirmDialogComponent,{
      width:"45rem"
    });
    dialogRef.afterClosed().subscribe(result=>{
      if(result){
        this.vehicleModelStore.delete(element);
      }
    })
  }
}
