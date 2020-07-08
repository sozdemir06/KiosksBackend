import { Component, OnInit, Inject } from '@angular/core';
import { IVehicleEngineSize } from 'src/app/shared/models/IVehicleEngineSize';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { VehicleEngineSizeStore } from 'src/app/core/services/stores/vehicle-engine-size-store';

@Component({
  selector: 'app-edit-vehicle-engine-size-dialog',
  templateUrl: './edit-vehicle-engine-size-dialog.component.html',
  styleUrls: ['./edit-vehicle-engine-size-dialog.component.scss'],
})
export class EditVehicleEngineSizeDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  item: IVehicleEngineSize;

  vehicleEngineSizeForm: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditVehicleEngineSizeDialogComponent>,
    private fb: FormBuilder,
    private vehicleEngineSizeStore: VehicleEngineSizeStore
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data?.item;

    const formControls = {
      name: ['', [Validators.required, Validators.maxLength(60)]],
    };

    if (this.mode == 'create') {
      this.vehicleEngineSizeForm = this.fb.group({ ...formControls });
    } else {
      this.vehicleEngineSizeForm = this.fb.group(formControls);
      this.vehicleEngineSizeForm.patchValue({ ...this.item });
    }
  }

  get f() {
    return this.vehicleEngineSizeForm.controls;
  }

  ngOnInit(): void {}

  onSubmitform() {
    if (this.vehicleEngineSizeForm.valid) {
      if (this.mode == 'create') {
        const model: IVehicleEngineSize = {
          ...this.vehicleEngineSizeForm.value,
        };
        this.vehicleEngineSizeStore.create(model);
        this.dialogRef.close();
      } else if (this.mode == 'update') {
        const model: IVehicleEngineSize = {
          ...this.vehicleEngineSizeForm.value,
          id: this.item?.id,
        };

        this.vehicleEngineSizeStore.update(model);
        this.dialogRef.close();
      }
    }
  }
}
