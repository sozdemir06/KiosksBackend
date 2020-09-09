import { Component, OnInit, Inject } from '@angular/core';
import { IScreen } from 'src/app/shared/models/IScreen';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialog } from '@angular/material/dialog';
import { ScreenStore } from 'src/app/core/services/stores/screen-store';
import { IScreenHeaderPhoto } from 'src/app/shared/models/IScreenHeaderPhoto';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-edit-screen-header-photo',
  templateUrl: './edit-screen-header-photo.component.html',
  styleUrls: ['./edit-screen-header-photo.component.scss']
})
export class EditScreenHeaderPhotoComponent implements OnInit {
  title: string;
  item: IScreen;
  item$:Observable<IScreen>;

  mode: 'create' | 'update';

  roleForUpdate: string[] = ["Sudo","ScreenHeaderPhoto.Update","Screen.All"];
  roleForDelete: string[] = ["Sudo","ScreenHeaderPhoto.Delete","Screen.All"];
  roleForCreate: string[] = ["Sudo","ScreenHeaderPhoto.Create","Screen.All"];


  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditScreenHeaderPhotoComponent>,
    private screenStore: ScreenStore,
    private dialog:MatDialog
  ) {
    this.title = data?.title;
    this.item = data?.item;
    this.mode=data?.mode;
  }



  ngOnInit(): void {
    this.item$=this.screenStore.screens$.pipe(
      map(screens=>screens.find(x=>x.id==this.item.id))
    )
  }

  onUnConfirm(image: IScreenHeaderPhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: "Bu fotoğraf bulunan bölgeden kaldırılacak...",
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IScreenHeaderPhoto = {
          ...image,
          isMain: false,
        };
        this.screenStore.setIsMainScreenHeaderPhoto(photo);
      }
    });
  }

  onSetPosition(image:IScreenHeaderPhoto,position:string){
    const pos:string=position.toLowerCase()=='left'?"Sol üst logo":"Sağ üst logo";

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message:"Bu fotoğrafı "+pos+" olarak ayarlamak istiyormusunuz.?"
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IScreenHeaderPhoto = {
          ...image,
          position:position.toLowerCase()
        };
        this.screenStore.updateScreenHeaderPhoto(photo);
      }
    });
  } 

  onConfirm(image: IScreenHeaderPhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message:"Bu fotoğraf belirlenen bölge için ana resim olarak atamak onaylıyormusunuz.?",
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IScreenHeaderPhoto = {
          ...image,
          isMain: true,
        };
        this.screenStore.setIsMainScreenHeaderPhoto(photo);
      }
    });
  }

  onDelete(image: IScreenHeaderPhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Fotoğrafı silmek istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.screenStore.deleteScreenHeaderPhoto(image);
      }
    });
  }

  uploadResult(photo:IScreenHeaderPhoto){
    this.screenStore.crateScreenHeaderPhoto(photo,this.item.id);
  }


}
