import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HelperService } from 'src/app/core/services/helper-service';
import { IAnnounceForPublic } from '../../models/IAnnounceForPublic';

@Component({
  selector: 'app-public-announce-detail',
  templateUrl: './public-announce-detail.component.html',
  styleUrls: ['./public-announce-detail.component.scss']
})
export class PublicAnnounceDetailComponent implements OnInit {
announce:IAnnounceForPublic;
readMore:boolean=false;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    private matDialogRef:MatDialogRef<PublicAnnounceDetailComponent>,
    public helperService:HelperService
  ) {
    this.announce=data?.announce;
   }

  ngOnInit(): void {

  }

  onClose(){
    this.matDialogRef.close();
  }

  onChangeReadMore(){
    this.readMore=!this.readMore;
  }

}
