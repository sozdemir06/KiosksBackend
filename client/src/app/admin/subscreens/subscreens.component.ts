import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EditSubscreensDialogComponent } from './edit-subscreens-dialog/edit-subscreens-dialog.component';
import { ActivatedRoute } from '@angular/router';
import { NotifyService } from 'src/app/core/services/notify-service';

@Component({
  selector: 'app-subscreens',
  templateUrl: './subscreens.component.html',
  styleUrls: ['./subscreens.component.scss']
})
export class SubscreensComponent implements OnInit {

  toolbarTitle:string="Alt Ekranlar";
  allowedRoles:string[]=['Sudo','SubScreens.Create','SubScreens.All'];
  screenId:number=0;
  
    constructor(
      private dialog:MatDialog,
      private route:ActivatedRoute,
      private notifyService:NotifyService

    
    ) { }
  
    ngOnInit(): void {
      const screendId:number=+this.route.snapshot.paramMap.get("id");
      this.screenId=screendId;

    }
  
  
    onCreate(){
      this.notifyService.notify("warning","Ana Ekran Eklendiğinde alt ekranlar otomatik oluşturulur...");
      // this.dialog.open(EditSubscreensDialogComponent,{
      //   width:"45rem",
      //   maxHeight:"100vh",
      //   data:{
      //     title:"Yeni alt Ekran Ekle",
      //     mode:"create",
      //     screenId:this.screenId,
      //     item:null
      //   }
      // })
    }

}
