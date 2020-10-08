import { AfterViewInit,  Component, Inject, Input, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { UserStore } from 'src/app/core/services/stores/user-store';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { IUserList } from 'src/app/shared/models/IUser';
import { IUserPhoto } from 'src/app/shared/models/IUserPhoto';

@Component({
  selector: 'app-user-photo-list',
  templateUrl: './user-photo-list.component.html',
  styleUrls: ['./user-photo-list.component.scss'],
})
export class UserPhotoListComponent implements OnInit,AfterViewInit {
user:IUserList;
user$:Observable<IUserList>;

roleForAddPhoto: string[] = [
  'Sudo',
  'User.Create',
  'User.All',
];
  @Input() roleForUpdate: string[] = ["Sudo","User.Update","User.All"];
  @Input() roleForDelete: string[] = ["Sudo","User.Delete","User.All"];
  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    private dialog: MatDialog,
    private matDialogRef:MatDialogRef<UserPhotoListComponent>,
    private userStore: UserStore,
  ) {
    this.user=data?.user;
  }

  ngOnInit(): void {}
  ngAfterViewInit(){
    setTimeout(()=>{
      this.user$=this.userStore.getuserById(this.user?.id);
    })
  }
  uploadResult(photo:IUserPhoto){
    this.userStore.addPhoto(photo);
  }

  onUnConfirm(image: IUserPhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message:"Fotoğrafı yayın dışı bırakmak istiyormusunuz?",
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IUserPhoto = {
          ...image,
          isConfirm: false,
        };
        this.userStore.updatePhoto(photo);
        this.matDialogRef.close();
        
      }
    });
  }

  onConfirm(image: IUserPhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message:"Fotoğrafı onaylamak istiyormusunuz?.",
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IUserPhoto = {
          ...image,
          isConfirm: true,
        };
       this.userStore.updatePhoto(photo);
       this.matDialogRef.close();
      }
    });
  }

  onDelete(image: IUserPhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Fotoğrafı silmek istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.userStore.deletePhoto(image);
        this.matDialogRef.close();
      }
    });
  }

  onReject(image:IUserPhoto){
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Fotoğrafı Ret Etmek istiyormusunuz.? ',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IUserPhoto = {
          ...image,
          unConfirm:true
        };
       this.userStore.updatePhoto(photo);
       this.matDialogRef.close();
      }
    });
  }

  onUnReject(image:IUserPhoto){
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Fotoğrafı Ret Etmekten  Vazgeçmek istiyormusunuz.? ',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IUserPhoto = {
          ...image,
          unConfirm:false
        };
       this.userStore.updatePhoto(photo);
       this.matDialogRef.close();
      }
    });
  }


}
