import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { IFoodMenuForKiosks } from '../../models/IFoodMenuForKiosks';
import { FoodMenuBgPhotoStore } from 'src/app/core/services/stores/food-menu-bg-photo-store';
import { Observable, Subscription } from 'rxjs';
import { IFoodMenuBgPhoto } from 'src/app/shared/models/IFoodMenuBgPhoto';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-food-menu',
  templateUrl: './food-menu.component.html',
  styleUrls: ['./food-menu.component.scss']
})
export class FoodMenuComponent implements OnInit,OnDestroy {
@Input() foodMenu:IFoodMenuForKiosks;
bgPhotoUrl:string;
subscription:Subscription=Subscription.EMPTY;

  constructor(
    private foodMenuBgPhotoStore:FoodMenuBgPhotoStore
  ) { }

  ngOnInit(): void {
  this.subscription=  this.foodMenuBgPhotoStore.bgphotos$.pipe(
      map(photos=>photos.find(x=>x.isSetBackground===true))
    ).subscribe(result=>{
      if(result){
        this.bgPhotoUrl=result?.fullPath;
      }
    })
  }

  ngOnDestroy(){
    this.subscription.unsubscribe();
  }

}
