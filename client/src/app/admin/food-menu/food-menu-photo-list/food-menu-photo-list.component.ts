import { Component, OnInit, Input } from '@angular/core';
import { IFoodMenuPhoto } from 'src/app/shared/models/IFoodMenuPhoto';
import { MatDialog } from '@angular/material/dialog';
import { FoodMenuStore } from 'src/app/core/services/stores/food-menu-store';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-food-menu-photo-list',
  templateUrl: './food-menu-photo-list.component.html',
  styleUrls: ['./food-menu-photo-list.component.scss'],
})
export class FoodMenuPhotoListComponent implements OnInit {
  @Input() images: IFoodMenuPhoto[];
  @Input() announceId: number;

  @Input() roleForUpdate: string[] = ["Sudo","FoodMenu.Update","FoodMenu.All"];
  @Input() roleForDelete: string[] = ["Sudo","FoodMenu.Delete","FoodMenu.All"];
  constructor(
    private dialog: MatDialog,
    private foodMenuStore: FoodMenuStore
  ) {}

  ngOnInit(): void {}

  onUnConfirm(image: IFoodMenuPhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Fotoğrafı yayın dışında tutmak istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IFoodMenuPhoto = {
          ...image,
          isConfirm: false,
        };
        this.foodMenuStore.updatePhoto(photo);
      }
    });
  }

  onConfirm(image: IFoodMenuPhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Fotoğrafı onaylamak istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IFoodMenuPhoto = {
          ...image,
          isConfirm: true,
        };
        this.foodMenuStore.updatePhoto(photo);
      }
    });
  }

  onSetBackground(image: IFoodMenuPhoto) {
    
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message:
          'Fotoğrafı Bu menu için arka plan resmi olarak atamak istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IFoodMenuPhoto = {
          ...image,
          isSetBackground: image.isSetBackground ?false:true
        };
        this.foodMenuStore.updatePhoto(photo);
      }
    });
  }

  onDelete(image: IFoodMenuPhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Fotoğrafı silmek istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.foodMenuStore.deletePhoto(image);
      }
    });
  }

  onReject(image:IFoodMenuPhoto){
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Fotoğrafı Ret Etmek istiyormusunuz.? ',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IFoodMenuPhoto = {
          ...image,
          unConfirm:true
        };
       this.foodMenuStore.updatePhoto(photo);
      }
    });
  }

  onUnReject(image:IFoodMenuPhoto){
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Fotoğrafı Ret Etmekten  Vazgeçmek istiyormusunuz.? ',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IFoodMenuPhoto = {
          ...image,
          unConfirm:false
        };
       this.foodMenuStore.updatePhoto(photo);
      }
    });
  }
}
