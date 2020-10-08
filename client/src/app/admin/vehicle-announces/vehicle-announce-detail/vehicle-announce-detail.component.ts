import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { VehilceAnnounceStore } from 'src/app/core/services/stores/vehicle-announce-store';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { IVehicleAnnouncePhoto } from 'src/app/shared/models/IVehicleAnnouncePhoto';
import { Location } from '@angular/common';
import { IVehicleAnnounceSubScreen } from 'src/app/shared/models/IVehicleAnnounceSubScreen';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { Observable } from 'rxjs';
import { IVehicleAnnounceList } from 'src/app/shared/models/IVehicleAnnounceList';

@Component({
  selector: 'app-vehicle-announce-detail',
  templateUrl: './vehicle-announce-detail.component.html',
  styleUrls: ['./vehicle-announce-detail.component.scss'],
})
export class VehicleAnnounceDetailComponent implements OnInit ,AfterViewInit{
  announceId: number;
  vehicleannounces$:Observable<IVehicleAnnounceList>;

  roleForAddPhoto: string[] = [
    'Sudo',
    'VehicleAnnounces.Create',
    'VehicleAnnounces.All',
  ];
  roleForUpdatePhoto: string[] = [
    'Sudo',
    'VehicleAnnounces.Update',
    'VehicleAnnounces.All',
  ];
  roleForDeletePhoto: string[] = [
    'Sudo',
    'VehicleAnnounces.Delete',
    'VehicleAnnounces.All',
  ];

  roleForAddSubScreen: string[] = [
    'Sudo',
    'VehicleAnnounces.Create',
    'VehicleAnnounces.All',
  ];


  constructor(
    private route: ActivatedRoute,
    public vehicleStore: VehilceAnnounceStore,
    private location: Location,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.announceId = +this.route.snapshot.paramMap.get('id');
    
  }

  ngAfterViewInit(){
    setTimeout(()=>{
      this.vehicleannounces$=this.vehicleStore.getDetailById(this.announceId);
    })
  }

  goBack() {
    this.location.back();
  }

  uploadResult(model: IVehicleAnnouncePhoto) {
    this.vehicleStore.addPhoto(model);
  }

  onSelectSubScreen(subscreen: ISubScreen) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: `Bu araç ilanını ${subscreen.name} adlı ekranda yayınlamak istiyormusunuz?`,
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: Partial<IVehicleAnnounceSubScreen> = {
          vehicleAnnounceId: this.announceId,
          subScreenId: subscreen?.id,
          screenId: subscreen?.screenId,
        };
        this.vehicleStore.addSubScreen(model);
      }
    });
  }
}
