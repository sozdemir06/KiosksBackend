import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AnnounceStore } from 'src/app/core/services/stores/announce-store';
import { MatDialog } from '@angular/material/dialog';
import { Location } from '@angular/common';
import { IAnnouncePhoto } from 'src/app/shared/models/IAnnouncePhoto';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { IAnnounceSubScreen } from 'src/app/shared/models/IAnnounceSubScreen';
import { HelperService } from 'src/app/core/services/helper-service';
import { Observable } from 'rxjs';
import { IAnnounce } from 'src/app/shared/models/IAnnounce';

@Component({
  selector: 'app-announces-detail',
  templateUrl: './announces-detail.component.html',
  styleUrls: ['./announces-detail.component.scss'],
})
export class AnnouncesDetailComponent implements OnInit,AfterViewInit {
  announceId: number;
  announce:Observable<IAnnounce>;

  roleForAddPhoto: string[] = [
    'Sudo',
    'Announces.Create',
    'Announces.All',
  ];
  roleForUpdatePhoto: string[] = [
    'Sudo',
    'Announces.Update',
    'Announces.All',
  ];
  roleForDeletePhoto: string[] = [
    'Sudo',
    'Announces.Delete',
    'Announces.All',
  ];

  roleForAddSubScreen: string[] = [
    'Sudo',
    'Announces.Publish',
    'Announces.All',
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
  }
  ngAfterViewInit(){
    setTimeout(()=>{
      this.announce=this.announceStore.getDetailById(this.announceId);
    })
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
