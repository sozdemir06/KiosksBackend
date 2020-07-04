import { Component, OnInit, Inject } from '@angular/core';
import { IHeatingType } from 'src/app/shared/models/IHeatingType';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HeatingTypeStore } from 'src/app/core/services/stores/heating-type-store';

@Component({
  selector: 'app-edit-heating-types-dialog',
  templateUrl: './edit-heating-types-dialog.component.html',
  styleUrls: ['./edit-heating-types-dialog.component.scss'],
})
export class EditHeatingTypesDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  item: IHeatingType;

  heatingTypeForm: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditHeatingTypesDialogComponent>,
    private fb: FormBuilder,
    private heatingTypeStore: HeatingTypeStore
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data?.item;

    const formControls = {
      name: ['', [Validators.required, Validators.maxLength(60)]],
    };
    if (this.mode == 'create') {
      this.heatingTypeForm = this.fb.group({
        ...formControls,
      });
    } else if (this.mode == 'update') {
      this.heatingTypeForm = this.fb.group(formControls);
      this.heatingTypeForm.patchValue({ ...this.item });
    }
  }

  get f() {
    return this.heatingTypeForm.controls;
  }

  ngOnInit(): void {}

  onSubmitform() {
    if (this.heatingTypeForm.valid) {
      if (this.mode == 'create') {
        const model: IHeatingType = {
          ...this.heatingTypeForm.value,
        };
        this.heatingTypeStore.create(model);
        this.dialogRef.close();
      } else if (this.mode == 'update') {
        const model: IHeatingType = {
          ...this.heatingTypeForm.value,
          id: this.item?.id,
        };
        this.heatingTypeStore.update(model);
        this.dialogRef.close();
      }
    }
  }
}
