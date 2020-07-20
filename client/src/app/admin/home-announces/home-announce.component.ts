import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EditHomeAnnounceDialogComponent } from './edit-home-announce-dialog/edit-home-announce-dialog.component';
import { HomeAnnounceStore } from 'src/app/core/services/stores/home-announce-store';

@Component({
  selector: 'app-home-announce',
  templateUrl: './home-announce.component.html',
  styleUrls: ['./home-announce.component.scss']
})
export class HomeAnnounceComponent implements OnInit {

  constructor(
    private dialog:MatDialog,
    public homeAnnounceStore:HomeAnnounceStore,
  ) { }

  ngOnInit(): void {
  }

  onCreateNew(){
    this.dialog.open(EditHomeAnnounceDialogComponent,{
      width:"55rem",
      maxHeight:"100vh",
      data:{
        title:"Yeni Ev ilanı Ekle",
        mode:"create",
        item:null
      }
    })
  }

  onReset(){

  }

}
