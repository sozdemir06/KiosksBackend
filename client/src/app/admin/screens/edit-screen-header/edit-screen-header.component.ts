import { Component, OnInit, Inject } from '@angular/core';
import { IScreen } from 'src/app/shared/models/IScreen';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EditScreensDialogComponent } from '../edit-screens-dialog/edit-screens-dialog.component';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ScreenStore } from 'src/app/core/services/stores/screen-store';
import { IScreenHeader } from 'src/app/shared/models/IScreenHeader';

@Component({
  selector: 'app-edit-screen-header',
  templateUrl: './edit-screen-header.component.html',
  styleUrls: ['./edit-screen-header.component.scss'],
})
export class EditScreenHeaderComponent implements OnInit {
  title: string;
  item: IScreen;
  mode: 'create' | 'update';

  screenheaderForm: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditScreenHeaderComponent>,
    private fb: FormBuilder,
    private screenStore: ScreenStore
  ) {
    this.title = data?.title;
    this.item = data?.item;
    this.mode=data?.mode;

    const formControls = {
      headerText: ['', Validators.required],
    };

    if (this.mode == 'create') {
      this.screenheaderForm = this.fb.group({ ...formControls });
    } else if (this.mode == 'update') {
      this.screenheaderForm = this.fb.group(formControls);
      this.screenheaderForm.patchValue({ ...this.item.screenHeaders });
    }
  }

  get f() {
    return this.screenheaderForm.controls;
  }

  ngOnInit(): void {}

  onSubmit() {
    if (this.screenheaderForm.valid) {
      if (this.mode == 'create') {
        const model: IScreenHeader = {
          ...this.screenheaderForm.value,
          screenId: this.item.id,
        };
        this.screenStore.createScreenHeader(model);
        this.dialogRef.close();
      } else if (this.mode == 'update') {
        const model: IScreenHeader = {
          ...this.item?.screenHeaders,
          ...this.screenheaderForm.value,
          screenId: this.item.id,
        };
        this.screenStore.updateScreenHeader(model);
        this.dialogRef.close();
      }
    }
  }
}
