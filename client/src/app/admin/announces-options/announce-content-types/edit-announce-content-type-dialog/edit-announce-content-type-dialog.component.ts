import { Component, OnInit, Inject } from '@angular/core';
import { IAnnounceContentType } from 'src/app/shared/models/IAnnounceContentType';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AnnounceContentTypeStore } from 'src/app/core/services/stores/announce-content-type-store';

@Component({
  selector: 'app-edit-announce-content-type-dialog',
  templateUrl: './edit-announce-content-type-dialog.component.html',
  styleUrls: ['./edit-announce-content-type-dialog.component.scss'],
})
export class EditAnnounceContentTypeDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  item: IAnnounceContentType;

  announceContentTypeForm: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditAnnounceContentTypeDialogComponent>,
    private fb: FormBuilder,
    private announceContentTypeStore: AnnounceContentTypeStore
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data?.item;

    const formControls = {
      name: ['', [Validators.required, Validators.maxLength(60)]],
      description: ['', [Validators.required]],
    };

    if (this.mode == 'create') {
      this.announceContentTypeForm = this.fb.group({ ...formControls });
    } else if (this.mode == 'update') {
      this.announceContentTypeForm = this.fb.group(formControls);
      this.announceContentTypeForm.patchValue({ ...this.item });
    }
  }

  get f() {
    return this.announceContentTypeForm.controls;
  }

  ngOnInit(): void {}

  onSubmitform() {
     if(this.announceContentTypeForm.valid){
       if(this.mode=="create"){
         const model:IAnnounceContentType={
           ...this.announceContentTypeForm.value
         }
         this.announceContentTypeStore.create(model);
         this.dialogRef.close();
       }else if(this.mode=="update"){
         const model:IAnnounceContentType={
           ...this.announceContentTypeForm.value,
           id:this.item?.id
         }
         this.announceContentTypeStore.update(model);
         this.dialogRef.close();
       }
     }
  }
}
