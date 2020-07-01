import { Component, OnInit } from '@angular/core';
import { BuildingAgeStore } from 'src/app/core/services/stores/building-age-store';
import { MatDialog } from '@angular/material/dialog';
import { EditBuildingAgeDialogComponent } from './edit-building-age-dialog/edit-building-age-dialog.component';

@Component({
  selector: 'app-building-age',
  templateUrl: './building-age.component.html',
  styleUrls: ['./building-age.component.scss']
})
export class BuildingAgeComponent implements OnInit {
  toolbarTitle:string="Binanın Yaşı";
  toolbarSearchInputPlaceholder:string="Arama kapalı";
  allowedRoles:string[]=["Sudo","BuildingAge.Create"];

  constructor(
    public buildingAgeStore:BuildingAgeStore,
    private dialog:MatDialog
  ) { }

  ngOnInit(): void {
  }

  onCreate(){
    this.dialog.open(EditBuildingAgeDialogComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Yeni Opsiyon ekle",
        mode:"create",
        item:null
      }
    })
  }

}
