import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { VehilceAnnounceStore } from 'src/app/core/services/stores/vehicle-announce-store';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { IVehicleAnnouncePhoto } from 'src/app/shared/models/IVehicleAnnouncePhoto';
import { Location } from '@angular/common';
import { IVehicleAnnounceSubScreen } from 'src/app/shared/models/IVehicleAnnounceSubScreen';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-vehicle-announce-detail',
  templateUrl: './vehicle-announce-detail.component.html',
  styleUrls: ['./vehicle-announce-detail.component.scss'],
})
export class VehicleAnnounceDetailComponent implements OnInit {
  announceId: number;
  roleForAddPhoto: string[] = [
    'Sudo',
    'VehicleAnnouncePhotos.Create',
    'VehicleAnnounces.All',
  ];
  roleForUpdatePhoto: string[] = [
    'Sudo',
    'VehicleAnnouncePhotos.Update',
    'VehicleAnnounces.All',
  ];
  roleForDeletePhoto: string[] = [
    'Sudo',
    'VehicleAnnouncePhotos.Delete',
    'VehicleAnnounces.All',
  ];

  roleForAddSubScreen: string[] = [
    'Sudo',
    'VehicleAnnounceSubScreens.Create',
    'VehicleAnnounces.All',
  ];


  constructor(
    private route: ActivatedRoute,
    public vehicleStore: VehilceAnnounceStore,
    private location: Location,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.announceId = +this.route.snapshot.paramMap.get('id');
    this.vehicleStore.getDetail(this.announceId);
  }

  goBack() {
    this.location.back();
  }

  uploadResult(model: IVehicleAnnouncePhoto) {
    this.vehicleStore.addPhoto(this.announceId, model);
  }

  onSelectSubScreen(subscreen: ISubScreen) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: `Bu araç ilanını ${subscreen.name} adlı ekranda yayınlamak istiyormusunuz?`,
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: Partial<IVehicleAnnounceSubScreen> = {
          vehicleAnnounceId: this.announceId,
          subScreenId: subscreen?.id,
          screenId: subscreen?.screenId,
        };
        this.vehicleStore.addSubScreen(model);
      }
    });
  }
}
