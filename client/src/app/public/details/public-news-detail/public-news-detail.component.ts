import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HelperService } from 'src/app/core/services/helper-service';
import { INewsForPublic } from '../../models/INewsForPublic';

@Component({
  selector: 'app-public-news-detail',
  templateUrl: './public-news-detail.component.html',
  styleUrls: ['./public-news-detail.component.scss']
})
export class PublicNewsDetailComponent implements OnInit {
  news:INewsForPublic;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    private matDialogRef:MatDialogRef<PublicNewsDetailComponent>,
    public helperService:HelperService
  ) { 
    this.news=data?.news;

  }

  ngOnInit(): void {
  }

  onClose(){
    this.matDialogRef.close();
  }
}
