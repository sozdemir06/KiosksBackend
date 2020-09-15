import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BuildingAgeStore } from 'src/app/core/services/stores/building-age-store';
import { CampusStore } from 'src/app/core/services/stores/campus-store';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { ICampus } from 'src/app/shared/models/ICampus';
import { EditCampusDialogComponent } from '../edit-campus-dialog/edit-campus-dialog.component';

@Component({
  selector: 'app-campus-list',
  templateUrl: './campus-list.component.html',
  styleUrls: ['./campus-list.component.scss']
})
export class CampusListComponent implements OnInit {
  displayedColumns: string[] = ["Id",'Name',"Actions"];
  @Input() dataSource:ICampus[];
  allowedRoles:string[]=['Sudo','UserOptions.All'];


  constructor(
    private dialog:MatDialog,
    private campusStore:CampusStore
  ) { }

  ngOnInit(): void {
  }

  onUpdate(element:ICampus){
      this.dialog.open(EditCampusDialogComponent,{
        width:"45rem",
        maxHeight:"100vh",
        data:{
          title:"Opsiyon Güncelle",
          mode:"update",
          item:element
        }
      });
  }

  onDelete(element:ICampus){
      const dialogRef=this.dialog.open(ConfirmDialogComponent,{
        width:"45rem"
      })

      dialogRef.afterClosed().subscribe(result=>{
        if(result){
           this.campusStore.delete(element);

        }
      })
  }

}
