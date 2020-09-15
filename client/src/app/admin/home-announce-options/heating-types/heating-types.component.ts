import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EditHeatingTypesDialogComponent } from './edit-heating-types-dialog/edit-heating-types-dialog.component';
import { HeatingTypeStore } from 'src/app/core/services/stores/heating-type-store';

@Component({
  selector: 'app-heating-types',
  templateUrl: './heating-types.component.html',
  styleUrls: ['./heating-types.component.scss']
})
export class HeatingTypesComponent implements OnInit {
toolbarTitle:string="Isıtma Tipi";
allowedRoles:string[]=["Sudo","HomeAnnounceOptions.All"];

  constructor(
    private dialog:MatDialog,
    public heatingTypeStore:HeatingTypeStore
  ) { }

  ngOnInit(): void {
  }

  onCreate(){
    this.dialog.open(EditHeatingTypesDialogComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Yeni opsiyon Ekle",
        mode:"create",
        item:null
      }
    })
  }

}
