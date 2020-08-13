import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NewsStore } from 'src/app/core/services/stores/news-store';
import { MatDialog } from '@angular/material/dialog';
import { Location} from "@angular/common";
import { INewsPhoto } from 'src/app/shared/models/INewsPhoto';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { INewsSubScreen } from 'src/app/shared/models/INewsSubScreen';

@Component({
  selector: 'app-news-detail',
  templateUrl: './news-detail.component.html',
  styleUrls: ['./news-detail.component.scss']
})
export class NewsDetailComponent implements OnInit {
  announceId: number;
  roleForAddPhoto: string[] = [
    'Sudo',
    'NewsPhotos.Create',
    'News.All',
  ];
  roleForUpdatePhoto: string[] = [
    'Sudo',
    'NewsPhotos.Update',
    'News.All',
  ];
  roleForDeletePhoto: string[] = [
    'Sudo',
    'NewsPhotos.Delete',
    'News.All',
  ];

  roleForAddSubScreen: string[] = [
    'Sudo',
    'NewsSubScreens.Create',
    'News.All',
  ];

  constructor(
    private route: ActivatedRoute,
    public newsStore: NewsStore,
    private location: Location,
    private dialog: MatDialog
  ) {}

  goBack() {
    this.location.back();
  }
  ngOnInit(): void {
    this.announceId=+this.route.snapshot.paramMap.get("id");
    this.newsStore.getDetail(this.announceId);
  }


  uploadResult(photo:INewsPhoto){
    this.newsStore.addPhoto(photo);
  }

  onSelectSubScreen(subscreen: ISubScreen) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: `Bu Haberi ${subscreen.name} adlı ekranda yayınlamak istiyormusunuz?`,
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: Partial<INewsSubScreen> = {
          newsId: this.announceId,
          subScreenId: subscreen?.id,
          screenId: subscreen?.screenId,
        };
        this.newsStore.addSubScreen(model);
      }
    });
  }


}
