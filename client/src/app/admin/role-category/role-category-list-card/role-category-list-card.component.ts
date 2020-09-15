import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IRoleCategory } from 'src/app/shared/models/IRoleCategory';
import { MatDialog } from '@angular/material/dialog';
import { EditRoleCategoryDialogComponent } from '../edit-role-category-dialog/edit-role-category-dialog.component';
import { IRole } from 'src/app/shared/models/IRole';

@Component({
  selector: 'app-role-category-list-card',
  templateUrl: './role-category-list-card.component.html',
  styleUrls: ['./role-category-list-card.component.scss']
})
export class RoleCategoryListCardComponent implements OnInit {
displayedColumns:string[]=["Name","Description","Actions"];
roleForUpdate:string[]=["Sudo","Roles.Update","Roles.All"]

@Input() dataSource:IRoleCategory[];
@Input() isShowAddBtn:boolean=false;

@Output() assignToUser=new EventEmitter<IRole>()

  constructor(
    private dilaog:MatDialog
  ) { }

  ngOnInit(): void {
  }

  onUpdate(element:IRoleCategory){
    this.dilaog.open(EditRoleCategoryDialogComponent,{
      width:"45rem",
      data:{
        title:"Yetki Katogori Güncelle",
        mode:"update",
        roleCategory:element
      }
    })
  }

  onAssignUser(role:IRole){
    this.assignToUser.emit(role);
  }

}
