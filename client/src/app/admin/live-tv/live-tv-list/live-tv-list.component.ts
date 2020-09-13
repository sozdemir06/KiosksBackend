import { Component, OnInit, Input } from '@angular/core';
import { ILiveTvBroadCast } from 'src/app/shared/models/ILiveTvBroadCast';
import { MatDialog } from '@angular/material/dialog';
import { LiveTvBroadCastStore } from 'src/app/core/services/stores/live-broadcast-store';
import { EditLiveTvDialogComponent } from '../edit-live-tv-dialog/edit-live-tv-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-live-tv-list',
  templateUrl: './live-tv-list.component.html',
  styleUrls: ['./live-tv-list.component.scss']
})
export class LiveTvListComponent implements OnInit {
  @Input() dataSource:ILiveTvBroadCast[];
  displayedColumns:string[]=['Image','Created','PublishDates','PublishStatus','Type','Actions'];
  
  roleForUpdate:string[]=["Sudo","LiveTvBroadCast.Update","LiveTvBroadCast.All"]
  roleForPublish:string[]=["Sudo","LiveTvBroadCast.Publish","LiveTvBroadCast.All"]
    constructor(
      private dialog:MatDialog,
      private liveTvBroadCastStore:LiveTvBroadCastStore
    ) { }

  ngOnInit(): void {
  }
  onUpdate(element:ILiveTvBroadCast){
    this.dialog.open(EditLiveTvDialogComponent,{
      width:"55rem",
      maxHeight:"100vh",
      autoFocus:false,
      data:{
        title:"Canlı Tv Yayın Güncelle",
        mode:"update",
        item:element
      }
    })    
  }

  onPublish(announce:ILiveTvBroadCast){
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Canlı Tv Yayınını yayınlamak istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: ILiveTvBroadCast = {
          ...announce,
          isNew: false,
          reject: false,
          isPublish: true,
        };
        this.liveTvBroadCastStore.publish(model);
      }
    });
  }
  unPublish(announce:ILiveTvBroadCast){
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Canlı Tv yayınını yayından kaldırmak istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: ILiveTvBroadCast = {
          ...announce,
          isNew: false,
          reject: false,
          isPublish: false
        };
        this.liveTvBroadCastStore.publish(model);
      }
    });
  }

  onReject(announce:ILiveTvBroadCast){
     const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Canlı tv yayınını Redetmek istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: ILiveTvBroadCast = {
          ...announce,
          isNew: false,
          reject: true,
          isPublish: false
        };
        this.liveTvBroadCastStore.publish(model);
      }
    });
  }

  onDelete(element:ILiveTvBroadCast){
    console.log("deleted");
  }

}
