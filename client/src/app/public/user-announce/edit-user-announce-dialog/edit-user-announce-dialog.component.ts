import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { AdminHubService } from 'src/app/core/services/admin-hub-signalr-service';
import { HelperService } from 'src/app/core/services/helper-service';
import { AnnounceContentTypeStore } from 'src/app/core/services/stores/announce-content-type-store';
import { IUser } from 'src/app/shared/models/IUser';
import { IAnnounceForPublic } from '../../models/IAnnounceForPublic';
import { UserAnnounceStore } from '../../store/user-announce-store';

@Component({
  selector: 'app-edit-user-announce-dialog',
  templateUrl: './edit-user-announce-dialog.component.html',
  styleUrls: ['./edit-user-announce-dialog.component.scss'],
})
export class EditUserAnnounceDialogComponent implements OnInit,OnDestroy {
  title: string;
  mode: 'create' | 'update';
  item: IAnnounceForPublic;
  subscription: Subscription = Subscription.EMPTY;
  showTextEditor: boolean = false;
  announceForm: FormGroup;
  formControls: any;
  user:IUser;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditUserAnnounceDialogComponent>,
    private userAnnouncestore: UserAnnounceStore,
    public announceContentTypeStore: AnnounceContentTypeStore,
    private fb: FormBuilder,
    public helperService: HelperService,
    private adminHubService:AdminHubService
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data?.item;
    this.user=data?.user;

    this.formControls = {
      header: ['', [Validators.required, Validators.maxLength(140)]],
      contentType: ['', Validators.required],
      content: [''],
    };

    if (this.mode == 'create') {
      this.announceForm = this.fb.group({ ...this.formControls });
    } else if (this.mode == 'update') {
      this.announceForm = this.fb.group(this.formControls);
      this.announceForm.patchValue({ ...this.item });
      this.announceForm.get('contentType').disable();
      const contentType = this.item?.contentType.toLowerCase();
      if (
        contentType == 'deathannounce' ||
        contentType == 'bloodannounce' ||
        contentType == 'generalannounce'
      ) {
        this.showTextEditor = true;
      }
    }
  }

  ngOnInit(): void {
    const contentType = this.item?.contentType.toLowerCase();
    if (contentType == 'video') {
      this.announceForm.get('slideIntervalTime')?.disable();
    }
    this.subscription = this.announceForm
      .get('contentType')
      .valueChanges.subscribe((result: string) => {
        const res: string = result.toLowerCase();
        if (res == 'image' || res == 'video' || res == 'gif') {
          this.showTextEditor = false;
        } else {
          this.showTextEditor = true;
        }
      });
  }

  get f() {
    return this.announceForm.controls;
  }

  onSubmit() {
    if (this.announceForm.valid) {
      if (this.mode == 'create') {
        const model: IAnnounceForPublic = {
          ...this.announceForm?.value,
          userId:this.user?.userId
        };
        this.userAnnouncestore.create(model,this.user?.userId);
        this.dialogRef.close();
      } else if (this.mode == 'update') {
        const model: IAnnounceForPublic = {
          ...this.item,
          ...this.announceForm.value,
        };
        this.userAnnouncestore.update(model,this.user?.userId);
        this.dialogRef.close();
      }
    }
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
