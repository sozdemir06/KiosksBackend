import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CampusStore } from 'src/app/core/services/stores/campus-store';
import { ICampus } from 'src/app/shared/models/ICampus';

@Component({
  selector: 'app-edit-campus-dialog',
  templateUrl: './edit-campus-dialog.component.html',
  styleUrls: ['./edit-campus-dialog.component.scss']
})
export class EditCampusDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  item: ICampus;

  campusForm: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditCampusDialogComponent>,
    private fb: FormBuilder,
    private campusStore: CampusStore
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data.item;

    const formControls = {
      name: ['', [Validators.required, Validators.maxLength(60)]],
    };

    if (this.mode == 'create') {
      this.campusForm = this.fb.group({ ...formControls });
    } else if (this.mode == 'update') {
      this.campusForm = this.fb.group(formControls);
      this.campusForm.patchValue({ ...this.item });
    }
  }

  get f() {
    return this.campusForm.controls;
  }

  ngOnInit(): void {}

  onSubmitform() {
   
    if (this.campusForm.valid) {
      if (this.mode == 'create') {
        const model: ICampus = {
          ...this.campusForm.value,
        };
        this.campusStore.create(model);
        this.dialogRef.close();
      } else if (this.mode == 'update') {
        const model: ICampus = {
          ...this.campusForm.value,
          id:this.item?.id
        };
        this.campusStore.update(model);
        this.dialogRef.close();
      }
    }
  }

}
