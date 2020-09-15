import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DepartmentStore } from 'src/app/core/services/stores/department-store';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { IDepartment } from 'src/app/shared/models/IDepartment';
import { EditDepartmentDialogComponent } from '../edit-department-dialog/edit-department-dialog.component';

@Component({
  selector: 'app-department-list',
  templateUrl: './department-list.component.html',
  styleUrls: ['./department-list.component.scss']
})
export class DepartmentListComponent implements OnInit {
  displayedColumns: string[] = ["Id",'Name',"Actions"];
  @Input() dataSource:IDepartment[];
  allowedRoles:string[]=['Sudo','UserOptions.All'];


  constructor(
    private dialog:MatDialog,
    private departmentStore:DepartmentStore
  ) { }

  ngOnInit(): void {
  }

  onUpdate(element:IDepartment){
      this.dialog.open(EditDepartmentDialogComponent,{
        width:"45rem",
        maxHeight:"100vh",
        data:{
          title:"Birim Güncelle",
          mode:"update",
          item:element
        }
      });
  }

  onDelete(element:IDepartment){
      const dialogRef=this.dialog.open(ConfirmDialogComponent,{
        width:"45rem"
      })

      dialogRef.afterClosed().subscribe(result=>{
        if(result){
           this.departmentStore.delete(element);

        }
      })
  }


}
