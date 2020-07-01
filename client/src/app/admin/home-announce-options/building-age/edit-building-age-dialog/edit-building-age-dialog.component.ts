import { Component, OnInit, Input, Inject } from '@angular/core';
import { IBuildingAge } from 'src/app/shared/models/IBuildingAge';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BuildingAgeStore } from 'src/app/core/services/stores/building-age-store';

@Component({
  selector: 'app-edit-building-age-dialog',
  templateUrl: './edit-building-age-dialog.component.html',
  styleUrls: ['./edit-building-age-dialog.component.scss'],
})
export class EditBuildingAgeDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  item: IBuildingAge;

  buildingAgeForm: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditBuildingAgeDialogComponent>,
    private fb: FormBuilder,
    private buildingAgeStore: BuildingAgeStore
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data.item;

    const formControls = {
      name: ['', [Validators.required, Validators.maxLength(60)]],
    };

    if (this.mode == 'create') {
      this.buildingAgeForm = this.fb.group({ ...formControls });
    } else if (this.mode == 'update') {
      this.buildingAgeForm = this.fb.group(formControls);
      this.buildingAgeForm.patchValue({ ...this.item });
    }
  }

  get f() {
    return this.buildingAgeForm.controls;
  }

  ngOnInit(): void {}

  onSubmitform() {
   
    if (this.buildingAgeForm.valid) {
      if (this.mode == 'create') {
        const model: IBuildingAge = {
          ...this.buildingAgeForm.value,
        };
        this.buildingAgeStore.create(model);
        this.dialogRef.close();
      } else if (this.mode == 'update') {
        const model: IBuildingAge = {
          ...this.buildingAgeForm.value,
          id:this.item?.id
        };
        this.buildingAgeStore.update(model);
        this.dialogRef.close();
      }
    }
  }
}
