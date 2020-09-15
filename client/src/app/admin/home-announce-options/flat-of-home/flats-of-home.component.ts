import { Component, OnInit } from '@angular/core';
import { FlatOfHomeStore } from 'src/app/core/services/stores/flat-of-home-store';
import { MatDialog } from '@angular/material/dialog';
import { EditFlatsOfHomeDialogComponent } from './edit-flats-of-home-dialog/edit-flats-of-home-dialog.component';

@Component({
  selector: 'app-flats-of-home',
  templateUrl: './flats-of-home.component.html',
  styleUrls: ['./flats-of-home.component.scss']
})
export class FlatsOfHomeComponent implements OnInit {
toolbarTitle:string="Bulunduğu Kat";
allowedRoles:string[]=["Sudo","HomeAnnounceOptions.All"];

  constructor(
    public flatOfHomeStore:FlatOfHomeStore,
    private dialog:MatDialog
  ) { }

  ngOnInit(): void {
  }

  onCreate(){
      this.dialog.open(EditFlatsOfHomeDialogComponent,{
        width:"45rem",
        maxHeight:"100vh",
        data:{
          title:"Yeni opsiyon ekle",
          mode:"create",
          item:null
        }
      })
  }

}
