import { Component, OnInit, Input } from '@angular/core';
import { IHomeAnnouncePhoto } from 'src/app/shared/models/IHomeAnnouncePhoto';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { HomeAnnounceStore } from 'src/app/core/services/stores/home-announce-store';

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
    private dialog:MatDialog,
    private homeAnnounceStore:HomeAnnounceStore
  ) { }

  ngOnInit(): void {
  }

  onUnConfirm(image:IHomeAnnouncePhoto){
      const photo:IHomeAnnouncePhoto={
         ...image,
         isConfirm:false
      }
      this.homeAnnounceStore.updatePhoto(photo);
  }

  onConfirm(image:IHomeAnnouncePhoto){
    const photo:IHomeAnnouncePhoto={
      ...image,
      isConfirm:true
    }
    this.homeAnnounceStore.updatePhoto(photo);
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
        this.homeAnnounceStore.deletePhoto(image);
      }
    })
      
  }



}
