import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PublicHomeAnnounceDetailComponent } from '../../details/public-home-announce-detail/public-home-announce-detail.component';
import { IHomeAnnounceForPublic } from '../../models/IHomeAnnounceForPublic';
import { PublicStore } from '../../store/public-store';

@Component({
  selector: 'app-public-home-announces-list',
  templateUrl: './public-home-announces-list.component.html',
  styleUrls: ['./public-home-announces-list.component.scss'],
})
export class PublicHomeAnnouncesListComponent implements OnInit {
  homeannounces$: Observable<IHomeAnnounceForPublic[]>;

  constructor(
    private publicStore: PublicStore,
    private dialog:MatDialog
    
    ) {}

  ngOnInit(): void {
    this.homeannounces$ = this.publicStore?.allannounces$.pipe(
      map((homeannounces) => homeannounces?.homeAnnounces)
    );
  }

  onDetail(homeannounce:IHomeAnnounceForPublic){
    this.dialog.open(PublicHomeAnnounceDetailComponent,{
      width:"100vw",
      height:"90vh",
      data:{
        homeannounce:homeannounce
      }
    })
  }
}
