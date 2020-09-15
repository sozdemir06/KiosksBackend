import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DegreeStore } from 'src/app/core/services/stores/degree-store';
import { IDegree } from 'src/app/shared/models/IDegree';

@Component({
  selector: 'app-edit-degree-dialog',
  templateUrl: './edit-degree-dialog.component.html',
  styleUrls: ['./edit-degree-dialog.component.scss'],
})
export class EditDegreeDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  item: IDegree;

  degreeForm: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditDegreeDialogComponent>,
    private degreeStore: DegreeStore,
    private fb: FormBuilder
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data?.item;

    const formControls = {
      name: ['', [Validators.required, Validators.maxLength(60)]],
    };
    if (this.mode == 'create') {
      this.degreeForm = this.fb.group({ ...formControls });
    } else if (this.mode == 'update') {
      this.degreeForm = this.fb.group(formControls);
      this.degreeForm.patchValue({ ...this.item });
    }
  }

  get f() {
    return this.degreeForm.controls;
  }

  ngOnInit(): void {}

  onSubmitform() {
    if (this.degreeForm.valid) {
      if (this.mode == 'create') {
        const model: IDegree = {
          ...this.degreeForm.value,
        };
        this.degreeStore.create(model);
        this.dialogRef.close();
      } else if (this.mode == 'update') {
        const model: IDegree = {
          ...this.degreeForm.value,
          id: this.item?.id,
        };
        this.degreeStore.update(model);
        this.dialogRef.close();
      }
    }
  }
}
