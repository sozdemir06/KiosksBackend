import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { LoadingService } from 'src/app/core/services/loading-service';
import { NotifyService } from 'src/app/core/services/notify-service';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IKiosks } from '../models/IKiosks';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';
import { HelperService } from 'src/app/core/services/helper-service';
import { IScreenHeader } from 'src/app/shared/models/IScreenHeader';
import { IScreenFooter } from 'src/app/shared/models/IScreenFooter';
import { IScreenHeaderPhoto } from 'src/app/shared/models/IScreenHeaderPhoto';
import { IScreenForKiosks } from '../models/IScreenForKiosks';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';


@Injectable({ providedIn: 'root' })
export class KiosksStore {
  apUrl: string = environment.apiUrl;

  private subject = new BehaviorSubject<IKiosks>(null);
  kiosks$: Observable<IKiosks> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService,
    private helperService: HelperService
  ) {
    
  }

  getListByScreenId(screenId: number) {
    const list$ = this.httpClient
      .get<IKiosks>(this.apUrl + 'kiosks/' + screenId)
      .pipe(
        map((kiosks) => kiosks),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((kioks) => {
          this.subject.next(kioks);
        })
      );
    this.loadingService.showLoaderUntilCompleted(list$).subscribe();
  }

  getListBySubScreenId(subscreenId:number):Observable<IKiosks>{
    return this.httpClient.get<IKiosks>(this.apUrl+"kiosks/subscreen/"+subscreenId); 
  }

  checkDateIfExpireAndRemoveFromStore(slideId: string, announceType: string) {
    switch (announceType.toLowerCase()) {
      case 'announce':
        this.removeFromAnnounces(slideId);
        break;
      case 'home':
        this.removeFromHomeAnnounces(slideId);
        break;
      case 'car':
        this.removeFromVehicleAnnounces(slideId);
        break;
      case 'news':
        this.removeFromNews(slideId);
        break;
      case 'foodmenu':
        this.removeFromFoodsMenu(slideId);
        break;
      default:
        break;
    }
  }

  removeFromAnnounces(slideId: string) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft?.announces.findIndex((x) => x.slideId == slideId);
      if (index != -1) {
        const checkIfExpire = this.helperService.checkExpire(
          draft?.announces[index].publishFinishDate
        );
        if (checkIfExpire) {
          draft?.announces.splice(index, 1);
        }
      }
    });
    this.subject.next(updateSubject);
  }

  removeFromHomeAnnounces(slideId: string) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft?.homeAnnounces.findIndex((x) => x.slideId == slideId);
      if (index != -1) {
        const checkIfExpire = this.helperService.checkExpire(
          draft?.homeAnnounces[index].publishFinishDate
        );
        if (checkIfExpire) {
          draft?.homeAnnounces.splice(index, 1);
        }
      }
    });
    this.subject.next(updateSubject);
  }

  removeFromVehicleAnnounces(slideId: string) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.vehicleAnnounces.findIndex(
        (x) => x.slideId == slideId
      );
      if (index != -1) {
        const checkIfExpire = this.helperService.checkExpire(
          draft.vehicleAnnounces[index].publishFinishDate
        );
        if (checkIfExpire) {
          draft.vehicleAnnounces.splice(index, 1);
        }
      }
    });
    this.subject.next(updateSubject);
  }

  removeFromNews(slideId: string) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.news.findIndex((x) => x.slideId == slideId);
      if (index != -1) {
        const checkIfExpire = this.helperService.checkExpire(
          draft.news[index].publishFinishDate
        );
        if (checkIfExpire) {
          draft.news.splice(index, 1);
        }
      }
    });
    this.subject.next(updateSubject);
  }

  removeFromFoodsMenu(slideId: string) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.foodsMenu.findIndex((x) => x.slideId == slideId);
      if (index != -1) {
        const checkIfExpire = this.helperService.checkExpire(
          draft.foodsMenu[index].publishFinishDate
        );
        if (checkIfExpire) {
          draft.foodsMenu.splice(index, 1);
        }
      }
    });
    this.subject.next(updateSubject);
  }

 

  //Screen Heaader START
  updateScreenHeaderRealTime(header: IScreenHeader) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      draft.screen.screenHeaders = header;
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', 'Ekran Üst başlığı güncellendi..');
  }
  //Screen Header END
  //Screen Footer START
  updateScreenFooterRealTime(footer: IScreenFooter) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      draft.screen.screenFooters = footer;
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', 'Ekran Alt başlığı güncellendi..');
  }
  //Screen Footer END

  updateScreenPhotoRealTime(photo: IScreenHeaderPhoto) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const photoIndex = draft.screen.screenHeaderPhotos.findIndex(
        (x) => x.id == photo.id
      );
      if (photoIndex != -1) {
        const isMain = draft.screen.screenHeaderPhotos.find(
          (x) =>
            x.isMain == true &&
            x.position.toLowerCase() == photo.position.toLowerCase() &&
            x.screenId==photo.screenId
        );

        if(isMain){
          isMain.isMain=false;
        }
        draft.screen.screenHeaderPhotos[photoIndex]=photo;
      }
    });
    this.subject.next(updateSubject);
  }

  //Update Screen START
  updateScreenRealTime(screen:IScreenForKiosks){
    const updateSubject=produce(this.subject.getValue(),draft=>{
       const screenid=draft.screen.id;
       if(screenid==screen.id){
         draft.screen=screen;
       }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify("success","Ekran Güncellendi..");
  }

  //Update SubScreen
  updateSubScreenRealTime(subscreen:ISubScreen){
    const updateSubject=produce(this.subject.getValue(),draft=>{
      const screenid=draft.screen.id;
      if(screenid==subscreen.screenId)
      {
         const subscreenIndex=draft.screen.subScreens.findIndex(x=>x.id==subscreen.id);
         if(subscreenIndex!=-1)
         {
            draft.screen.subScreens[subscreenIndex]=subscreen;
         }
      }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify("success","Alt Ekran Güncellendi..");
  }
}
