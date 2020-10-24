import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { KiosksHubService } from 'src/app/kiosks/store/kiosks-hub';
import { EditScreensDialogComponent } from './edit-screens-dialog/edit-screens-dialog.component';

@Component({
  selector: 'app-screens',
  templateUrl: './screens.component.html',
  styleUrls: ['./screens.component.scss']
})
export class ScreensComponent implements OnInit,OnDestroy {
toolbarTitle:string="Ekranlar";
allowedRoles:string[]=['Sudo','Screens.Create','Screens.All'];

  constructor(
    private dialog:MatDialog,
    private kiosksHub:KiosksHubService
  ) { }

  ngOnInit(): void {
     this.kiosksHub.createHubConnection(); 
  }


  onCreate(){
    this.dialog.open(EditScreensDialogComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Yeni ekran ekle",
        mode:"create",
        item:null
      }
    })
  }

  ngOnDestroy(){
    this.kiosksHub.stopHubConnection();
  }


}
