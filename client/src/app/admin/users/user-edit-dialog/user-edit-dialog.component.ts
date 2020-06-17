import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { IUserList } from 'src/app/shared/models/IUser';
import { ErrorMessagesService } from 'src/app/core/services/error-messages.service';
import { MustMatch } from 'src/app/shared/helpers/password-match';

@Component({
  selector: 'app-user-edit-dialog',
  templateUrl: './user-edit-dialog.component.html',
  styleUrls: ['./user-edit-dialog.component.scss'],
  providers:[
    ErrorMessagesService
  ]
})
export class UserEditDialogComponent implements OnInit {
title:string;
mode:'create' | 'update';
user:IUserList;

loginForm:FormGroup;

  constructor(
    private fb:FormBuilder,
    @Inject(MAT_DIALOG_DATA) private data:any,
    private dialogRef:MatDialogRef<UserEditDialogComponent>
  ) { 

    this.title=data.title;
    this.mode=data.mode;
    this.user=data.user;

    const formControls={
      firstName:["",[Validators.required,Validators.maxLength(50)]],
      lastName:["",[Validators.required,Validators.maxLength(50)]],
      email:["",[Validators.required,Validators.email]],
      interPhone:["",[Validators.maxLength(11)]],
      gsmPhone:["",[Validators.maxLength(11)]],
      campusId:["",Validators.required],
      departmentId:["",Validators.required],
      degreeId:["",Validators.required],
      password:["",[Validators.required,Validators.minLength(4),Validators.maxLength(8)]],
      passwordConfirm:["",[Validators.required,Validators.minLength(4),Validators.maxLength(8)]]

    }

    if(this.mode=='create'){
      this.loginForm=this.fb.group({
        ...formControls
      },{
         validator:MustMatch('password','passwordConfirm')
      })
    }


  }

 get f(){return this.loginForm.controls;}

  ngOnInit(): void {
  }


  onSubmit(){
     console.log(this.loginForm.value);
  }

}
