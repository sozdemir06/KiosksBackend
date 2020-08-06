import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { HomeAnnounceStore } from 'src/app/core/services/stores/home-announce-store';
import { IHomeAnnouncePhoto } from 'src/app/shared/models/IHomeAnnouncePhoto';
import { MatDialog } from '@angular/material/dialog';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { IHomeAnnounceSubScreen } from 'src/app/shared/models/IHomeAnnounceSubScreen';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-home-announce-detail',
  templateUrl: './home-announce-detail.component.html',
  styleUrls: ['./home-announce-detail.component.scss'],
})
export class HomeAnnounceDetailComponent implements OnInit {
  announceId: number;

  roleForAddPhoto: string[] = [
    'Sudo',
    'HomeAnnouncePhotos.Create',
    'HomeAnnounces.All',
  ];
  roleForUpdatePhoto: string[] = [
    'Sudo',
    'HomeAnnouncePhotos.Update',
    'HomeAnnounces.All',
  ];
  roleForDeletePhoto: string[] = [
    'Sudo',
    'HomeAnnouncePhotos.Delete',
    'HomeAnnounces.All',
  ];

  roleForAddSubScreen:string[]=['Sudo',"HomeAnnounceSubScreens.Create",'HomeAnnounces.All'];

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    public homeAnnounceStore: HomeAnnounceStore,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.announceId = +this.route.snapshot.paramMap.get('id');
    this.homeAnnounceStore.getDetail(this.announceId);
  }

  goBack() {
    this.location.back();
  }

  uploadResult(model: IHomeAnnouncePhoto) {
    this.homeAnnounceStore.addPhoto(model);
  }

  onSelectSubScreen(subscreen: ISubScreen) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: `Bu Ev ilanını ${subscreen.name} adlı ekranda yayınlamak istiyormusunuz?`,
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: Partial<IHomeAnnounceSubScreen> = {
          homeAnnounceId: this.announceId,
          subScreenId: subscreen?.id,
          screenId: subscreen?.screenId,
        };

        this.homeAnnounceStore.addSubScreen(model);
      }
    });
  }
}
