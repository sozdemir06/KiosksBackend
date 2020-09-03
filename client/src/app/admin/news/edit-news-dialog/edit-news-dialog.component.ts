import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { INews } from 'src/app/shared/models/INews';
import { Subscription } from 'rxjs';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NewsStore } from 'src/app/core/services/stores/news-store';
import { HelperService } from 'src/app/core/services/helper-service';
import { UserStore } from 'src/app/core/services/stores/user-store';

@Component({
  selector: 'app-edit-news-dialog',
  templateUrl: './edit-news-dialog.component.html',
  styleUrls: ['./edit-news-dialog.component.scss'],
})
export class EditNewsDialogComponent implements OnInit, OnDestroy {
  title: string;
  mode: 'create' | 'update';
  item: INews;
  subscription: Subscription = Subscription.EMPTY;
  showTextEditor: boolean = false;
  newsForm: FormGroup;
  formControls: any;
  contentType: { name: string; description: string }[] = [
    { name: 'Image', description: 'Fotoğraf' },
    { name: 'TextAndImage', description: 'Metin ve Fotoğraf' },
    { name: 'Text', description: 'Sadece Metin' },
  ];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditNewsDialogComponent>,
    private newsStore: NewsStore,
    private fb: FormBuilder,
    public helperService: HelperService,
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
      newsDate: [
        this.helperService.dateToLocaleFormat(new Date()),
        Validators.required,
      ],
      newsAgency: [''],

      slideIntervalTime: [9, Validators.required],
    };

    if (this.mode == 'create') {
      this.newsForm = this.fb.group({ ...this.formControls });
    } else if (this.mode == 'update') {
      this.newsForm = this.fb.group(this.formControls);
      this.newsForm.patchValue({ ...this.item });
      this.newsForm.get('contentType').disable();
      const contenType = this.item?.contentType.toLowerCase();

      if (contenType == 'text' || contenType == 'textandimage') {
        this.showTextEditor = true;
      }
    }
  }

  ngOnInit(): void {
    this.subscription = this.newsForm
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
    return this.newsForm.controls;
  }

  onChangeStartDate(event) {
    this.newsForm.patchValue({
      publishStartDate: this.helperService.dateToLocaleFormat(event.value),
    });
  }

  onChangeNewsDate(event){
    this.newsForm.patchValue({
      newsDate: this.helperService.dateToLocaleFormat(event.value),
    });
  }

  onChangeFinishDate(event) {
    this.newsForm.patchValue({
      publishFinishDate: this.helperService.dateToLocaleFormat(event.value),
    });
  }

  onSubmit() {
    if (this.newsForm.valid) {
      const startDate = this.newsForm.get('publishStartDate').value;
      const finishDate = this.newsForm.get('publishFinishDate').value;

      const checkDate = this.helperService.checkPublishDate(
        startDate,
        finishDate
      );

      if (checkDate) {
        if (this.mode == 'create') {
          const model: INews = {
            ...this.newsForm?.value,
            isNew: true,
            isPublish: false,
            reject: false,
          };
          this.newsStore.create(model);
          this.dialogRef.close();
        } else if (this.mode == 'update') {
          const model: INews = {
            ...this.item,
            ...this.newsForm.value,
          };
          this.newsStore.update(model);
          this.dialogRef.close();
        }
      }
    }
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
