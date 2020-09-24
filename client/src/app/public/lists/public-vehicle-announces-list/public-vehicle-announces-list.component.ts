import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PublicVehicleAnnounceDetailComponent } from '../../details/public-vehicle-announce-detail/public-vehicle-announce-detail.component';
import { IVehicleAnnounceForPublic } from '../../models/IVehicleAnnounceForPublic';
import { PublicStore } from '../../store/public-store';

@Component({
  selector: 'app-public-vehicle-announces-list',
  templateUrl: './public-vehicle-announces-list.component.html',
  styleUrls: ['./public-vehicle-announces-list.component.scss'],
})
export class PublicVehicleAnnouncesListComponent implements OnInit {
  vehicleannounces$: Observable<IVehicleAnnounceForPublic[]>;

  constructor(
    private publicStore: PublicStore,
    private dialog:MatDialog
    ) {}

  ngOnInit(): void {
    this.vehicleannounces$ = this.publicStore.allannounces$.pipe(
      map((vehicleannounces) => vehicleannounces?.vehicleAnnounces)
    );
  }

  onDetail(vehicleannounce:IVehicleAnnounceForPublic){
    this.dialog.open(PublicVehicleAnnounceDetailComponent,{
      width:"100vw",
      height:"90vh",
      data:{
        vehicleannounce:vehicleannounce
      }
    })
  }
}
