import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { EditHomeAnnounceDialogComponent } from 'src/app/admin/home-announces/edit-home-announce-dialog/edit-home-announce-dialog.component';
import { HelperService } from 'src/app/core/services/helper-service';
import { LoadingService } from 'src/app/core/services/loading-service';
import { IUser } from 'src/app/shared/models/IUser';
import { PublicHomeAnnounceDetailComponent } from '../../details/public-home-announce-detail/public-home-announce-detail.component';
import { IHomeAnnounceForPublic } from '../../models/IHomeAnnounceForPublic';
import { UserHomeAnnounceStore } from '../../store/user-home-announce-store';
import { EditUserHomeAnnounceDialogComponent } from '../edit-user-home-announce-dialog/edit-user-home-announce-dialog.component';
import { EditUserHomeAnnouncePhotoDialogComponent } from '../edit-user-home-announce-photo-dialog/edit-user-home-announce-photo-dialog.component';
const AUTH_DATA="auth_data";

@Component({
  selector: 'app-public-user-homeannounce',
  templateUrl: './public-user-homeannounce.component.html',
  styleUrls: ['./public-user-homeannounce.component.scss']
})
export class PublicUserHomeannounceComponent implements OnInit {
  displayedColumns: string[] = [
    'Image',
    'Header',
    'Created',
    'PublishDates',
    'PublishStatus',
    'Actions',
  ];

  user:IUser;

  constructor(
    public userHomeAnnounceStore:UserHomeAnnounceStore,
    public helperService:HelperService,
    private dialog:MatDialog
  ) { }

  ngOnInit(): void {
    const user:IUser=JSON.parse(localStorage.getItem(AUTH_DATA));
    if(user){
      this.user=user;
    }
  }


  onPageChange(event: PageEvent) {
    const params = this.userHomeAnnounceStore.getParams();
    params.pageSize = event.pageSize;
    params.pageIndex = event.pageIndex + 1;
    this.userHomeAnnounceStore.setParams(params);
    this.userHomeAnnounceStore.getListByParams();
  }

  onDetail(announce:IHomeAnnounceForPublic){
    this.dialog.open(PublicHomeAnnounceDetailComponent,{
      width:"100vw",
      height:"90vh",
      data:{
        homeannounce:announce
      }
    })
  }

  onCreate(){
    this.dialog.open(EditUserHomeAnnounceDialogComponent,{
      width:"60rem",
      height:"90vh",
      data:{
        title:"Yeni Ev İlanı Yükle",
        mode:"create",
        item:null,
        user:this.user
      }
    })
  }

  onEditPhoto(announce:IHomeAnnounceForPublic){
    this.dialog.open(EditUserHomeAnnouncePhotoDialogComponent,{
      width:"100vw",
      height:"90vh",
      data:{
        announce:announce
      }
    })
  }
  onUpdate(homeannounce:IHomeAnnounceForPublic){
    this.dialog.open(EditUserHomeAnnounceDialogComponent,{
      width:"60rem",
      height:"90vh",
      data:{
        title:"Ev ilanını güncelle",
        mode:"update",
        item:homeannounce,
        user:this.user
      }
    })
  }


}
