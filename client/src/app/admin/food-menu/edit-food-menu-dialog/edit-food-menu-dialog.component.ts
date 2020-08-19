import { Component, OnInit, Inject } from '@angular/core';
import { IFoodMenu } from 'src/app/shared/models/IFoodMenu';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FoodMenuStore } from 'src/app/core/services/stores/food-menu-store';
import { HelperService } from 'src/app/core/services/helper-service';
import { UserStore } from 'src/app/core/services/stores/user-store';

@Component({
  selector: 'app-edit-food-menu-dialog',
  templateUrl: './edit-food-menu-dialog.component.html',
  styleUrls: ['./edit-food-menu-dialog.component.scss'],
})
export class EditFoodMenuDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  item: IFoodMenu;

  foodMenuForm: FormGroup;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditFoodMenuDialogComponent>,
    private foodMenuStore: FoodMenuStore,
    private fb: FormBuilder,
    private helperService: HelperService,
    public userStore: UserStore
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data?.item;

    const formControls = {
      content: ['', [Validators.required]],
      slideIntervalTime: [60, [Validators.required]],
      publishStartDate: [
        this.helperService.dateToLocaleFormat(new Date()),
        Validators.required,
      ],
      publishFinishDate: [
        this.helperService.dateToLocaleFormat(new Date()),
        Validators.required,
      ],
    };
    if (this.mode == 'create') {
      this.foodMenuForm = this.fb.group({
        ...formControls,
      });
    } else if (this.mode == 'update') {
      this.foodMenuForm = this.fb.group(formControls);
      this.foodMenuForm.patchValue({
        ...this.item,
        userId: this.item?.user?.id,
      });
    }
  }

  get f() {
    return this.foodMenuForm.controls;
  }

  ngOnInit(): void {}

  onChangeStartDate(event) {
    this.foodMenuForm.patchValue({
      publishStartDate: this.helperService.dateToLocaleFormat(event.value),
    });
  }

  onChangeFinishDate(event) {
    this.foodMenuForm.patchValue({
      publishFinishDate: this.helperService.dateToLocaleFormat(event.value),
    });
  }

  onSubmit() {
    if (this.foodMenuForm.valid) {
      const startDate = this.foodMenuForm.get('publishStartDate').value;
      const finishDate = this.foodMenuForm.get('publishFinishDate').value;

      const checkDate = this.helperService.checkPublishDate(
        startDate,
        finishDate
      );

      if (checkDate) {
        if (this.mode == 'create') {
          const model: IFoodMenu = {
            ...this.foodMenuForm?.value,
            isNew: true,
            isPublish: true,
            reject: false,
          };
          this.foodMenuStore.create(model);
          this.dialogRef.close();
        } else if (this.mode == 'update') {
          const model: IFoodMenu = {
            ...this.item,
            ...this.foodMenuForm.value,
          };
          this.foodMenuStore.update(model);
          this.dialogRef.close();
        }
      }
    }
  }
}
