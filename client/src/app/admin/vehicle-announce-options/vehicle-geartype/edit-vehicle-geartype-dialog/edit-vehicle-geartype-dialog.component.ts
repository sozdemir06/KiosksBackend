import { Component, OnInit, Inject } from '@angular/core';
import { IVehicleGearType } from 'src/app/shared/models/IVehicleGearType';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { VehicleGearTypeStore } from 'src/app/core/services/stores/vehicle-gear-type-store';

@Component({
  selector: 'app-edit-vehicle-geartype-dialog',
  templateUrl: './edit-vehicle-geartype-dialog.component.html',
  styleUrls: ['./edit-vehicle-geartype-dialog.component.scss'],
})
export class EditVehicleGeartypeDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  item: IVehicleGearType;

  vehicleGearTypeForm: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditVehicleGeartypeDialogComponent>,
    private vehicleGearTypeStore: VehicleGearTypeStore,
    private fb: FormBuilder
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data?.item;

    const formControls = {
      name: ['', [Validators.required, Validators.maxLength(60)]],
    };
    if (this.mode == 'create') {
      this.vehicleGearTypeForm = this.fb.group({ ...formControls });
    } else if (this.mode == 'update') {
      this.vehicleGearTypeForm = this.fb.group(formControls);
      this.vehicleGearTypeForm.patchValue({ ...this.item });
    }
  }

  get f() {
    return this.vehicleGearTypeForm.controls;
  }

  ngOnInit(): void {}

  onSubmitform() {
    if (this.vehicleGearTypeForm.valid) {
      if (this.mode == 'create') {
        const model: IVehicleGearType = {
          ...this.vehicleGearTypeForm.value,
        };
        this.vehicleGearTypeStore.create(model);
        this.dialogRef.close();
      } else if (this.mode == 'update') {
        const model: IVehicleGearType = {
          ...this.vehicleGearTypeForm.value,
          id: this.item?.id,
        };

        this.vehicleGearTypeStore.update(model);
        this.dialogRef.close();
      }
    }
  }
}
