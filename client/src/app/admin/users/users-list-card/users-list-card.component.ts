import { Component, OnInit, Input } from '@angular/core';
import { IUserList } from 'src/app/shared/models/IUser';

import { MatDialog } from '@angular/material/dialog';
import { UserEditDialogComponent } from '../user-edit-dialog/user-edit-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { UserStore } from 'src/app/core/services/stores/user-store';
import { EditUserRolesComponent } from '../edit-user-roles/edit-user-roles.component';



@Component({
  selector: 'app-users-list-card',
  templateUrl: './users-list-card.component.html',
  styleUrls: ['./users-list-card.component.scss'],
})
export class UsersListCardComponent implements OnInit {
  displayedColumns: string[] = ['Avatar', 'Name', 'Phone','Campus','Status',"Actions"];
  allowdRolesForUpdate:string[]=["Sudo","User.Update"];
  @Input() dataSource:IUserList[];

  constructor(
    private dialog:MatDialog,
    private userStore:UserStore
  ) { }

  ngOnInit(): void {
  }


  onUserRole(user:IUserList){
      this.dialog.open(EditUserRolesComponent,{
        minWidth:"70vw",
        maxHeight:"100vh",
        data:{
           user:user
        }

      })
  }

  onUpdate(user:IUserList){
     this.dialog.open(UserEditDialogComponent,{
       width:"45rem",
       maxHeight:"100vh",
       data:{
          mode:'update',
          title:"Kullanıcı Bilgilerini Güncelle",
          user:user
       }
     }) 
  }

  onActivePassive(user:IUserList){
    const userStatus=user.isActive?false:true;

     const dialogRef= this.dialog.open(ConfirmDialogComponent,{
        width:"40rem",
      })

      dialogRef.afterClosed().subscribe(result=>{
        if(result){
          const updateUser={
            ...user,
            isActive:userStatus,
            campusId:user.campus?.id,
            degreeId:user.degree?.id,
            departmentId:user.department?.id
          }

          this.userStore.update(user.id,updateUser);
        }
      })

  }



 

}
