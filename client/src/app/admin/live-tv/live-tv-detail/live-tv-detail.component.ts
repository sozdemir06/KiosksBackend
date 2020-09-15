import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { LiveTvBroadCastStore } from 'src/app/core/services/stores/live-broadcast-store';
import { Location } from '@angular/common';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { ILiveTvBroadCastSubScreen } from 'src/app/shared/models/ILiveTvBroadCastSubScreen';
import { DomSanitizer } from '@angular/platform-browser';


@Component({
  selector: 'app-live-tv-detail',
  templateUrl: './live-tv-detail.component.html',
  styleUrls: ['./live-tv-detail.component.scss']
})
export class LiveTvDetailComponent implements OnInit {
  announceId: number;

  roleForAddPhoto: string[] = [
    'Sudo',
    'LiveTvBroadCast.Create',
    'LiveTvBroadCast.All',
  ];
 
  roleForAddSubScreen:string[]=['Sudo',"LiveTvBroadCasts.Create",'LiveTvBroadCasts.All'];

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    public liveTvBroadCastStore: LiveTvBroadCastStore,
    private dialog: MatDialog,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this.announceId = +this.route.snapshot.paramMap.get('id');
    this.liveTvBroadCastStore.getDetail(this.announceId);

  }

  goBack() {
    this.location.back();
  }

  safeURL(youtubeId:string){
    return this.sanitizer.bypassSecurityTrustResourceUrl (`https://www.youtube.com/embed/${youtubeId}?autoplay=1&rel=0`);
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
        const model: Partial<ILiveTvBroadCastSubScreen> = {
          liveTvBroadCastId: this.announceId,
          subScreenId: subscreen?.id,
          screenId: subscreen?.screenId,
        };

        this.liveTvBroadCastStore.addSubScreen(model);
      }
    });
  }

}
