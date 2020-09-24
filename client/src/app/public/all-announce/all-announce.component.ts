import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PublicAnnounceDetailComponent } from '../details/public-announce-detail/public-announce-detail.component';
import { PublicFoodMenuDetailComponent } from '../details/public-food-menu-detail/public-food-menu-detail.component';
import { PublicHomeAnnounceDetailComponent } from '../details/public-home-announce-detail/public-home-announce-detail.component';
import { PublicNewsDetailComponent } from '../details/public-news-detail/public-news-detail.component';
import { PublicVehicleAnnounceDetailComponent } from '../details/public-vehicle-announce-detail/public-vehicle-announce-detail.component';
import { IAnnounceForPublic } from '../models/IAnnounceForPublic';
import { IFoodMenuForPublic } from '../models/IFoodMenuForPublic';
import { IHomeAnnounceForPublic } from '../models/IHomeAnnounceForPublic';
import { INewsForPublic } from '../models/INewsForPublic';
import { IVehicleAnnounceForPublic } from '../models/IVehicleAnnounceForPublic';
import { PublicStore } from '../store/public-store';

@Component({
  selector: 'app-all-announce',
  templateUrl: './all-announce.component.html',
  styleUrls: ['./all-announce.component.scss']
})
export class AllAnnounceComponent implements OnInit {

  constructor(
    public publicStore:PublicStore,
    private dialog:MatDialog
  ) { }

  ngOnInit(): void {
  }

  onAnnounceDetail(announce:IAnnounceForPublic){
    this.dialog.open(PublicAnnounceDetailComponent,{
      width:"100vw",
      height:"90vh",
      data:{
        announce:announce
      }
    })
  }

  onHomeAnnounceDetail(homeannounce:IHomeAnnounceForPublic){
    this.dialog.open(PublicHomeAnnounceDetailComponent,{
      width:"100vw",
      height:"90vh",
      data:{
        homeannounce:homeannounce
      }
    })
  }

  onVehicleAnnounceDetail(vehicleannounce:IVehicleAnnounceForPublic){
    this.dialog.open(PublicVehicleAnnounceDetailComponent,{
      width:"100vw",
      height:"90vh",
      data:{
        vehicleannounce:vehicleannounce
      }
    })
  }

  onNewsDetail(news:INewsForPublic){
    this.dialog.open(PublicNewsDetailComponent,{
      width:"100vw",
      height:"90vh",
      data:{
        news:news
      }
    })
  }

  onFoodMenuDetail(foodMenu:IFoodMenuForPublic){
    this.dialog.open(PublicFoodMenuDetailComponent,{
      width:"100vw",
      height:"90vh",
      data:{
        foodsmenu:foodMenu
      }
    })
  }

}
