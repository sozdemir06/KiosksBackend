import { Component, OnInit, Input } from '@angular/core';
import { IRole } from 'src/app/shared/models/IRole';
import { MatDialog } from '@angular/material/dialog';
import { EditRolesDialogComponent } from '../edit-roles-dialog/edit-roles-dialog.component';

@Component({
  selector: 'app-roles-list-card',
  templateUrl: './roles-list-card.component.html',
  styleUrls: ['./roles-list-card.component.scss']
})
export class RolesListCardComponent implements OnInit {
  displayedColumns: string[] = ['RoleName', 'Description', 'CategoryName',"Actions"];
  @Input() dataSource:IRole[];
  allowedRolesForUpdate:string[]=["Sudo","Roles.Update","Roles.All"];

  constructor(
    private dialog:MatDialog
  ) { }

  ngOnInit(): void {
  }

  onUpdate(element:IRole){
        this.dialog.open(EditRolesDialogComponent,{
          width:"45rem",
          data:{
            mode:"update",
            title:"Yetki Güncelle",
            role:element
          }
        })
  }

}
