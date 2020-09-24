import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { HelperService } from 'src/app/core/services/helper-service';
import { FoodMenuBgPhotoStore } from 'src/app/core/services/stores/food-menu-bg-photo-store';
import { IFoodMenuForPublic } from '../../models/IFoodMenuForPublic';

@Component({
  selector: 'app-public-food-menu-detail',
  templateUrl: './public-food-menu-detail.component.html',
  styleUrls: ['./public-food-menu-detail.component.scss']
})
export class PublicFoodMenuDetailComponent implements OnInit,OnDestroy {
  foodsMenu:IFoodMenuForPublic;
  bgPhotoUrl:string;
subscription:Subscription=Subscription.EMPTY;
    constructor(
      @Inject(MAT_DIALOG_DATA) public data:any,
      private matDialogRef:MatDialogRef<PublicFoodMenuDetailComponent>,
      public helperService:HelperService,
      private foodMenuBgPhotoStore:FoodMenuBgPhotoStore
    ) {
      this.foodsMenu=data?.foodsmenu;
     }
  
    ngOnInit(): void {
      this.subscription=  this.foodMenuBgPhotoStore.bgphotos$.pipe(
        map(photos=>photos.find(x=>x.isSetBackground===true))
      ).subscribe(result=>{
        if(result){
          this.bgPhotoUrl=result?.fullPath;
        }
      })
    }
  
    onClose(){
      this.matDialogRef.close();
    }

    ngOnDestroy(){
      this.subscription.unsubscribe();
    }
  

}
