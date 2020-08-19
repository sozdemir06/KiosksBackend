import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AnnounceStore } from 'src/app/core/services/stores/announce-store';
import { MatDialog } from '@angular/material/dialog';
import { Location } from '@angular/common';
import { IAnnouncePhoto } from 'src/app/shared/models/IAnnouncePhoto';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { IAnnounceSubScreen } from 'src/app/shared/models/IAnnounceSubScreen';
import { HelperService } from 'src/app/core/services/helper-service';

@Component({
  selector: 'app-announces-detail',
  templateUrl: './announces-detail.component.html',
  styleUrls: ['./announces-detail.component.scss'],
})
export class AnnouncesDetailComponent implements OnInit {
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
    public announceStore: AnnounceStore,
    private location: Location,
    private dialog: MatDialog,
    public helperService:HelperService
  ) {}

  goBack() {
    this.location.back();
  }
  ngOnInit(): void {
    this.announceId=+this.route.snapshot.paramMap.get("id");
    this.announceStore.getDetail(this.announceId);
  }


  uploadResult(photo:IAnnouncePhoto){
    this.announceStore.addPhoto(photo);
  }

  onSelectSubScreen(subscreen: ISubScreen) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: `Bu duyuruyu ${subscreen.name} adlı ekranda yayınlamak istiyormusunuz?`,
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: Partial<IAnnounceSubScreen> = {
          announceId: this.announceId,
          subScreenId: subscreen?.id,
          screenId: subscreen?.screenId,
        };
        this.announceStore.addSubScreen(model);
      }
    });
  }
}
