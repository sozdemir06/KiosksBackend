import { Component, OnInit, Inject } from '@angular/core';
import { IScreen } from 'src/app/shared/models/IScreen';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ScreenStore } from 'src/app/core/services/stores/screen-store';

@Component({
  selector: 'app-edit-screens-dialog',
  templateUrl: './edit-screens-dialog.component.html',
  styleUrls: ['./edit-screens-dialog.component.scss'],
})
export class EditScreensDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  item: IScreen;

  screenForm: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditScreensDialogComponent>,
    private fb: FormBuilder,
    private screenStore: ScreenStore
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data?.item;

    const formControls = {
      name: ['', [Validators.required, Validators.maxLength(100)]],
      position: ['', [Validators.required, Validators.maxLength(30)]],
      isFull: [false],
    };
    if (this.mode == 'create') {
      this.screenForm = this.fb.group({ ...formControls });
    } else if (this.mode == 'update') {
      this.screenForm = this.fb.group(formControls);
      this.screenForm.patchValue({ ...this.item });
    }
  }

  get f() {
    return this.screenForm.controls;
  }

  ngOnInit(): void {}

  onSubmit() {
    if (this.screenForm.valid) {
      if (this.mode == 'create') {
        const model: IScreen = {
          ...this.screenForm.value,
        };
        this.screenStore.create(model);
        this.dialogRef.close();
      } else if (this.mode == 'update') {
        const model: IScreen = {
          ...this.screenForm.value,
          id: this.item?.id,
        };

        this.screenStore.update(model);
        this.dialogRef.close();
      }
    }
  }
}
