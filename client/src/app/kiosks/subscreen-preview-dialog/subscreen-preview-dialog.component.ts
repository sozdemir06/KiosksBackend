import { AfterViewInit, Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { KiosksStore } from '../store/kiosks-store';

@Component({
  selector: 'app-subscreen-preview-dialog',
  templateUrl: './subscreen-preview-dialog.component.html',
  styleUrls: ['./subscreen-preview-dialog.component.scss']
})
export class SubscreenPreviewDialogComponent implements OnInit,AfterViewInit {
subscreen:ISubScreen;
screenId:number;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    private kiosksStore:KiosksStore
  ) {
    this.subscreen=data?.subscreen;
    this.screenId=data?.screenId;
   }
ngAfterViewInit(){
  setTimeout(()=>{
    this.kiosksStore.getListByScreenId(this.screenId);
  })
  
}

  ngOnInit(): void {
   
  }

}
