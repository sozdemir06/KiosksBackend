import { Component, OnInit, Input } from '@angular/core';
import { IHomeAnnouncePhoto } from 'src/app/shared/models/IHomeAnnouncePhoto';
import { MatDialog } from '@angular/material/dialog';
import { VehilceAnnounceStore } from 'src/app/core/services/stores/vehicle-announce-store';
import { IVehicleAnnouncePhoto } from 'src/app/shared/models/IVehicleAnnouncePhoto';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-vehicle-announce-photo-list',
  templateUrl: './vehicle-announce-photo-list.component.html',
  styleUrls: ['./vehicle-announce-photo-list.component.scss'],
})
export class VehicleAnnouncePhotoListComponent implements OnInit {
  @Input() images: IHomeAnnouncePhoto[];
  @Input() announceId: number;

  @Input() roleForUpdate: string[] = [];
  @Input() roleForDelete: string[] = [];
  constructor(
    private dialog: MatDialog,
    private vehicleStore: VehilceAnnounceStore
  ) {}

  ngOnInit(): void {}

  onUnConfirm(image: IVehicleAnnouncePhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Fotoğrafı yayın dışında tutmak istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IVehicleAnnouncePhoto = {
          ...image,
          isConfirm: false,
        };
        this.vehicleStore.updatePhoto(photo);
      }
    });
  }

  onConfirm(image: IVehicleAnnouncePhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Fotoğrafı onaylamak istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IVehicleAnnouncePhoto = {
          ...image,
          isConfirm: true,
        };
        this.vehicleStore.updatePhoto(photo);
      }
    });
  }

  onDelete(image: IVehicleAnnouncePhoto) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Fotoğrafı silmek istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.vehicleStore.deletePhoto(image);
      }
    });
  }
}
