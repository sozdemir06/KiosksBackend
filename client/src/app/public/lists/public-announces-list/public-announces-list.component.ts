import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PublicAnnounceDetailComponent } from '../../details/public-announce-detail/public-announce-detail.component';
import { IAnnounceForPublic } from '../../models/IAnnounceForPublic';
import { PublicStore } from '../../store/public-store';

@Component({
  selector: 'app-public-announces-list',
  templateUrl: './public-announces-list.component.html',
  styleUrls: ['./public-announces-list.component.scss'],
})
export class PublicAnnouncesListComponent implements OnInit {
  announces$: Observable<IAnnounceForPublic[]>;

  constructor(
    private publicStore: PublicStore,
    private dialog:MatDialog
    ) {}

  ngOnInit(): void {
    this.announces$ = this.publicStore?.allannounces$.pipe(
      map((announces) => announces?.announces)
    );
  }

  onDetail(announce:IAnnounceForPublic){
    this.dialog.open(PublicAnnounceDetailComponent,{
      width:"100vw",
      height:"90vh",
      data:{
        announce:announce
      }
    })
  }
}
