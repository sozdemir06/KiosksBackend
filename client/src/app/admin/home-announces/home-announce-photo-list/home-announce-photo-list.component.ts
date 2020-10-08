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

@Input() roleForUpdate:string[]=["Sudo","HomeAnnounces.Update","HomeAnnounces.All"];
@Input() roleForDelete:string[]=["Sudo","HomeAnnounces.Delete","HomeAnnounces.All"];

  constructor(
    private dialog:MatDialog,
    private homeAnnounceStore:HomeAnnounceStore
  ) { }

  ngOnInit(): void {
  }

  onUnConfirm(image: IHomeAnnouncePhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message:"Fotoğrafı yayın dışında tutmak istiyormusunuz.?",
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IHomeAnnouncePhoto = {
          ...image,
          isConfirm: false,
        };
        this.homeAnnounceStore.updatePhoto(photo);
      }
    });
  }

  onConfirm(image: IHomeAnnouncePhoto) {

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message:"Fotoğrafı Onaylamak istiyormusunuz.?" 
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IHomeAnnouncePhoto = {
          ...image,
          isConfirm: true,
        };
       this.homeAnnounceStore.updatePhoto(photo);
      }
    });
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

  onReject(image:IHomeAnnouncePhoto){
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Fotoğrafı Ret Etmek istiyormusunuz.? ',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IHomeAnnouncePhoto = {
          ...image,
          unConfirm:true
        };
       this.homeAnnounceStore.updatePhoto(photo);
      }
    });
  }

  onUnReject(image:IHomeAnnouncePhoto){
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Fotoğrafı Ret Etmekten  Vazgeçmek istiyormusunuz.? ',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IHomeAnnouncePhoto = {
          ...image,
          unConfirm:false
        };
       this.homeAnnounceStore.updatePhoto(photo);
      }
    });
  }



}
