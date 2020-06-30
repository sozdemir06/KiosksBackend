import { Component, OnInit, Inject } from '@angular/core';
import { INumberOfRoom } from 'src/app/shared/models/INumberOFRoom';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NumberOfroomStore } from 'src/app/core/services/stores/number-of-rrom-store';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-number-of-rooms-dialog',
  templateUrl: './edit-number-of-rooms-dialog.component.html',
  styleUrls: ['./edit-number-of-rooms-dialog.component.scss'],
})
export class EditNumberOfRoomsDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  item: INumberOfRoom;

  numberOfRoomForm: FormGroup;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditNumberOfRoomsDialogComponent>,
    private numberOfroomStore: NumberOfroomStore,
    private fb: FormBuilder
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data?.item;

    const formControls = {
      name: ['', [Validators.required, Validators.maxLength(60)]],
    };
    if (this.mode == 'create') {
      this.numberOfRoomForm = this.fb.group({ ...formControls });
    } else if (this.mode == 'update') {
      this.numberOfRoomForm = this.fb.group(formControls);
      this.numberOfRoomForm.patchValue({ ...this.item });
    }
  }
  get f() {
    return this.numberOfRoomForm.controls;
  }

  ngOnInit(): void {}
  onSubmitform() {
    if (this.numberOfRoomForm.valid) {
      const model: INumberOfRoom = {
        ...this.numberOfRoomForm.value,
      };
      if (this.mode == 'create') {
        this.numberOfroomStore.create(model);
        this.dialogRef.close();
      } else if (this.mode == 'update') {
        const model: INumberOfRoom = {
          ...this.numberOfRoomForm.value,
          id: this.item?.id,
        };
        this.numberOfroomStore.update(model);
        this.dialogRef.close();
      }
    }
  }
}
