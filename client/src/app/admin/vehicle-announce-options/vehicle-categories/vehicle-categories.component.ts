import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EditVehicleCategoryDialogComponent } from './edit-vehicle-category-dialog/edit-vehicle-category-dialog.component';
import { VehicleCategoryStore } from 'src/app/core/services/stores/vehicle-category-store';

@Component({
  selector: 'app-vehicle-categories',
  templateUrl: './vehicle-categories.component.html',
  styleUrls: ['./vehicle-categories.component.scss']
})
export class VehicleCategoriesComponent implements OnInit {
toolbarTitle:string="Araç Kategori";
allowedRoles:string[]=["Sudo","VehicleAnnounceOptions.All"];

  constructor(
    private dialog:MatDialog,
    public vehicleCategoryStore:VehicleCategoryStore
  ) { }

  ngOnInit(): void {
  }

  onCreate(){
    this.dialog.open(EditVehicleCategoryDialogComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Yeni Kategori Ekle",
        mode:"create",
        item:null
      }
    })
  }

}
