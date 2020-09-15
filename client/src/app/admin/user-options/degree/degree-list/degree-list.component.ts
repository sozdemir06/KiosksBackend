import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DegreeStore } from 'src/app/core/services/stores/degree-store';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { IDegree } from 'src/app/shared/models/IDegree';
import { EditDegreeDialogComponent } from '../edit-degree-dialog/edit-degree-dialog.component';

@Component({
  selector: 'app-degree-list',
  templateUrl: './degree-list.component.html',
  styleUrls: ['./degree-list.component.scss']
})
export class DegreeListComponent implements OnInit {
  displayedColumns:string[]=["Id","Name","Actions"];
  @Input() dataSource:IDegree[];
  allowedRoles:string[]=["Sudo","UserOptions.All"];

    constructor(
      private dialog:MatDialog,
      private degreeStore:DegreeStore
    ) { }
  
    ngOnInit(): void {
     
    }
  
    onUpdate(element:IDegree){
      this.dialog.open(EditDegreeDialogComponent,{
        width:"45rem",
        maxHeight:"100vh",
        data:{
          title:"Ünvanı Güncelle",
          mode:"update",
          item:element
        }
      })
    }
  
    onDelete(element:IDegree){
       const dialogRef=this.dialog.open(ConfirmDialogComponent,{
         width:"45rem"
       });
       dialogRef.afterClosed().subscribe(result=>{
         if(result){
            this.degreeStore.delete(element);
         }
       })
    }
}
