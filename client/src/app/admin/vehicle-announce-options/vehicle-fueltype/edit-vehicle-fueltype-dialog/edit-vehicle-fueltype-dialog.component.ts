import { Component, OnInit, Inject } from '@angular/core';
import { IVehicleFuelType } from 'src/app/shared/models/IVehicleFuelType';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VehicleFuelTypeStore } from 'src/app/core/services/stores/vehicle-fuel-type-store';

@Component({
  selector: 'app-edit-vehicle-fueltype-dialog',
  templateUrl: './edit-vehicle-fueltype-dialog.component.html',
  styleUrls: ['./edit-vehicle-fueltype-dialog.component.scss']
})
export class EditVehicleFueltypeDialogComponent implements OnInit {
title:string;
mode:'create' | 'update';
item:IVehicleFuelType;

vehicleFuelTypeForm:FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    private matdialogRef:MatDialogRef<EditVehicleFueltypeDialogComponent>,
    private fb:FormBuilder,
    private vehicleFuelTypeStore:VehicleFuelTypeStore

  ) { 
    this.title=data?.title;
    this.mode=data?.mode;
    this.item=data?.item;

    const formControls={
      name:["",[Validators.required,Validators.maxLength(60)]]
    }

    if(this.mode=='create'){
      this.vehicleFuelTypeForm=this.fb.group({...formControls});
    }else if(this.mode=='update'){
      this.vehicleFuelTypeForm=this.fb.group(formControls);
      this.vehicleFuelTypeForm.patchValue({...this.item});
    } 
  }

  get f(){return this.vehicleFuelTypeForm.controls};

  ngOnInit(): void {
  }


  onSubmitform(){
    if(this.vehicleFuelTypeForm.valid){
      if(this.mode=='create'){
        const model:IVehicleFuelType={
          ...this.vehicleFuelTypeForm.value
        };
        this.vehicleFuelTypeStore.create(model);
        this.matdialogRef.close();
      }else if(this.mode=='update'){
        const model:IVehicleFuelType={
          ...this.vehicleFuelTypeForm.value,
          id:this.item?.id
        };
        this.vehicleFuelTypeStore.update(model);
        this.matdialogRef.close();
      }
    }
  }


}
