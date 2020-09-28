import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { HelperService } from 'src/app/core/services/helper-service';
import { IUser } from 'src/app/shared/models/IUser';
import { PublicAnnounceDetailComponent } from '../../details/public-announce-detail/public-announce-detail.component';
import { IAnnounceForPublic } from '../../models/IAnnounceForPublic';
import { UserAnnounceStore } from '../../store/user-announce-store';
import { EditUserAnnounceDialogComponent } from '../edit-user-announce-dialog/edit-user-announce-dialog.component';
import { EditUserAnnouncePhotoDialogComponent } from '../edit-user-announce-photo-dialog/edit-user-announce-photo-dialog.component';
const AUTH_DATA="auth_data";

@Component({
  selector: 'app-public-user-announce',
  templateUrl: './public-user-announce.component.html',
  styleUrls: ['./public-user-announce.component.scss']
})
export class PublicUserAnnounceComponent implements OnInit {
  displayedColumns: string[] = [
    'Image',
    'Header',
    'Created',
    'PublishDates',
    'ContentType',
    'PublishStatus',
    'Actions',
  ];
  user:IUser;

  constructor(
    public userAnnounceStore:UserAnnounceStore,
    public helperService:HelperService,
    private dialog:MatDialog,
  
  ) { }

  ngOnInit(): void {
    const user:IUser=JSON.parse(localStorage.getItem(AUTH_DATA));
    if(user){
      this.user=user;
    }
  }

  onPageChange(event: PageEvent) {
    const params = this.userAnnounceStore.getParams();
    params.pageSize = event.pageSize;
    params.pageIndex = event.pageIndex + 1;
    this.userAnnounceStore.setParams(params);
    this.userAnnounceStore.getListByParams();
  }

  onCreate(){
    this.dialog.open(EditUserAnnounceDialogComponent,{
      width:"60rem",
      maxHeight:"100vh",
      data:{
        title: 'Yeni Duyuru  ekle',
        mode: 'create',
        item: null,
        user:this.user
      }
    })
  }

  onUpdate(announce:IAnnounceForPublic){
    this.dialog.open(EditUserAnnounceDialogComponent,{
      width:"60rem",
      maxHeight:"100vh",
      data:{
        title: 'Duyuruyu Güncelle',
        mode: 'update',
        item: announce,
        user:this.user
      }
    })
  }

  onDetail(announce:IAnnounceForPublic){
    this.dialog.open(PublicAnnounceDetailComponent,{
      width:"100vw",
      height:"90vh",
      data:{
        announce:announce
      }
    })
  }

  onEditPhoto(announce:IAnnounceForPublic){
    this.dialog.open(EditUserAnnouncePhotoDialogComponent,{
      width:"100vw",
      height:"90vh",
      data:{
        announce:announce
      }
    })
  }

}
