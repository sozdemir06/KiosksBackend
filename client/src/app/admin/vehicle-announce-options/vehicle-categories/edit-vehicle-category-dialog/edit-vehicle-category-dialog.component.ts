import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IVehicleCategory } from 'src/app/shared/models/IVehicleCategory';
import { VehicleCategoryStore } from 'src/app/core/services/stores/vehicle-category-store';

@Component({
  selector: 'app-edit-vehicle-category-dialog',
  templateUrl: './edit-vehicle-category-dialog.component.html',
  styleUrls: ['./edit-vehicle-category-dialog.component.scss'],
})
export class EditVehicleCategoryDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  item: IVehicleCategory;

  vehicleCategoryForm: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EditVehicleCategoryDialogComponent>,
    private vehicleCategoryStore: VehicleCategoryStore
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data?.item;

    const formControls = {
      categoryName: ['', [Validators.required, Validators.maxLength(60)]],
    };
    if (this.mode == 'create') {
      this.vehicleCategoryForm = this.fb.group({ ...formControls });
    } else if (this.mode == 'update') {
      this.vehicleCategoryForm = this.fb.group(formControls);
      this.vehicleCategoryForm.patchValue({ ...this.item });
    }
  }

  get f() {
    return this.vehicleCategoryForm.controls;
  }

  ngOnInit(): void {}

  onSubmitform() {
    if (this.vehicleCategoryForm.valid) {
      if (this.mode == 'create') {
        const model: IVehicleCategory = {
          ...this.vehicleCategoryForm.value,
        };
        this.vehicleCategoryStore.create(model);
        this.dialogRef.close();
      } else if (this.mode == 'update') {
        const model: IVehicleCategory = {
          ...this.vehicleCategoryForm.value,
          id: this.item?.id,
        };

        this.vehicleCategoryStore.update(model);
        this.dialogRef.close();
      }
    }
  }
}
