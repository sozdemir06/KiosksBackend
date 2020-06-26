import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss']
})
export class ConfirmDialogComponent implements OnInit {
message:string="İşlemi onaylıyor musunuz.?";

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>
  ) { 
    this.message=data?.message?data?.message:this.message;
  }

  ngOnInit(): void {
  }


  onOk(){
    this.dialogRef.close(true);
  }

  OnCancel(){
    this.dialogRef.close(false);
  }

}
