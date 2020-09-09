import { Component, OnInit, Inject } from '@angular/core';
import { IScreen } from 'src/app/shared/models/IScreen';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EditScreenHeaderComponent } from '../edit-screen-header/edit-screen-header.component';
import { ScreenStore } from 'src/app/core/services/stores/screen-store';
import { IScreenFooter } from 'src/app/shared/models/IScreenFooter';

@Component({
  selector: 'app-edit-screen-footer',
  templateUrl: './edit-screen-footer.component.html',
  styleUrls: ['./edit-screen-footer.component.scss'],
})
export class EditScreenFooterComponent implements OnInit {
  title: string;
  item: IScreen;
  mode: 'create' | 'update';

  screenFooterForm: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditScreenHeaderComponent>,
    private fb: FormBuilder,
    private screenStore: ScreenStore
  ) {
    this.title = data?.title;
    this.item = data?.item;
    this.mode = data?.mode;

    const formControls = {
      footerText: ['', Validators.required],
      isShowWheatherForCast: [false],
      isShowStockExchange: [false],
    };

    if (this.mode == 'create') {
      this.screenFooterForm = this.fb.group({ ...formControls });
    } else if (this.mode == 'update') {
      this.screenFooterForm = this.fb.group(formControls);
      this.screenFooterForm.patchValue({ ...this.item.screenFooters });
    }
  }

  get f() {
    return this.screenFooterForm.controls;
  }
  ngOnInit(): void {}

  onSubmit() {
    if (this.screenFooterForm.valid) {
      if (this.mode == 'create') {
        const model: IScreenFooter = {
          ...this.screenFooterForm.value,
          screenId: this.item?.id,
        };
        this.screenStore.createScreenFooter(model);
        this.dialogRef.close();
      } else if (this.mode == 'update') {
        const model: IScreenFooter = {
          ...this.item?.screenFooters,
          ...this.screenFooterForm.value,
          screenId: this.item?.id,
        };
        this.screenStore.updateScreenFooter(model);
        this.dialogRef.close();
      }
    }
  }
}
