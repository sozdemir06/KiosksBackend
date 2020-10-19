import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IVehicleAnnouncePhoto } from 'src/app/shared/models/IVehicleAnnouncePhoto';
import { IVehicleAnnounceForPublic } from '../../models/IVehicleAnnounceForPublic';
import { UserVehicleAnnounceStore } from '../../store/user-vehicle-announce-store';

@Component({
  selector: 'app-edit-user-vehicle-announce-photo-dialog',
  templateUrl: './edit-user-vehicle-announce-photo-dialog.component.html',
  styleUrls: ['./edit-user-vehicle-announce-photo-dialog.component.scss']
})
export class EditUserVehicleAnnouncePhotoDialogComponent implements OnInit {
  announce:IVehicleAnnounceForPublic;
  allowedRole:string[]=['Sudo','Public'];
  announce$:Observable<IVehicleAnnounceForPublic>;
  
    constructor(
      @Inject(MAT_DIALOG_DATA) public data:any,
      private userVehicleAnnounceStore:UserVehicleAnnounceStore
    ) {
      this.announce=data?.announce;
     }
  
    ngOnInit(): void {
     this.announce$=this.userVehicleAnnounceStore.Vehicleannounces$.pipe(
       map(announces=>announces.data.find(x=>x.id==this.announce.id))
     );
    }
  
    uploadResult(event:IVehicleAnnouncePhoto){
        this.userVehicleAnnounceStore.addPhoto(event,this.announce?.id);
    }
  

}
