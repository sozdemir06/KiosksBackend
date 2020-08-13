import { Component, OnInit, Input } from '@angular/core';
import { IAnnounce } from 'src/app/shared/models/IAnnounce';
import { MatDialog } from '@angular/material/dialog';
import { EditAnnouncesDialogComponent } from '../edit-announces-dialog/edit-announces-dialog.component';
import { AnnounceStore } from 'src/app/core/services/stores/announce-store';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-announces-list',
  templateUrl: './announces-list.component.html',
  styleUrls: ['./announces-list.component.scss'],
})
export class AnnouncesListComponent implements OnInit {
  @Input() dataSource: IAnnounce[];
  displayedColumns: string[] = [
    'Image',
    'Header',
    'Created',
    'PublishDates',
    'ContentType',
    'PublishStatus',
    'Actions',
  ];

  roleForUpdate: string[] = [
    'Sudo',
    'Announces.Update',
    'Announces.All',
  ];
  roleForPublish: string[] = [
    'Sudo',
    'Announces.Publish',
    'Announces.All',
  ];
  constructor(
    private dialog:MatDialog,
    private announceStore:AnnounceStore
  ) {}

  ngOnInit(): void {}

  checkContentType(contentType: string): string {
    let type: string = '';

    switch (contentType.toLowerCase()) {
      case 'image':
        type = 'Fotoğraf';
        break;
      case 'video':
        type = 'Video';
        break;
      case 'deathannounce':
        type = 'Vefat Duyurusu';
        break;
      case 'bloodannounce':
        type = 'Kan Duyurusu';
        break;
      case 'generalannounce':
        type = 'Genel Duyuru';
        break;
      default:
        type = 'Yok';
        break;
    }
    return type;
  }
  onUpdate(element: IAnnounce) {
    this.dialog.open(EditAnnouncesDialogComponent,{
      width:"55rem",
      maxHeight:"100vh",
      autoFocus:false,
      data:{
        title:"Duyuruyu güncelle",
        mode:"update",
        item:element
      }
    })
  }

  onPublish(announce: IAnnounce) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Duyuruyu yayınlamak  istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: IAnnounce = {
          ...announce,
          isNew: false,
          reject: false,
          isPublish: true,
        };
        this.announceStore.publish(model);
      }
    });
  }
  unPublish(announce: IAnnounce) {
      const dialogRef = this.dialog.open(ConfirmDialogComponent, {
        width: '45rem',
        data: {
          message: 'Duyuruyu yayından kaldırmak istiyormusunuz.?',
        },
      });
      dialogRef.afterClosed().subscribe((result) => {
        if (result) {
          const model: IAnnounce = {
            ...announce,
            isNew: false,
            reject: false,
            isPublish: false
          };
          this.announceStore.publish(model);
        }
      });
  }

  onReject(announce: IAnnounce) {
     const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'İlanı Reddetmek istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: IAnnounce = {
          ...announce,
          isNew: false,
          reject: true,
          isPublish: false
        };
        this.announceStore.publish(model);
      }
    });
  }

  onDelete(element: IAnnounce) {
    // console.log("deleted");
  }
}
