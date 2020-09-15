import { Component, OnInit } from '@angular/core';
import { AnnounceContentTypeStore } from 'src/app/core/services/stores/announce-content-type-store';
import { MatDialog } from '@angular/material/dialog';
import { EditAnnounceContentTypeDialogComponent } from './edit-announce-content-type-dialog/edit-announce-content-type-dialog.component';

@Component({
  selector: 'app-announce-content-types',
  templateUrl: './announce-content-types.component.html',
  styleUrls: ['./announce-content-types.component.scss']
})
export class AnnounceContentTypesComponent implements OnInit {
  toolbarTitle:string="Bulunduğu Kat";
  allowedRoles:string[]=["Sudo","AnnounceOptons.All"];
  constructor(
    public announcecontentTypeStore:AnnounceContentTypeStore,
    private dialog:MatDialog
  ) { }

  ngOnInit(): void {
  }

  onCreate(){
     this.dialog.open(EditAnnounceContentTypeDialogComponent,{
       width:"45rem",
       maxHeight:"100vh",
       data:{
         title:"Yeni Opsiyon Ekle",
         mode:"create",
         item:null
       }
     }) 
  }

}
