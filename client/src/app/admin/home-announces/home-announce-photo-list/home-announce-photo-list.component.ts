import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IHomeAnnouncePhoto } from 'src/app/shared/models/IHomeAnnouncePhoto';
import { HomeAnnouncePhotoStore } from 'src/app/core/services/stores/home-announce-photo-store';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-home-announce-photo-list',
  templateUrl: './home-announce-photo-list.component.html',
  styleUrls: ['./home-announce-photo-list.component.scss']
})
export class HomeAnnouncePhotoListComponent implements OnInit {
@Input() images:IHomeAnnouncePhoto[];
@Input() announceId:number;

@Input() roleForUpdate:string[]=[];
@Input() roleForDelete:string[]=[];

  constructor(
    private homeAnnouncePhoto:HomeAnnouncePhotoStore,
    private dialog:MatDialog
  ) { }

  ngOnInit(): void {
  }

  onUnConfirm(image:IHomeAnnouncePhoto){
      const photo:IHomeAnnouncePhoto={
         ...image,
         isConfirm:false
      }
      this.homeAnnouncePhoto.update(this.announceId,photo);
  }

  onConfirm(image:IHomeAnnouncePhoto){
    const photo:IHomeAnnouncePhoto={
      ...image,
      isConfirm:true
    }
    this.homeAnnouncePhoto.update(this.announceId, photo);
  }

  onDelete(image:IHomeAnnouncePhoto){
    const dialogRef=this.dialog.open(ConfirmDialogComponent,{
      width:"45rem",
      data:{
        message:"Fotoğrafı silmek istiyormusunuz.?"
      }
    });
    dialogRef.afterClosed().subscribe(result=>{
      if(result){
        this.homeAnnouncePhoto.delete(this.announceId,image);
      }
    })
      
  }



}
