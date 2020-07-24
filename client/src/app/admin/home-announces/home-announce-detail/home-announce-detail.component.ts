import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { IHomeAnnounce } from 'src/app/shared/models/IHomeAnnounce';
import { Observable } from 'rxjs';
import { HomeAnnounceStore } from 'src/app/core/services/stores/home-announce-store';
import { IHomeAnnouncePhoto } from 'src/app/shared/models/IHomeAnnouncePhoto';
import { HomeAnnouncePhotoStore } from 'src/app/core/services/stores/home-announce-photo-store';
import { MatDialog } from '@angular/material/dialog';
import { EditHomeAnnounceDialogComponent } from '../edit-home-announce-dialog/edit-home-announce-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-home-announce-detail',
  templateUrl: './home-announce-detail.component.html',
  styleUrls: ['./home-announce-detail.component.scss'],
})
export class HomeAnnounceDetailComponent implements OnInit {
  homeAnnounce$: Observable<IHomeAnnounce>;
  announceId: number;
  roleForUpdate:string[]=["Sudo","HomeAnnounces.Update","HomeAnnounces.All"]
  roleForPublish:string[]=["Sudo","HomeAnnounces.Publish","HomeAnnounces.All"]
  roleForAddPhoto:string[]=["Sudo","HomeAnnouncePhotos.Create","HomeAnnounces.All"];
  roleForUpdatePhoto:string[]=["Sudo","HomeAnnouncePhotos.Update","HomeAnnounces.All"];
  roleForDeletePhoto:string[]=["Sudo","HomeAnnouncePhotos.Delete","HomeAnnounces.All"];
  
  constructor(
    private route: ActivatedRoute,
    private location: Location,
    public homeAnnounceStore: HomeAnnounceStore,
    public homeAnnouncePhotoStore: HomeAnnouncePhotoStore,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    const announceId: number = +this.route.snapshot.paramMap.get('id');
    this.announceId = announceId;
    this.homeAnnounce$ = this.homeAnnounceStore.getAnnounceById(announceId);
  }

  goBack() {
    this.location.back();
  }

  uploadResult(model: IHomeAnnouncePhoto) {
    this.homeAnnouncePhotoStore.create(this.announceId, model);
  }

  onUpdate(announce: IHomeAnnounce) {
    this.dialog.open(EditHomeAnnounceDialogComponent, {
      width: '55rem',
      maxHeight: '100vh',
      data: {
        title: 'İlanı Güncelle',
        mode: 'update',
        item: announce,
      },
    });
  }
  onDelete() {}

  unPublish(announce:IHomeAnnounce) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'İlanı yayından kaldırmak istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: IHomeAnnounce = {
          ...announce,
          isNew: false,
          reject: false,
          isPublish: false
        };
        this.homeAnnounceStore.publish(model);
      }
    });
  }

  onPublish(announce: IHomeAnnounce) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'İlanı yayınlamak  istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: IHomeAnnounce = {
          ...announce,
          isNew: false,
          reject: false,
          isPublish: true,
        };
        this.homeAnnounceStore.publish(model);
      }
    });
  }

  onReject(announce:IHomeAnnounce) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'İlanı Reddetmek istoyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: IHomeAnnounce = {
          ...announce,
          isNew: false,
          reject: true,
          isPublish: false
        };
        this.homeAnnounceStore.publish(model);
      }
    });
  }
}
