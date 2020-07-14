import { Component, OnInit } from '@angular/core';
import { ScreenStore } from 'src/app/core/services/stores/screen-store';
import { IScreen } from 'src/app/shared/models/IScreen';
import { MatDialog } from '@angular/material/dialog';
import { EditScreensDialogComponent } from '../edit-screens-dialog/edit-screens-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';


@Component({
  selector: 'app-screens-list',
  templateUrl: './screens-list.component.html',
  styleUrls: ['./screens-list.component.scss']
})
export class ScreensListComponent implements OnInit {
  panelOpenState:boolean=false;
  displayedColumns:string[]=["Id","Name","Position","IsFull","Actions"];
  allowedRoleForUpdate:string[]=["Sudo","Screens.Update"];
  allowedRoleForDelete:string[]=["Sudo","Screens.Delete"];
 allowedroleForSubScreenList:string[]=['Sudo','SubScreens.List'];
  constructor(
    public screenStore:ScreenStore,
    private dialog:MatDialog
  ) { }

  ngOnInit(): void {
  }

  onUpdate(element:IScreen){
     this.dialog.open(EditScreensDialogComponent,{
       width:"45rem",
       maxHeight:"100vh",
       data:{
         title:"Ekran bilgilerini güncelle",
         mode:"update",
         item:element
       }
     }) 
  }

  onDelete(element:IScreen){
      const dialogRef=this.dialog.open(ConfirmDialogComponent,{
        width:"45rem"
      });

      dialogRef.afterClosed().subscribe(result=>{
        if(result){
          this.screenStore.delete(element);
        }
      })
  }

}
