import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { IVehicleModel } from 'src/app/shared/models/IVehicleModel';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { VehicleModelStore } from 'src/app/core/services/stores/vehicle-model-store';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { VehicleCategoryStore } from 'src/app/core/services/stores/vehicle-category-store';
import { VehicleBrandStore } from 'src/app/core/services/stores/vehicle-brand-store';
import { VehicleModelParams } from 'src/app/shared/models/VehicleModelParams';

@Component({
  selector: 'app-edit-vehicle-model-dialog',
  templateUrl: './edit-vehicle-model-dialog.component.html',
  styleUrls: ['./edit-vehicle-model-dialog.component.scss'],
})
export class EditVehicleModelDialogComponent implements OnInit,OnDestroy {
  title: string;
  mode: 'create' | 'update';
  item: IVehicleModel;
  selectedOption: number = 1;
  unSubscribeFromVehicleCategoryId:any;

  vehicleModelForm: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditVehicleModelDialogComponent>,
    private vehicleModelStore: VehicleModelStore,
    public vehicleBrandStore: VehicleBrandStore,
    public vehicleCategoryStore: VehicleCategoryStore,
    private fb: FormBuilder
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data?.item;


    const formControls = {
      vehicleModelName: ['', [Validators.required, Validators.maxLength(60)]],
      vehicleCategoryId: ['', Validators.required],
      vehicleBrandId: ['', Validators.required],
    };

    if (this.mode == 'create') {
      this.vehicleModelForm = this.fb.group({ ...formControls });
    } else if (this.mode == 'update') {
      this.vehicleModelForm = this.fb.group(formControls);
      this.vehicleModelForm.patchValue({ ...this.item });
    }
  }

  get f() {
    return this.vehicleModelForm.controls;
  }

  ngOnInit(): void {
  this.unSubscribeFromVehicleCategoryId=  this.vehicleModelForm
      .get('vehicleCategoryId')
      .valueChanges.subscribe((result) => {
        const params = this.vehicleBrandStore.getVehicleBrandParams();
        params.pageIndex = 1;
        params.pageSize = 500;
        params.vehicleCategoryId=result;
        this.vehicleBrandStore.setVehicleBrandParams(params);
        this.vehicleBrandStore.getVehicleBrandList();
      });
  }

  onSubmitform() {
    if(this.vehicleModelForm.valid){
      if(this.mode=="create"){
        const model:IVehicleModel={
          ...this.vehicleModelForm.value
        };
        this.vehicleModelStore.create(model);
        this.dialogRef.close();
      }else if(this.mode=="update"){
        const model:IVehicleModel={
          ...this.vehicleModelForm.value,
          id:this.item?.id
        };
        this.vehicleModelStore.update(model);
        this.dialogRef.close();
      }
    }
  }

  ngOnDestroy(){
    this.unSubscribeFromVehicleCategoryId.unsubscribe();

  }
}
