import { Component, OnInit, InjectionToken, Inject } from '@angular/core';
import { IFlatOfHome } from 'src/app/shared/models/IFlatOfHome';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EditNumberOfRoomsDialogComponent } from '../../number-of-room/edit-number-of-rooms-dialog/edit-number-of-rooms-dialog.component';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { FlatOfHomeStore } from 'src/app/core/services/stores/flat-of-home-store';

@Component({
  selector: 'app-edit-flats-of-home-dialog',
  templateUrl: './edit-flats-of-home-dialog.component.html',
  styleUrls: ['./edit-flats-of-home-dialog.component.scss']
})
export class EditFlatsOfHomeDialogComponent implements OnInit {
title:string;
mode:"create" | "update";
item:IFlatOfHome;

flatOFHomeForm:FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    private dialogRef:MatDialogRef<EditNumberOfRoomsDialogComponent>,
    private fb:FormBuilder,
    private flatOfHomeStore:FlatOfHomeStore

  ) {
    this.title=data?.title;
    this.mode=data?.mode;
    this.item=data?.item;

    const formControls={
      name:["",[Validators.required,Validators.maxLength(60)]]
    }

    if(this.mode=="create"){
        this.flatOFHomeForm=this.fb.group({...formControls});
    }else if(this.mode=="update"){
      this.flatOFHomeForm=this.fb.group(formControls);
      this.flatOFHomeForm.patchValue({...this.item})
    }

   }

   get f(){return this.flatOFHomeForm.controls}

  ngOnInit(): void {
  }

  onSubmitform(){
      if(this.flatOFHomeForm.valid){
        if(this.mode=="create"){
          const model:IFlatOfHome={
            ...this.flatOFHomeForm.value
          }
          this.flatOfHomeStore.create(model);
          this.dialogRef.close();
        }else if(this.mode=="update"){
          const model:IFlatOfHome={
            ...this.flatOFHomeForm.value,
            id:this.item?.id
          }

          this.flatOfHomeStore.update(model);
          this.dialogRef.close();
        }
      }
  }


}
