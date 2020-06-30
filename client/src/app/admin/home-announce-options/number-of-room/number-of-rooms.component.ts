import { Component, OnInit } from '@angular/core';
import { NumberOfroomStore } from 'src/app/core/services/stores/number-of-rrom-store';
import { MatDialog } from '@angular/material/dialog';
import { EditNumberOfRoomsDialogComponent } from './edit-number-of-rooms-dialog/edit-number-of-rooms-dialog.component';

@Component({
  selector: 'app-number-of-rooms',
  templateUrl: './number-of-rooms.component.html',
  styleUrls: ['./number-of-rooms.component.scss']
})
export class NumberOfRoomsComponent implements OnInit {
  toolbarTitle:string="Oda Sayısı Listesi";
  toolbarSearchInputPlaceholder:string="Arama kapalı";
  allowedRoles:string[]=["Sudo"];

  constructor(
    public numberOfRoomStore:NumberOfroomStore,
    private dialog:MatDialog
  ) { }

  ngOnInit(): void {
  }

  onCreate(){
     this.dialog.open(EditNumberOfRoomsDialogComponent,{
       width:"45rem",
       maxHeight:"100vh",
       data:{
         title:"Yeni Ekle",
         mode:"create",
         item:null
       }
     })
  }

}
