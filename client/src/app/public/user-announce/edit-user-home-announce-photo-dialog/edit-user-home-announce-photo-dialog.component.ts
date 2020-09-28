import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IHomeAnnouncePhoto } from 'src/app/shared/models/IHomeAnnouncePhoto';
import { IHomeAnnounceForPublic } from '../../models/IHomeAnnounceForPublic';
import { UserHomeAnnounceStore } from '../../store/user-home-announce-store';

@Component({
  selector: 'app-edit-user-home-announce-photo-dialog',
  templateUrl: './edit-user-home-announce-photo-dialog.component.html',
  styleUrls: ['./edit-user-home-announce-photo-dialog.component.scss']
})
export class EditUserHomeAnnouncePhotoDialogComponent implements OnInit {
  announce:IHomeAnnounceForPublic;
  allowedRole:string[]=['Sudo','Public'];
  announce$:Observable<IHomeAnnounceForPublic>;
  
    constructor(
      @Inject(MAT_DIALOG_DATA) public data:any,
      private userHomeAnnounceStore:UserHomeAnnounceStore
    ) {
      this.announce=data?.announce;
     }
  
    ngOnInit(): void {
     this.announce$=this.userHomeAnnounceStore.homeannounces$.pipe(
       map(announces=>announces.data.find(x=>x.id==this.announce.id))
     );
    }
  
    uploadResult(event:IHomeAnnouncePhoto){
        this.userHomeAnnounceStore.addPhoto(event,this.announce?.id);
    }
  

}
