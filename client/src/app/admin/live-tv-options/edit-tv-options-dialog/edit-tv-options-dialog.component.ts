import { Component, OnInit, Inject } from '@angular/core';
import { ILiveTvList } from 'src/app/shared/models/ILiveTvList';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { LiveTvListStore } from 'src/app/core/services/stores/live-tv-list-store';

@Component({
  selector: 'app-edit-tv-options-dialog',
  templateUrl: './edit-tv-options-dialog.component.html',
  styleUrls: ['./edit-tv-options-dialog.component.scss']
})
export class EditTvOptionsDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  item: ILiveTvList;

  liveTvListForm: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditTvOptionsDialogComponent>,
    private fb: FormBuilder,
    private liveTvListStore: LiveTvListStore
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data?.item;

    const formControls = {
      tvName: ['', [Validators.required, Validators.maxLength(60)]],
      youtubeId:["",Validators.required]
    };

    if (this.mode == 'create') {
      this.liveTvListForm = this.fb.group({ ...formControls });
    } else if (this.mode == 'update') {
      this.liveTvListForm = this.fb.group(formControls);
      this.liveTvListForm.patchValue({ ...this.item });
    }
  }

  get f() {
    return this.liveTvListForm.controls;
  }

  ngOnInit(): void {
  }

  onSubmitform(){
    if (this.liveTvListForm.valid) {
      if (this.mode == 'create') {
        const model: ILiveTvList = {
          ...this.liveTvListForm.value,
        };
        this.liveTvListStore.create(model);
        this.dialogRef.close();
      } else if (this.mode == 'update') {
        const model: ILiveTvList = {
          ...this.liveTvListForm.value,
          id:this.item?.id
        };
        this.liveTvListStore.update(model);
        this.dialogRef.close();
      }
    }
  }

}
