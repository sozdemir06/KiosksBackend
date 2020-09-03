import { Component, OnInit, Input } from '@angular/core';
import { IFoodMenuPhoto } from 'src/app/shared/models/IFoodMenuPhoto';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { FoodMenuBgPhotoStore } from 'src/app/core/services/stores/food-menu-bg-photo-store';
import { IFoodMenuBgPhoto } from 'src/app/shared/models/IFoodMenuBgPhoto';

@Component({
  selector: 'app-background-photo-list',
  templateUrl: './background-photo-list.component.html',
  styleUrls: ['./background-photo-list.component.scss']
})
export class BackgroundPhotoListComponent implements OnInit {

  @Input() roleForUpdate: string[] = ['Sudo','FoodMenuPhotos.Update','FoodMenu.All'];
  @Input() roleForCreate: string[] = ['Sudo','FoodMenuPhotos.Create','FoodMenu.All'];
  @Input() roleForDelete: string[] =['Sudo','FoodMenuPhotos.Delete','FoodMenu.All'];
  constructor(
    private dialog: MatDialog,
    public foodMenuBgStore:FoodMenuBgPhotoStore
  ) {}


  ngOnInit(): void {
  }

  uploadResult(model: IFoodMenuBgPhoto) {
    this.foodMenuBgStore.addBgPhoto(model);
  }

  onSetBackground(image: IFoodMenuBgPhoto) {
    
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message:
          'Fotoğrafı Ana Arka plan resmi olarak atamak istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IFoodMenuBgPhoto = {
          ...image,
          isSetBackground: image.isSetBackground ?false:true
        };
        this.foodMenuBgStore.setPhotoAsBackground(photo);
      }
    });
  }

  onDelete(image: IFoodMenuBgPhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Fotoğrafı silmek istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.foodMenuBgStore.deleteBgPhoto(image);
      }
    });
  }

}
