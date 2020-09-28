import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { IUser } from 'src/app/shared/models/IUser';
import { PublicVehicleAnnounceDetailComponent } from '../../details/public-vehicle-announce-detail/public-vehicle-announce-detail.component';
import { IVehicleAnnounceForPublic } from '../../models/IVehicleAnnounceForPublic';
import { UserVehicleAnnounceStore } from '../../store/user-vehicle-announce-store';
import { EditUserVehicleAnnounceDialogComponent } from '../edit-user-vehicle-announce-dialog/edit-user-vehicle-announce-dialog.component';
import { EditUserVehicleAnnouncePhotoDialogComponent } from '../edit-user-vehicle-announce-photo-dialog/edit-user-vehicle-announce-photo-dialog.component';
const AUTH_DATA="auth_data";

@Component({
  selector: 'app-public-user-vehicleannounce',
  templateUrl: './public-user-vehicleannounce.component.html',
  styleUrls: ['./public-user-vehicleannounce.component.scss']
})
export class PublicUserVehicleannounceComponent implements OnInit {
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
    public userVehicleAnnounceStore:UserVehicleAnnounceStore,
    private dialog:MatDialog
  ) { }

  ngOnInit(): void {
    const user:IUser=JSON.parse(localStorage.getItem(AUTH_DATA));
    if(user){
      this.user=user;
    }
  }

  onPageChange(event: PageEvent) {
    const params = this.userVehicleAnnounceStore.getParams();
    params.pageSize = event.pageSize;
    params.pageIndex = event.pageIndex + 1;
    this.userVehicleAnnounceStore.setParams(params);
    this.userVehicleAnnounceStore.getListByParams();
  }

  onDetail(announce:IVehicleAnnounceForPublic){
    this.dialog.open(PublicVehicleAnnounceDetailComponent,{
      width:"100vw",
      height:"90vh",
      data:{
        vehicleannounce:announce
      }
    })
  }

  onCreate(){
    this.dialog.open(EditUserVehicleAnnounceDialogComponent,{
      width:"60rem",
      height:"90vh",
      data:{
        title:"Yeni Araç İlanı Yükle",
        mode:"create",
        item:null,
        user:this.user
      }
    })
  }

  onEditPhoto(announce:IVehicleAnnounceForPublic){
    this.dialog.open(EditUserVehicleAnnouncePhotoDialogComponent,{
      width:"100vw",
      height:"90vh",
      data:{
        announce:announce
      }
    })
  }
  onUpdate(homeannounce:IVehicleAnnounceForPublic){
    this.dialog.open(EditUserVehicleAnnounceDialogComponent,{
      width:"60rem",
      height:"90vh",
      data:{
        title:"Araç ilanını güncelle",
        mode:"update",
        item:homeannounce,
        user:this.user
      }
    })
  }

}
