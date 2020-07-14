import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EditScreensDialogComponent } from './edit-screens-dialog/edit-screens-dialog.component';

@Component({
  selector: 'app-screens',
  templateUrl: './screens.component.html',
  styleUrls: ['./screens.component.scss']
})
export class ScreensComponent implements OnInit {
toolbarTitle:string="Ekranlar";
allowedRoles:string[]=['Sudo','Screens.Create'];

  constructor(
    private dialog:MatDialog
  ) { }

  ngOnInit(): void {
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

}
