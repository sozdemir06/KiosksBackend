import { Component, OnInit} from '@angular/core';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { Observable } from 'rxjs';
import { SubScreenStore } from 'src/app/core/services/stores/subscreen-store';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { EditSubscreensDialogComponent } from '../edit-subscreens-dialog/edit-subscreens-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-subscreens-list',
  templateUrl: './subscreens-list.component.html',
  styleUrls: ['./subscreens-list.component.scss']
})
export class SubscreensListComponent implements OnInit {

  displayedColumns:string[]=["Id","Name","Position","Width","Height","Status","Actions"];
  allowedRoleForUpdate:string[]=["Sudo","SubScreens.Update"];
  allowedRoleForDelete:string[]=["Sudo","SubScreens.Delete"];
  subScreens$:Observable<ISubScreen[]>;
  screenId:number;
  constructor(
    private route:ActivatedRoute,
    public subScreensStore:SubScreenStore,
    private dialog:MatDialog
  ) { }

  ngOnInit(): void {
    this.screenId=+this.route.snapshot.paramMap.get("id");
    this.subScreensStore.getByScreenId(this.screenId); 
  }

  onUpdate(element:ISubScreen){
    const dialogRef=  this.dialog.open(EditSubscreensDialogComponent,{
        width:"45rem",
        maxHeight:"100vh",
        data:{
          title:"Alt Ekranı Güncelle",
          mode:"update",
          item:element,
          screenId:this.screenId
        }
      });

     
  }

  onDelete(element:ISubScreen){
  const dialogRef=this.dialog.open(ConfirmDialogComponent,{
       width:"45rem",
     });
     dialogRef.afterClosed().subscribe(result=>{
       if(result){
         this.subScreensStore.delete(element);
         
       }
     }) 
  }

  

}
