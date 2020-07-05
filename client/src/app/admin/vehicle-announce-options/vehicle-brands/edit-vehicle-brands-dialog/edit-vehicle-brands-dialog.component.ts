import { Component, OnInit, Inject } from '@angular/core';
import { IVehicleBrand } from 'src/app/shared/models/IVehicleBrand';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { VehicleBrandStore } from 'src/app/core/services/stores/vehicle-brand-store';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { VehicleCategoryStore } from 'src/app/core/services/stores/vehicle-category-store';

@Component({
  selector: 'app-edit-vehicle-brands-dialog',
  templateUrl: './edit-vehicle-brands-dialog.component.html',
  styleUrls: ['./edit-vehicle-brands-dialog.component.scss']
})
export class EditVehicleBrandsDialogComponent implements OnInit {
title:string;
mode:"create" | "update";
item:IVehicleBrand;

vehicleBrandForm:FormGroup;


  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    private dialogRef:MatDialogRef<EditVehicleBrandsDialogComponent>,
    private vehicleBrandStore:VehicleBrandStore,
    public vehicleCategoryStore:VehicleCategoryStore,
    private fb:FormBuilder
  ) {
    this.title=data?.title;
    this.mode=data?.mode;
    this.item=data?.item;

    const formControls={
      brandName:["",[Validators.required,Validators.maxLength(60)]],
      vehicleCategoryId:["",Validators.required]
    }
    if(this.mode=="create"){
      this.vehicleBrandForm=this.fb.group({...formControls});
    }else if(this.mode=="update"){
      this.vehicleBrandForm=this.fb.group(formControls);
      this.vehicleBrandForm.patchValue({...this.item});
    }

   }

   get f(){return this.vehicleBrandForm.controls};

   
  ngOnInit(): void {
  }

  onSubmitform(){
    if(this.vehicleBrandForm.valid){
      if(this.mode=="create"){
        const model:IVehicleBrand={
          ...this.vehicleBrandForm.value
        };
        this.vehicleBrandStore.create(model);
        this.dialogRef.close();
      }else if(this.mode=="update"){
        const model:IVehicleBrand={
          ...this.vehicleBrandForm.value,
          id:this.item?.id
        };
        this.vehicleBrandStore.update(model);
        this.dialogRef.close();
      }
    }
  }

}
