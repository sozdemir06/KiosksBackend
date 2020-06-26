import { Component, OnInit, Inject } from '@angular/core';
import { IRoleCategory } from 'src/app/shared/models/IRoleCategory';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { RoleCategorStore } from 'src/app/core/services/stores/role-category-store';


@Component({
  selector: 'app-edit-role-category-dialog',
  templateUrl: './edit-role-category-dialog.component.html',
  styleUrls: ['./edit-role-category-dialog.component.scss']
})
export class EditRoleCategoryDialogComponent implements OnInit {
title:string;
mode:"create" | "update";
roleCategory:IRoleCategory;

roleCategoryForm:FormGroup;

  constructor(
    private dialogRef:MatDialogRef<EditRoleCategoryDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data:any,
    private fb:FormBuilder,
    private roleCategoryStore:RoleCategorStore

  ) {
     this.title=this.data?.title;
     this.mode=this.data?.mode;
     this.roleCategory=this.data.roleCategory;

     const formControls={
         name:["",Validators.required],
         description:["",Validators.required] 
     };

     if(this.mode=='create'){
        this.roleCategoryForm=this.fb.group({...formControls});
     }else if(this.mode=="update"){
       this.roleCategoryForm=this.fb.group(formControls);
       this.roleCategoryForm.patchValue({
         ...this.roleCategory
       });
     }


   }

   get f(){return this.roleCategoryForm.controls;}

  ngOnInit(): void {
  }


  onSubmit(){
    if(this.roleCategoryForm.valid){
      if(this.mode=="create"){
         this.roleCategoryStore.create(this.roleCategoryForm.value);
         this.dialogRef.close();
      }else if(this.mode=="update"){
        const model:IRoleCategory={
          ...this.roleCategoryForm.value,
          id:this.roleCategory?.id,
          
        }
        this.roleCategoryStore.update(model);
        this.dialogRef.close();
      }
    }

  }

}
