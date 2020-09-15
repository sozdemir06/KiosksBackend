import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DepartmentStore } from 'src/app/core/services/stores/department-store';
import { IDepartment } from 'src/app/shared/models/IDepartment';

@Component({
  selector: 'app-edit-department-dialog',
  templateUrl: './edit-department-dialog.component.html',
  styleUrls: ['./edit-department-dialog.component.scss']
})
export class EditDepartmentDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  item: IDepartment;

  departmentForm: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditDepartmentDialogComponent>,
    private fb: FormBuilder,
    private departmentStore: DepartmentStore
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data.item;

    const formControls = {
      name: ['', [Validators.required, Validators.maxLength(60)]],
    };

    if (this.mode == 'create') {
      this.departmentForm = this.fb.group({ ...formControls });
    } else if (this.mode == 'update') {
      this.departmentForm = this.fb.group(formControls);
      this.departmentForm.patchValue({ ...this.item });
    }
  }

  get f() {
    return this.departmentForm.controls;
  }

  ngOnInit(): void {}

  onSubmitform() {
   
    if (this.departmentForm.valid) {
      if (this.mode == 'create') {
        const model: IDepartment = {
          ...this.departmentForm.value,
        };
        this.departmentStore.create(model);
        this.dialogRef.close();
      } else if (this.mode == 'update') {
        const model: IDepartment = {
          ...this.departmentForm.value,
          id:this.item?.id
        };
        this.departmentStore.update(model);
        this.dialogRef.close();
      }
    }
  }
}
