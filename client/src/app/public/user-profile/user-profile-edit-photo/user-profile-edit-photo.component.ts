import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { IUserList } from 'src/app/shared/models/IUser';
import { IUserPhoto } from 'src/app/shared/models/IUserPhoto';
import { PublicStore } from '../../store/public-store';
import { PublicUserStore } from '../../store/public-user-store';

@Component({
  selector: 'app-user-profile-edit-photo',
  templateUrl: './user-profile-edit-photo.component.html',
  styleUrls: ['./user-profile-edit-photo.component.scss']
})
export class UserProfileEditPhotoComponent implements OnInit {
  @Input() images:IUserPhoto[];
  @Input() user:IUserList;
 

  constructor(
    private publicUserStore:PublicUserStore,
    private dialog:MatDialog
  ) { }

  ngOnInit(): void {
  }



  onMainPhoto(image:IUserPhoto){
     const dialogRef=this.dialog.open(ConfirmDialogComponent,{
       width:"50rem",
       data:{
         message:"Bu Fotoğrafı profil fotoğrafı yapmak istiyormusunuz.?"
       }
     });
     
     dialogRef.afterClosed().subscribe(result=>{
       if(result){
         const photo:IUserPhoto={
           ...image,
           isMain:true
         }
         this.publicUserStore.makeMainPhoto(photo,this.user?.id);
       }
     })
  }

  onConfirm(image:IUserPhoto){

  }

}
