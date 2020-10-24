import { Component, ElementRef, Inject, OnDestroy, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NgbCarousel, NgbSlideEvent } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { FoodMenuBgPhotoStore } from 'src/app/core/services/stores/food-menu-bg-photo-store';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { IKiosks } from '../models/IKiosks';
import { KiosksStore } from '../store/kiosks-store';

@Component({
  selector: 'app-subscreen-preview-dialog',
  templateUrl: './subscreen-preview-dialog.component.html',
  styleUrls: ['./subscreen-preview-dialog.component.scss']
})
export class SubscreenPreviewDialogComponent implements OnInit,OnDestroy {
kiosks:Observable<IKiosks>;
subscreen:ISubScreen;
interval:number=2500;
bgPhotoUrl:string;

@ViewChild('carousel') carousel: NgbCarousel;
@ViewChildren('video') videoInput: QueryList<ElementRef>;
video: HTMLVideoElement;

subscriptionFoodMenuBgPhoto:Subscription=Subscription.EMPTY;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data:any,
    private kiosksStore:KiosksStore,
    private foodMenuBgPhotoStore:FoodMenuBgPhotoStore
  ) {
    this.subscreen=data?.subscreen;
   
   }


  ngOnInit(): void {
    this.kiosks=this.kiosksStore.getListBySubScreenId(this.subscreen?.id);
    this.subscriptionFoodMenuBgPhoto=  this.foodMenuBgPhotoStore.bgphotos$.pipe(
      map(photos=>photos.find(x=>x.isSetBackground===true))
    ).subscribe(result=>{
      if(result){
        this.bgPhotoUrl=result?.fullPath;
      }
    })
  }

  onEnded(event) {
    const numberSlide = this.carousel.slides.length;
    if(numberSlide==1){
      this.video.play();
    }
  }

  onLoadedloaded(event) {
    const numberSlide = this.carousel?.slides?.length;
    if (numberSlide == 1) {
      this.carousel.slides.forEach((x) => {
        const parseId = x.id.split('_');
        const [
          id,
          index,
          announceType,
          contentType,
          intervalTime,
          fullPath,
        ] = parseId;
        if (contentType.toLowerCase() == 'video') {
          this.video = this.videoInput.find(
            (x) => x.nativeElement.id == index
          ).nativeElement;
          this.video.play();
         
        }
      });
    }
  }


  onSlideChange(event: NgbSlideEvent) {
    const parseId = event.current.split('_');
    const [
      id,
      index,
      announceType,
      contentType,
      intervalTime,
      fullPath,
    ] = parseId;

    let intervalTimeToMiliSecond = parseInt(intervalTime) * 1000;
    if (this.video?.played) {
      this.video.pause();
      this.video.load();
    }
    if (contentType?.toLowerCase() == 'video') {
      this.video = this.videoInput.find(
        (x) => x.nativeElement.id == index
      ).nativeElement;
      this.video.play();
  
    }

    this.interval = intervalTimeToMiliSecond;
  }

  ngOnDestroy(){
    this.subscriptionFoodMenuBgPhoto.unsubscribe();
  }

}
