import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EditSubscreensDialogComponent } from './edit-subscreens-dialog/edit-subscreens-dialog.component';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-subscreens',
  templateUrl: './subscreens.component.html',
  styleUrls: ['./subscreens.component.scss']
})
export class SubscreensComponent implements OnInit {

  toolbarTitle:string="Alt Ekranlar";
  allowedRoles:string[]=['Sudo','SubScreens.Create'];
  screenId:number=0;
  
    constructor(
      private dialog:MatDialog,
      private route:ActivatedRoute

    
    ) { }
  
    ngOnInit(): void {
      const screendId:number=+this.route.snapshot.paramMap.get("id");
      this.screenId=screendId;

    }
  
  
    onCreate(){
      this.dialog.open(EditSubscreensDialogComponent,{
        width:"45rem",
        maxHeight:"100vh",
        data:{
          title:"Yeni alt Ekran Ekle",
          mode:"create",
          screenId:this.screenId,
          item:null
        }
      })
    }

}
