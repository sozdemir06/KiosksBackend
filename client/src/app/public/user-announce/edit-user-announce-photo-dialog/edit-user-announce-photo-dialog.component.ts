import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IAnnouncePhoto } from 'src/app/shared/models/IAnnouncePhoto';
import { IUser } from 'src/app/shared/models/IUser';
import { IAnnounceForPublic } from '../../models/IAnnounceForPublic';
import { UserAnnounceStore } from '../../store/user-announce-store';
const AUTH_DATA="auth_data";

@Component({
  selector: 'app-edit-user-announce-photo-dialog',
  templateUrl: './edit-user-announce-photo-dialog.component.html',
  styleUrls: ['./edit-user-announce-photo-dialog.component.scss']
})
export class EditUserAnnouncePhotoDialogComponent implements OnInit {
announce:IAnnounceForPublic;
allowedRole:string[]=['Sudo','Public'];
announce$:Observable<IAnnounceForPublic>;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    private userAnnounceStore:UserAnnounceStore
  ) {
    this.announce=data?.announce;
   }

  ngOnInit(): void {
   this.announce$=this.userAnnounceStore.announces$.pipe(
     map(announces=>announces.data.find(x=>x.id==this.announce.id))
   );
  }

  uploadResult(event:IAnnouncePhoto){
      this.userAnnounceStore.addPhoto(event,this.announce?.id);
  }

}
