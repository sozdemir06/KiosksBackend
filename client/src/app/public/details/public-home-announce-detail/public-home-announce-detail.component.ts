import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { HelperService } from 'src/app/core/services/helper-service';
import { IHomeAnnounceForPublic } from '../../models/IHomeAnnounceForPublic';

@Component({
  selector: 'app-public-home-announce-detail',
  templateUrl: './public-home-announce-detail.component.html',
  styleUrls: ['./public-home-announce-detail.component.scss']
})
export class PublicHomeAnnounceDetailComponent implements OnInit {
homeAnnounce:IHomeAnnounceForPublic;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    private matDialogRef:MatDialogRef<PublicHomeAnnounceDetailComponent>,
    public helperService:HelperService
  ) { 
    this.homeAnnounce=data?.homeannounce;

  }

  ngOnInit(): void {
  }

  onClose(){
    this.matDialogRef.close();
  }

}
