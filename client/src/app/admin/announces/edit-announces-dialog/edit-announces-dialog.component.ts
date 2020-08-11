import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { IAnnounce } from 'src/app/shared/models/IAnnounce';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EditVehicleAnnounceDialogComponent } from '../../vehicle-announces/edit-vehicle-announce-dialog/edit-vehicle-announce-dialog.component';
import { HelperService } from 'src/app/core/services/helper-service';
import { UserStore } from 'src/app/core/services/stores/user-store';
import { AnnounceStore } from 'src/app/core/services/stores/announce-store';
import { IUserList } from 'src/app/shared/models/IUser';
import { AnnounceContentTypeStore } from 'src/app/core/services/stores/announce-content-type-store';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-edit-announces-dialog',
  templateUrl: './edit-announces-dialog.component.html',
  styleUrls: ['./edit-announces-dialog.component.scss'],
})
export class EditAnnouncesDialogComponent implements OnInit, OnDestroy {
  title: string;
  mode: 'create' | 'update';
  item: IAnnounce;
  subscription: Subscription = Subscription.EMPTY;
  showTextEditor: boolean = false;
  announceForm: FormGroup;
  formControls: any;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditVehicleAnnounceDialogComponent>,
    private announceStore: AnnounceStore,
    public announceContentTypeStore: AnnounceContentTypeStore,
    private fb: FormBuilder,
    private helperService: HelperService,
    public userStore: UserStore
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data?.item;

    this.formControls = {
      header: ['', [Validators.required, Validators.maxLength(140)]],
      contentType: ['', Validators.required],
      content: [''],
      publishStartDate: [
        this.helperService.dateToLocaleFormat(new Date()),
        Validators.required,
      ],
      publishFinishDate: [
        this.helperService.dateToLocaleFormat(new Date()),
        Validators.required,
      ],
      slideIntervalTime: [9, Validators.required],
      userId: ['', Validators.required],
    };

    if (this.mode == 'create') {
      this.announceForm = this.fb.group({ ...this.formControls });
    } else if (this.mode == 'update') {
      this.announceForm = this.fb.group(this.formControls);
      this.announceForm.patchValue({ ...this.item });
      this.announceForm.get("contentType").disable();
    }
  }

  ngOnInit(): void {
    this.subscription = this.announceForm
      .get('contentType')
      .valueChanges.subscribe((result: string) => {
        const res: string = result.toLowerCase();
        if (res == 'image' || res == 'video') {
          this.showTextEditor = false;
        } else {
          this.showTextEditor = true;
        }
      });
  }

  get f() {
    return this.announceForm.controls;
  }

  onUserSelectionChange(user: IUserList) {
    this.announceForm.patchValue({
      userId: user.id,
    });
  }
  onChangeStartDate(event) {
    this.announceForm.patchValue({
      publishStartDate: this.helperService.dateToLocaleFormat(event.value),
    });
  }

  onChangeFinishDate(event) {
    this.announceForm.patchValue({
      publishFinishDate: this.helperService.dateToLocaleFormat(event.value),
    });
  }

  onSubmit() {
    if (this.announceForm.valid) {
      const startDate = this.announceForm.get('publishStartDate').value;
      const finishDate = this.announceForm.get('publishFinishDate').value;

      const checkDate = this.helperService.checkPublishDate(
        startDate,
        finishDate
      );

      if (checkDate) {
        if (this.mode == 'create') {
          const model: IAnnounce = {
            ...this.announceForm?.value,
            isNew: true,
            isPublish: false,
            reject: false,
          };
          this.announceStore.create(model);
          this.dialogRef.close();
        } else if (this.mode == 'update') {
          const model: IAnnounce = {
            ...this.announceForm.value,
            id: this.item?.id,
            isNew: this.item?.isNew,
            reject: this.item?.reject,
            isPublish: this.item?.isPublish,
            contentType:this.item?.contentType
          };
          this.announceStore.update(model);
          this.dialogRef.close();
        }
      }
    }
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
