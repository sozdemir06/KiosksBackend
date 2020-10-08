import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { HomeAnnounceStore } from 'src/app/core/services/stores/home-announce-store';
import { IHomeAnnouncePhoto } from 'src/app/shared/models/IHomeAnnouncePhoto';
import { MatDialog } from '@angular/material/dialog';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { IHomeAnnounceSubScreen } from 'src/app/shared/models/IHomeAnnounceSubScreen';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { Observable } from 'rxjs';
import { IHomeAnnounce } from 'src/app/shared/models/IHomeAnnounce';

@Component({
  selector: 'app-home-announce-detail',
  templateUrl: './home-announce-detail.component.html',
  styleUrls: ['./home-announce-detail.component.scss'],
})
export class HomeAnnounceDetailComponent implements OnInit,AfterViewInit {
  announceId: number;
  homeannounce$:Observable<IHomeAnnounce>;

  roleForAddPhoto: string[] = [
    'Sudo',
    'HomeAnnounces.Create',
    'HomeAnnounces.All',
  ];
  roleForUpdatePhoto: string[] = [
    'Sudo',
    'HomeAnnounces.Update',
    'HomeAnnounces.All',
  ];
  roleForDeletePhoto: string[] = [
    'Sudo',
    'HomeAnnounces.Delete',
    'HomeAnnounces.All',
  ];

  roleForAddSubScreen:string[]=['Sudo',"HomeAnnounces.Create",'HomeAnnounces.All'];

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    public homeAnnounceStore: HomeAnnounceStore,
    private dialog: MatDialog
  ) {}

  ngAfterViewInit(){
    setTimeout(()=>{
     this.homeannounce$= this.homeAnnounceStore.getDetailById(this.announceId);
    })
  }
  ngOnInit(): void {
    this.announceId = +this.route.snapshot.paramMap.get('id');

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
