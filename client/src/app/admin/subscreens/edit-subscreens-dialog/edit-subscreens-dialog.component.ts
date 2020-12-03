import { Component, OnInit, Input, Inject } from '@angular/core';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SubScreenStore } from 'src/app/core/services/stores/subscreen-store';
import { Observable } from 'rxjs';
import { IScreen } from 'src/app/shared/models/IScreen';
import { ScreenStore } from 'src/app/core/services/stores/screen-store';

@Component({
  selector: 'app-edit-subscreens-dialog',
  templateUrl: './edit-subscreens-dialog.component.html',
  styleUrls: ['./edit-subscreens-dialog.component.scss'],
})
export class EditSubscreensDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  item: ISubScreen;
  screenId: number;
  screen$: Observable<IScreen>;

  subScreenForm: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private fb: FormBuilder,
    private subScreenStore: SubScreenStore,
    private screenStore: ScreenStore,
    private dialogRef: MatDialogRef<EditSubscreensDialogComponent>
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data?.item;
    this.screenId = data?.screenId;

    const formControls = {
      name: ['', [Validators.required, Validators.maxLength(100)]],
      position: ['', [Validators.required, Validators.maxLength(30)]],
      width: [''],
      height: [''],
      status: [false],
    };

    if (this.mode == 'create') {
      this.subScreenForm = this.fb.group({ ...formControls });
    } else if (this.mode == 'update') {
      this.subScreenForm = this.fb.group(formControls);
      this.subScreenForm.patchValue({ ...this.item });
    }
  }

  get f() {
    return this.subScreenForm.controls;
  }

  diabledInput(position: string): void {
    if (position?.toLowerCase() === 'vertical') {
      this.subScreenForm.get('width').disable();
    } else if (position?.toLowerCase() == 'horizontal') {
      this.subScreenForm.get('height').disable();
    }
  }

  ngOnInit(): void {
    this.screen$ = this.screenStore.getScreenById(this.screenId);
  }

  onSubmit() {
    if (this.subScreenForm.valid) {
      if (this.mode == 'create') {
        const model: ISubScreen = {
          ...this.subScreenForm.value,
          screenId:this?.screenId
        };
        this.subScreenStore.create(model);
        this.dialogRef.close();
      }else if(this.mode=="update"){
        const model:ISubScreen={
          ...this.subScreenForm.value,
          id:this.item?.id,
          screenId:this?.screenId
        }
        this.subScreenStore.update(model);
        this.dialogRef.close(true);
      }
    }
  }
}
