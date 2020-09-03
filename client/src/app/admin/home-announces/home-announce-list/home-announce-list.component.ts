import { Component, OnInit, Input } from '@angular/core';
import { IHomeAnnounce } from 'src/app/shared/models/IHomeAnnounce';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { HomeAnnounceStore } from 'src/app/core/services/stores/home-announce-store';
import { MatDialog } from '@angular/material/dialog';
import { EditHomeAnnounceDialogComponent } from '../edit-home-announce-dialog/edit-home-announce-dialog.component';
import { HelperService } from 'src/app/core/services/helper-service';
import { NotifyService } from 'src/app/core/services/notify-service';

@Component({
  selector: 'app-home-announce-list',
  templateUrl: './home-announce-list.component.html',
  styleUrls: ['./home-announce-list.component.scss'],
})
export class HomeAnnounceListComponent implements OnInit {
  displayedColumns: string[] = [
    'Image',
    'Header',
    'Created',
    'PublishDates',
    'Price',
    'PublishStatus',
    'Actions',
  ];
  @Input() dataSource: IHomeAnnounce[];
  roleForUpdate: string[] = [
    'Sudo',
    'HomeAnnounces.Update',
    'HomeAnnounces.All',
  ];
  roleForPublish: string[] = [
    'Sudo',
    'HomeAnnounces.Publish',
    'HomeAnnounces.All',
  ];
  constructor(
    private homeAnnounceStore: HomeAnnounceStore,
    private dialog: MatDialog,
    private helperService: HelperService,
    private notifyService: NotifyService
  ) {}

  ngOnInit(): void {}

  onUpdate(announce: IHomeAnnounce) {
    this.dialog.open(EditHomeAnnounceDialogComponent, {
      width: '60vw',
      maxHeight: '100vh',
      autoFocus: false,
      data: {
        title: 'İlanı Güncelle',
        mode: 'update',
        item: announce,
      },
    });
  }
  onDelete() {}

  unPublish(announce: IHomeAnnounce) {
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
          isPublish: false,
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

  onReject(announce: IHomeAnnounce) {
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
          isPublish: false,
        };
        this.homeAnnounceStore.publish(model);
      }
    });
  }
}
