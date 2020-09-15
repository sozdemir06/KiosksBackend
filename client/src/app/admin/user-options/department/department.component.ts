import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DepartmentStore } from 'src/app/core/services/stores/department-store';
import { EditDepartmentDialogComponent } from './edit-department-dialog/edit-department-dialog.component';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.scss']
})
export class DepartmentComponent implements OnInit {
  toolbarTitle:string="Birim Listesi";
  toolbarSearchInputPlaceholder:string="Arama kapalı";
  allowedRoles:string[]=["Sudo","UserOptions.All"];

  constructor(
    public departmentStore:DepartmentStore,
    private dialog:MatDialog
  ) { }

  ngOnInit(): void {
  }

  onCreate(){
    this.dialog.open(EditDepartmentDialogComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Yeni Birim Ekle",
        mode:"create",
        item:null
      }
    })
  }


}
