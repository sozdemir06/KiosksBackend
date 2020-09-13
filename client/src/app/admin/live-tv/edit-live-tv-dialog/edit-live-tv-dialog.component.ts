import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { ILiveTvBroadCast } from 'src/app/shared/models/ILiveTvBroadCast';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { LiveTvBroadCastStore } from 'src/app/core/services/stores/live-broadcast-store';
import { HelperService } from 'src/app/core/services/helper-service';
import { UserStore } from 'src/app/core/services/stores/user-store';
import { LiveTvListStore } from 'src/app/core/services/stores/live-tv-list-store';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-edit-live-tv-dialog',
  templateUrl: './edit-live-tv-dialog.component.html',
  styleUrls: ['./edit-live-tv-dialog.component.scss']
})
export class EditLiveTvDialogComponent implements OnInit,OnDestroy {

  title: string;
  mode: 'create' | 'update';
  item: ILiveTvBroadCast;
  contentType:string;
  subscription=Subscription.EMPTY;

  liveTvBroadCastForm: FormGroup;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditLiveTvDialogComponent>,
    private liveTvBroadCastStore: LiveTvBroadCastStore,
    public liveTvListStore:LiveTvListStore,
    private fb: FormBuilder,
    public helperService: HelperService,
    public userStore: UserStore
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data?.item;

    const formControls = {
      header: ['', [Validators.required, Validators.maxLength(140)]],
      contentType: ['', Validators.required],
      youtubeId:["",Validators.required],
      publishStartDate: [
        this.helperService.dateToLocaleFormat(new Date()),
        Validators.required,
      ],
      publishFinishDate: [
        this.helperService.dateToLocaleFormat(new Date()),
        Validators.required,
      ],
      slideIntervalTime: [9, Validators.required],
    };
    if (this.mode == 'create') {
      this.liveTvBroadCastForm = this.fb.group({
        ...formControls,
      });
    } else if (this.mode == 'update') {
      this.liveTvBroadCastForm = this.fb.group(formControls);
      this.liveTvBroadCastForm.patchValue({
        ...this.item,
        userId: this.item?.user?.id,
      });
      this.contentType=this.item.contentType;
      let slideTime=this.liveTvBroadCastForm.get("slideIntervalTime");

    }
  }

  get f() {
    return this.liveTvBroadCastForm.controls;
  }

  ngOnInit(): void {
   this.subscription= this.liveTvBroadCastForm.get("contentType").valueChanges.subscribe(result=>{
     this.contentType=result;
   });
  }

  onChangeStartDate(event) {
    this.liveTvBroadCastForm.patchValue({
      publishStartDate: this.helperService.dateToLocaleFormat(event.value),
    });
  }

  onChangeFinishDate(event) {
    this.liveTvBroadCastForm.patchValue({
      publishFinishDate: this.helperService.dateToLocaleFormat(event.value),
    });
  }

  onSubmit() {
    if (this.liveTvBroadCastForm.valid) {
      const startDate = this.liveTvBroadCastForm.get('publishStartDate').value;
      const finishDate = this.liveTvBroadCastForm.get('publishFinishDate').value;

      const checkDate = this.helperService.checkPublishDate(
        startDate,
        finishDate
      );

      if (checkDate) {
        if (this.mode == 'create') {
          const model: ILiveTvBroadCast = {
            ...this.liveTvBroadCastForm?.value,
            isNew: true,
            isPublish: false,
            reject: false,
          };
          this.liveTvBroadCastStore.create(model);
          this.dialogRef.close();
        } else if (this.mode == 'update') {
          const model: ILiveTvBroadCast = {
            ...this.item,
            ...this.liveTvBroadCastForm.value,
          };
          this.liveTvBroadCastStore.update(model);
          this.dialogRef.close();
        }
      }
    }
  }

  ngOnDestroy(){
    this.subscription.unsubscribe();
  }

}
