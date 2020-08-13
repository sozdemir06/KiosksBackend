import { Component, OnInit, Input } from '@angular/core';
import { INews } from 'src/app/shared/models/INews';
import { MatDialog } from '@angular/material/dialog';
import { NewsStore } from 'src/app/core/services/stores/news-store';
import { EditNewsDialogComponent } from '../edit-news-dialog/edit-news-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-news-list',
  templateUrl: './news-list.component.html',
  styleUrls: ['./news-list.component.scss'],
})
export class NewsListComponent implements OnInit {
  @Input() dataSource: INews[];
  displayedColumns: string[] = [
    'Image',
    'Header',
    'Created',
    'PublishDates',
    'ContentType',
    'PublishStatus',
    'Actions',
  ];

  roleForUpdate: string[] = ['Sudo', 'News.Update', 'News.All'];
  roleForPublish: string[] = ['Sudo', 'News.Publish', 'News.All'];
  constructor(private dialog: MatDialog, private newsStore: NewsStore) {}

  ngOnInit(): void {}

  checkContentType(contentType: string): string {
    let type: string = '';

    switch (contentType.toLowerCase()) {
      case 'image':
        type = 'Fotoğraf';
        break;
      case 'textandimage':
        type = 'Metin ve Fotoğraf';
        break;
      case 'text':
        type = 'Sadece Metin';
        break;
      default:
        type = 'Yok';
        break;
    }
    return type;
  }
  onUpdate(element: INews) {
    this.dialog.open(EditNewsDialogComponent, {
      width: '60vw',
      maxHeight: '100vh',
      autoFocus: false,
      data: {
        title: 'Haberi güncelle',
        mode: 'update',
        item: element,
      },
    });
  }

  onPublish(announce: INews) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Haberi yayınlamak  istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: INews = {
          ...announce,
          isNew: false,
          reject: false,
          isPublish: true,
        };
        this.newsStore.publish(model);
      }
    });
  }
  unPublish(announce: INews) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Haberi yayından kaldırmak istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: INews = {
          ...announce,
          isNew: false,
          reject: false,
          isPublish: false,
        };
        this.newsStore.publish(model);
      }
    });
  }

  onReject(announce: INews) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Haberi Reddetmek istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: INews = {
          ...announce,
          isNew: false,
          reject: true,
          isPublish: false,
        };
        this.newsStore.publish(model);
      }
    });
  }

  onDelete(element: INews) {
    // console.log("deleted");
  }
}
