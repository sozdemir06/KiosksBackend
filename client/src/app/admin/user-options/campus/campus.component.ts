import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CampusStore } from 'src/app/core/services/stores/campus-store';
import { EditCampusDialogComponent } from './edit-campus-dialog/edit-campus-dialog.component';

@Component({
  selector: 'app-campus',
  templateUrl: './campus.component.html',
  styleUrls: ['./campus.component.scss']
})
export class CampusComponent implements OnInit {
  toolbarTitle:string="Kampüs Listesi";
  toolbarSearchInputPlaceholder:string="Arama kapalı";
  allowedRoles:string[]=["Sudo","UserOptions.All"];

  constructor(
    public campusStore:CampusStore,
    private dialog:MatDialog
  ) { }

  ngOnInit(): void {
  }

  onCreate(){
    this.dialog.open(EditCampusDialogComponent,{
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
