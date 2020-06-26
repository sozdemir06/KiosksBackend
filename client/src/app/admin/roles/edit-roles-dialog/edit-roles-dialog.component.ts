import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IRole } from 'src/app/shared/models/IRole';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RoleCategorStore } from 'src/app/core/services/stores/role-category-store';
import { RoleStore } from 'src/app/core/services/stores/role-store';

@Component({
  selector: 'app-edit-roles-dialog',
  templateUrl: './edit-roles-dialog.component.html',
  styleUrls: ['./edit-roles-dialog.component.scss']
})
export class EditRolesDialogComponent implements OnInit {
title:string;
mode:'create' | 'update';
role:IRole;


rolesForm:FormGroup;

  constructor(
    private dialogRef:MatDialogRef<EditRolesDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data:any,
    private fb:FormBuilder,
    public roleCategoryStore:RoleCategorStore,
    private roleStore:RoleStore
  ) { 
    this.title=data?.title;
    this.mode=data?.mode;
    this.role=data?.role;

    const formControls={
      name:["",Validators.required],
      description:["",Validators.required],
      roleCategoryId:["",Validators.required]
    }

    if(this.mode=="create"){
      this.rolesForm=this.fb.group({...formControls});
    }else if(this.mode="update"){
      this.rolesForm=this.fb.group(formControls);
      this.rolesForm.patchValue({...this.role});
    }

  }

  get f(){return this.rolesForm.controls;}

  ngOnInit(): void {
  }


  onSubmitform(){
    if(this.rolesForm.valid){
      if(this.mode=="create"){
        this.roleStore.create(this.rolesForm.value);
        this.dialogRef.close();
      }else if(this.mode=="update")
      {
        const model:IRole={
          ...this.rolesForm.value,
          id:this.role.id,

        }
        
        this.roleStore.update(model);
        this.dialogRef.close();
      }
    }
  }


}
