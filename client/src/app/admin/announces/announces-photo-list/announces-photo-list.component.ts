import { Component, OnInit, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { IAnnouncePhoto } from 'src/app/shared/models/IAnnouncePhoto';
import { AnnounceStore } from 'src/app/core/services/stores/announce-store';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-announces-photo-list',
  templateUrl: './announces-photo-list.component.html',
  styleUrls: ['./announces-photo-list.component.scss']
})
export class AnnouncesPhotoListComponent implements OnInit {

  @Input() images: IAnnouncePhoto[];
  @Input() announceId: number;

  @Input() roleForUpdate: string[] = [];
  @Input() roleForDelete: string[] = [];
  constructor(
    private dialog: MatDialog,
    private announceStore: AnnounceStore
  ) {}

  ngOnInit(): void {}

  onUnConfirm(image: IAnnouncePhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: image.fileType.toLowerCase()=='image'?'Fotoğrafı':'Video '+'yayın dışında tutmak istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IAnnouncePhoto = {
          ...image,
          isConfirm: false,
        };
        this.announceStore.updatePhoto(photo);
      }
    });
  }

  onConfirm(image: IAnnouncePhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: image.fileType.toLowerCase()=='image'?'Fotoğrafı':'Video '+'onaylamak istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IAnnouncePhoto = {
          ...image,
          isConfirm: true,
        };
       this.announceStore.updatePhoto(photo);
      }
    });
  }

  onDelete(image: IAnnouncePhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Fotoğrafı silmek istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.announceStore.deletePhoto(image);
      }
    });
  }

}
