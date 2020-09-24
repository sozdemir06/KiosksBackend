import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HelperService } from 'src/app/core/services/helper-service';
import { IVehicleAnnounceForPublic } from '../../models/IVehicleAnnounceForPublic';

@Component({
  selector: 'app-public-vehicle-announce-detail',
  templateUrl: './public-vehicle-announce-detail.component.html',
  styleUrls: ['./public-vehicle-announce-detail.component.scss']
})
export class PublicVehicleAnnounceDetailComponent implements OnInit {
  vehicleAnnounce:IVehicleAnnounceForPublic;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    private matDialogRef:MatDialogRef<PublicVehicleAnnounceDetailComponent>,
    public helperService:HelperService
  ) { 
    this.vehicleAnnounce=data?.vehicleannounce;

  }

  ngOnInit(): void {
  }

  onClose(){
    this.matDialogRef.close();
  }

}
