import { Component, OnInit, Input } from '@angular/core';
import { IVehicleBrand } from 'src/app/shared/models/IVehicleBrand';
import { MatDialog } from '@angular/material/dialog';
import { EditVehicleBrandsDialogComponent } from '../edit-vehicle-brands-dialog/edit-vehicle-brands-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { VehicleBrandStore } from 'src/app/core/services/stores/vehicle-brand-store';

@Component({
  selector: 'app-vehicle-brands-list',
  templateUrl: './vehicle-brands-list.component.html',
  styleUrls: ['./vehicle-brands-list.component.scss']
})
export class VehicleBrandsListComponent implements OnInit {
displayedColumns:string[]=["Id","BrandName","Actions"];
@Input() dataSource:IVehicleBrand[];
allowedRoles:string[]=["Sudo","VehicleAnnounceOptions.All"];

  constructor(
    private dialog:MatDialog,
    private vehicleBrandStore:VehicleBrandStore
  ) { }

  ngOnInit(): void {
   
  }

  onUpdate(element:IVehicleBrand){
    this.dialog.open(EditVehicleBrandsDialogComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Araç markasını güncelle",
        mode:"update",
        item:element
      }
    })
  }

  onDelete(element:IVehicleBrand){
     const dialogRef=this.dialog.open(ConfirmDialogComponent,{
       width:"45rem"
     });
     dialogRef.afterClosed().subscribe(result=>{
       if(result){
          this.vehicleBrandStore.delete(element);
       }
     })
  }

}
