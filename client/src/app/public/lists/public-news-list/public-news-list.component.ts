import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { INews } from 'src/app/shared/models/INews';
import { PublicNewsDetailComponent } from '../../details/public-news-detail/public-news-detail.component';
import { PublicVehicleAnnounceDetailComponent } from '../../details/public-vehicle-announce-detail/public-vehicle-announce-detail.component';
import { INewsForPublic } from '../../models/INewsForPublic';
import { IVehicleAnnounceForPublic } from '../../models/IVehicleAnnounceForPublic';
import { PublicStore } from '../../store/public-store';

@Component({
  selector: 'app-public-news-list',
  templateUrl: './public-news-list.component.html',
  styleUrls: ['./public-news-list.component.scss'],
})
export class PublicNewsListComponent implements OnInit {
  news$: Observable<INewsForPublic[]>;

  constructor(
    private publicStore: PublicStore,
    private dialog:MatDialog
    ) {}

  ngOnInit(): void {
    this.news$ = this.publicStore?.allannounces$.pipe(
      map((news) => news?.news)
    );
  }

  onDetail(news:INewsForPublic){
    this.dialog.open(PublicNewsDetailComponent,{
      width:"100vw",
      height:"90vh",
      data:{
        news:news
      }
    })
  }

  
}
