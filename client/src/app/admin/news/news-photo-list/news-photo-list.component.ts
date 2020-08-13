import { Component, OnInit, Input } from '@angular/core';
import { INewsPhoto } from 'src/app/shared/models/INewsPhoto';
import { MatDialog } from '@angular/material/dialog';
import { NewsStore } from 'src/app/core/services/stores/news-store';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-news-photo-list',
  templateUrl: './news-photo-list.component.html',
  styleUrls: ['./news-photo-list.component.scss']
})
export class NewsPhotoListComponent implements OnInit {
  @Input() images: INewsPhoto[];
  @Input() announceId: number;

  @Input() roleForUpdate: string[] = [];
  @Input() roleForDelete: string[] = [];
  constructor(
    private dialog: MatDialog,
    private newsStore: NewsStore
  ) {}

  ngOnInit(): void {}

  onUnConfirm(image: INewsPhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message:"Fotoğrafı yayın dışı bırakmak istiyormusunuz?",
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: INewsPhoto = {
          ...image,
          isConfirm: false,
        };
        this.newsStore.updatePhoto(photo);
      }
    });
  }

  onConfirm(image: INewsPhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message:"Fotoğrafı onaylamak istiyormusunuz?.",
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: INewsPhoto = {
          ...image,
          isConfirm: true,
        };
       this.newsStore.updatePhoto(photo);
      }
    });
  }

  onDelete(image: INewsPhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Fotoğrafı silmek istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.newsStore.deletePhoto(image);
      }
    });
  }


}
