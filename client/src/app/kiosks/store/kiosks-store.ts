import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { LoadingService } from 'src/app/core/services/loading-service';
import { NotifyService } from 'src/app/core/services/notify-service';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IKiosks } from '../models/IKiosks';
import { map, catchError, tap, subscribeOn } from 'rxjs/operators';
import produce from 'immer';
import { HelperService } from 'src/app/core/services/helper-service';
import { IAnnounce } from 'src/app/shared/models/IAnnounce';
import { IAnnouncePhoto } from 'src/app/shared/models/IAnnouncePhoto';
import { IAnnounceSubScreen } from 'src/app/shared/models/IAnnounceSubScreen';
import { IHomeAnnounce } from 'src/app/shared/models/IHomeAnnounce';
import { IHomeAnnouncePhoto } from 'src/app/shared/models/IHomeAnnouncePhoto';
import { IHomeAnnounceSubScreen } from 'src/app/shared/models/IHomeAnnounceSubScreen';
import { IVehicleAnnounceList } from 'src/app/shared/models/IVehicleAnnounceList';
import { IVehicleAnnouncePhoto } from 'src/app/shared/models/IVehicleAnnouncePhoto';
import { IVehicleAnnounceSubScreen } from 'src/app/shared/models/IVehicleAnnounceSubScreen';
import { INews } from 'src/app/shared/models/INews';
import { INewsPhoto } from 'src/app/shared/models/INewsPhoto';
import { INewsSubScreen } from 'src/app/shared/models/INewsSubScreen';
import { IFoodMenu } from 'src/app/shared/models/IFoodMenu';
import { IFoodMenuPhoto } from 'src/app/shared/models/IFoodMenuPhoto';
import { IFoodMenuSubScreen } from 'src/app/shared/models/IFoodMenuSubScreen';
import { IScreenHeader } from 'src/app/shared/models/IScreenHeader';
import { IScreenFooter } from 'src/app/shared/models/IScreenFooter';
import { IScreenHeaderPhoto } from 'src/app/shared/models/IScreenHeaderPhoto';
import { IScreenForKiosks } from '../models/IScreenForKiosks';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { IHomeAnnounceForKiosks } from '../models/IHomeAnnounceForKiosks';
import { IAnnounceForKiosks } from '../models/IAnnounceForKiosks';
import { IFoodMenuForKiosks } from '../models/IFoodMenuForKiosks';
import { INewsForKiosks } from '../models/INewsForKiosks';
import { IVehicleAnnounceForKiosks } from '../models/IVehicleAnnounceForKiosks';

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

  //SignalR Events
  updateOrCreateAnnounceRealTime(announce: IAnnounce): void {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.announces.findIndex((x) => x.id === announce.id);
      if (index != -1) {
        if (!announce.isNew && announce.isPublish && !announce.reject) {
          draft.announces[index] = announce;
          this.notifyService.notify('success', 'Duyuru Güncellendi...');
        } else if (!announce.isNew && !announce.isPublish && !announce.reject) {
          draft.announces.splice(index, 1);
          this.notifyService.notify('success', 'Duyuru Kaldırıldı...');
        }
      } else {
        draft.announces.push(announce);
        this.notifyService.notify('success', 'Yeni Duyuru eklendi...');
      }
    });
    this.subject.next(updateSubject);
  }

  updateOrAddNewAnnouncePhotoRealTime(photo: IAnnouncePhoto) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.announces.findIndex((x) => x.id === photo.announceId);
      if (index != -1) {
        const photoIndex = draft.announces[index].announcePhotos.findIndex(
          (x) => x.id === photo.id
        );
        if (photoIndex != -1) {
          if ((!photo.isConfirm && !photo.unConfirm) || photo.unConfirm) {
            draft.announces[index].announcePhotos.splice(photoIndex, 1);
            this.notifyService.notify(
              'success',
              'Fotoğraf yayından kaldırıldı...'
            );
          }
        } else {
          if (photo.isConfirm && !photo.unConfirm) {
            draft.announces[index].announcePhotos.push(photo);
            this.notifyService.notify(
              'success',
              'Duyuru için Yeni fotoğraf yayınlandı...'
            );
          }
        }
      }
    });
    this.subject.next(updateSubject);
  }

  deleteannouncePhotoRealTime(photo: IAnnouncePhoto) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.announces.findIndex((x) => x.id === photo.announceId);
      if (index != -1) {
        const photoIndex = draft.announces[index].announcePhotos.findIndex(
          (x) => x.id === photo.id
        );
        if (photoIndex != -1) {
          draft.announces[index].announcePhotos.splice(photoIndex, 1);
         
        }
      }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify(
      'success',
      'Fotoğraf yayından kaldırıldı...'
    );
  }

  createSubsCreenRealTime(announce: IAnnounceForKiosks,subscreen:IAnnounceSubScreen) {
    const updatesubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.announces.findIndex(
        (x) => x.id === announce.id
      );
      if (index !=-1) {
         draft.announces[index].announceSubScreens.push(subscreen)
      }else if(index===-1){
        draft.announces.push(announce);
      }
    });
    this.subject.next(updatesubject);
    this.notifyService.notify(
      'success',
      'Duyuru yayına alındı...'
    );
  }

  removeSubscreenRealTime(subscreen:IAnnounceSubScreen) {
    const updatesubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.announces.findIndex(
        (x) => x.id === subscreen.announceId
      );
      if (index != -1) {
        const subscreenIndex = draft.announces[
          index
        ].announceSubScreens.findIndex((x) => x.id === subscreen.id);
        if (subscreenIndex != -1) {
          draft.announces[index].announceSubScreens.splice(subscreenIndex, 1);
         
        }
      }
    });
    this.subject.next(updatesubject);
    this.notifyService.notify(
      'success',
      'Duyuru ' + subscreen.subScreenName + ' adlı ekrandan kaldırıldı...'
    );
  }
  //Announce Events END

  //Home Announce Events START
  updateOrCreateHomeAnnounceRealTime(announce: IHomeAnnounce): void {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.homeAnnounces.findIndex((x) => x.id === announce.id);
      if (index != -1) {
        if (!announce.isNew && announce.isPublish && !announce.reject) {
          draft.homeAnnounces[index] = announce;
          this.notifyService.notify('success', 'Ev ilanı Güncellendi...');
        } else if (!announce.isNew && !announce.isPublish && !announce.reject) {
          draft.homeAnnounces.splice(index, 1);
          this.notifyService.notify('success', 'Ev ilanı Kaldırıldı...');
        }
      } else {
        draft.homeAnnounces.push(announce);
        this.notifyService.notify('success', 'Yeni Ev ilanı eklendi...');
      }
    });
    this.subject.next(updateSubject);
  }

  updateOrAddNewHomeAnnouncePhotoRealTime(photo: IHomeAnnouncePhoto) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.homeAnnounces.findIndex(
        (x) => x.id === photo.homeAnnounceId
      );
      if (index != -1) {
        const photoIndex = draft.homeAnnounces[
          index
        ].homeAnnouncePhotos.findIndex((x) => x.id === photo.id);
        if (photoIndex != -1) {
          if ((!photo.isConfirm && !photo.unConfirm) || photo.unConfirm) {
            draft.homeAnnounces[index].homeAnnouncePhotos.splice(photoIndex, 1);
            this.notifyService.notify(
              'success',
              'Ev ilanı için fotoğraf yayından kaldırıldı...'
            );
          }
        } else {
          if (photo.isConfirm && !photo.unConfirm) {
            draft.homeAnnounces[index].homeAnnouncePhotos.push(photo);
            this.notifyService.notify(
              'success',
              'Ev ilanı için Yeni fotoğraf yayınlandı...'
            );
          }
        }
      }
    });
    this.subject.next(updateSubject);
  }

  deleteHomeAnnouncePhotoRealTime(photo: IHomeAnnouncePhoto) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.homeAnnounces.findIndex(
        (x) => x.id === photo.homeAnnounceId
      );
      if (index != -1) {
        const photoIndex = draft.homeAnnounces[
          index
        ].homeAnnouncePhotos.findIndex((x) => x.id === photo.id);
        if (photoIndex != -1) {
          draft.homeAnnounces[index].homeAnnouncePhotos.splice(photoIndex, 1);
          this.notifyService.notify(
            'success',
            'Ev ilanı için fotoğraf silindi...'
          );
        }
      }
    });
    this.subject.next(updateSubject);
   
  }

  createHomeAnnounceSubsCreenRealTime(subScreen: IHomeAnnounceSubScreen,homeAnnounce:IHomeAnnounceForKiosks) {
    const updatesubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.homeAnnounces.findIndex(
        (x) => x.id === subScreen.homeAnnounceId
      );
      if (index != -1) {
        draft.homeAnnounces[index].homeAnnounceSubScreens.push(subScreen);
      }else if(index===-1){
        draft.homeAnnounces.push(homeAnnounce);
      }
    });
    this.subject.next(updatesubject);
    this.notifyService.notify(
      'success',
      'Ev ilanı ' +
        subScreen.subScreenName +
        ' adlı ekranda yayına alındı...'
    );
  }

  removeHomeAnnounceSubscreenRealTime(subscreen: IHomeAnnounceSubScreen) {
    const updatesubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.homeAnnounces.findIndex(
        (x) => x.id === subscreen.homeAnnounceId
      );
      if (index != -1) {
        const subscreenIndex = draft.homeAnnounces[
          index
        ].homeAnnounceSubScreens.findIndex((x) => x.id === subscreen.id);
        if (subscreenIndex != -1) {
          draft.homeAnnounces[index].homeAnnounceSubScreens.splice(
            subscreenIndex,
            1
          );
         
        }
      }
    });
    this.subject.next(updatesubject);
    this.notifyService.notify(
      'success',
      'Ev ilanı ' +
        subscreen.subScreenName +
        ' adlı ekrandan kaldırıldı...'
    );
  }
  //HomeAnnounce Events END

  //VehicleAnnounce Events START
  updateOrCreateVehicleAnnounceRealTime(announce: IVehicleAnnounceList): void {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.vehicleAnnounces.findIndex(
        (x) => x.id === announce.id
      );
      if (index != -1) {
        if (!announce.isNew && announce.isPublish && !announce.reject) {
          draft.vehicleAnnounces[index] = announce;
          this.notifyService.notify('success', 'Araç ilanı Güncellendi...');
        } else if (!announce.isNew && !announce.isPublish && !announce.reject) {
          draft.vehicleAnnounces.splice(index, 1);
          this.notifyService.notify('success', 'Araç ilanı Kaldırıldı...');
        }
      } else {
        draft.vehicleAnnounces.push(announce);
        this.notifyService.notify('success', 'Araç Ev ilanı eklendi...');
      }
    });
    this.subject.next(updateSubject);
  }

  updateOrAddNewVehicleAnnouncePhotoRealTime(photo: IVehicleAnnouncePhoto) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.vehicleAnnounces.findIndex(
        (x) => x.id === photo.vehicleAnnounceId
      );
      if (index != -1) {
        const photoIndex = draft.vehicleAnnounces[
          index
        ].vehicleAnnouncePhotos.findIndex((x) => x.id === photo.id);
        if (photoIndex != -1) {
          if ((!photo.isConfirm && !photo.unConfirm) || photo.unConfirm) {
            draft.vehicleAnnounces[index].vehicleAnnouncePhotos.splice(
              photoIndex,
              1
            );
            this.notifyService.notify(
              'success',
              'Araç ilanı için fotoğraf yayından kaldırıldı...'
            );
          }
        } else {
          if (photo.isConfirm && !photo.unConfirm) {
            draft.vehicleAnnounces[index].vehicleAnnouncePhotos.push(photo);
            this.notifyService.notify(
              'success',
              'Araç ilanı için Yeni fotoğraf yayınlandı...'
            );
          }
        }
      }
    });
    this.subject.next(updateSubject);
  }

  deleteVehicleAnnouncePhotoRealTime(photo: IVehicleAnnouncePhoto) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.vehicleAnnounces.findIndex(
        (x) => x.id === photo.vehicleAnnounceId
      );
      if (index != -1) {
        const photoIndex = draft.vehicleAnnounces[
          index
        ].vehicleAnnouncePhotos.findIndex((x) => x.id === photo.id);
        if (photoIndex != -1) {
          draft.vehicleAnnounces[index].vehicleAnnouncePhotos.splice(
            photoIndex,
            1
          );
         
        }
      }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify(
      'success',
      'Araç ilanı için fotoğraf silindi...'
    );
  }

  createVehicleAnnounceSubsCreenRealTime(subScreen: IVehicleAnnounceSubScreen,vehicleAnnounce:IVehicleAnnounceForKiosks) {
    const updatesubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.vehicleAnnounces.findIndex(
        (x) => x.id === subScreen.vehicleAnnounceId
      );
      console.log(index);
      if (index != -1) {
        draft.vehicleAnnounces[index].vehicleAnnounceSubScreens.push(subScreen);
      }else if(index===-1){
        draft.vehicleAnnounces.push(vehicleAnnounce);
      }
    });
    this.subject.next(updatesubject);
    console.log(this.subject.getValue());
    this.notifyService.notify(
      'success',
      'Araç ilanı ' +
        subScreen.subScreenName +
        ' adlı ekranda yayına alındı...'
    );
  }

  removeVehicleAnnounceSubscreenRealTime(subscreen: IVehicleAnnounceSubScreen) {
    const updatesubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.vehicleAnnounces.findIndex(
        (x) => x.id === subscreen.vehicleAnnounceId
      );
      if (index != -1) {
        const subscreenIndex = draft.vehicleAnnounces[
          index
        ].vehicleAnnounceSubScreens.findIndex((x) => x.id === subscreen.id);
        if (subscreenIndex != -1) {
          draft.vehicleAnnounces[index].vehicleAnnounceSubScreens.splice(
            subscreenIndex,
            1
          );
        
        }
      }
    });
    this.subject.next(updatesubject);
    this.notifyService.notify(
      'success',
      'Araç ilanı ' +
        subscreen.subScreenName +
        ' adlı ekrandan kaldırıldı...'
    );
  }
  //VehicleAnnounce Events END

  //News Events START
  updateOrCreateNewsRealTime(news: INews): void {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.news.findIndex((x) => x.id === news.id);
      if (index != -1) {
        if (!news.isNew && news.isPublish && !news.reject) {
          draft.news[index] = news;
          this.notifyService.notify('success', 'Haber Güncellendi...');
        } else if (!news.isNew && !news.isPublish && !news.reject) {
          draft.news.splice(index, 1);
          this.notifyService.notify('success', 'Haber Kaldırıldı...');
        }
      } else {
        draft.news.push(news);
        this.notifyService.notify('success', 'Haber  eklendi...');
      }
    });
    this.subject.next(updateSubject);
  }

  updateOrAddNewNewsPhotoRealTime(photo: INewsPhoto) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.news.findIndex((x) => x.id === photo.newsId);
      if (index != -1) {
        const photoIndex = draft.news[index].newsPhotos.findIndex(
          (x) => x.id === photo.id
        );
        if (photoIndex != -1) {
          if ((!photo.isConfirm && !photo.unConfirm) || photo.unConfirm) {
            draft.news[index].newsPhotos.splice(photoIndex, 1);
            this.notifyService.notify(
              'success',
              'Haber için fotoğraf yayından kaldırıldı...'
            );
          }
        } else {
          if (photo.isConfirm && !photo.unConfirm) {
            draft.news[index].newsPhotos.push(photo);
            this.notifyService.notify(
              'success',
              'Haber için Yeni fotoğraf yayınlandı...'
            );
          }
        }
      }
    });
    this.subject.next(updateSubject);
  }

  deleteNewsPhotoRealTime(photo: INewsPhoto) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.news.findIndex((x) => x.id === photo.newsId);
      if (index != -1) {
        const photoIndex = draft.news[index].newsPhotos.findIndex(
          (x) => x.id === photo.id
        );
        if (photoIndex != -1) {
          draft.news[index].newsPhotos.splice(photoIndex, 1);
          this.notifyService.notify(
            'success',
            'Haber için fotoğraf silindi...'
          );
        }
      }
    });
    this.subject.next(updateSubject);
   
  }

  createNewsSubsCreenRealTime(subScreen: INewsSubScreen,news:INewsForKiosks) {
    const updatesubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.news.findIndex((x) => x.id === subScreen.newsId);
      if (index != -1) {
        draft.news[index].newsSubScreens.push(subScreen);
      }else if(index===-1){
        draft.news.push(news);
      }
    });
    this.subject.next(updatesubject);
    this.notifyService.notify(
      'success',
      'Haber ' + subScreen.subScreenName + ' adlı ekranda yayına alındı...'
    );
  }

  removeNewsSubscreenRealTime(subscreen: INewsSubScreen) {
    const updatesubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.news.findIndex((x) => x.id === subscreen.newsId);
      if (index != -1) {
        const subscreenIndex = draft.news[index].newsSubScreens.findIndex(
          (x) => x.id === subscreen.id
        );
        if (subscreenIndex != -1) {
          draft.news[index].newsSubScreens.splice(subscreenIndex, 1);
          this.notifyService.notify(
            'success',
            'Haber ' + subscreen.subScreenName + ' adlı ekrandan kaldırıldı...'
          );
        }
      }
    });
    this.subject.next(updatesubject);
    
  }
  //News EVents END
  //FoodMenu Events START
  updateOrCreateFoodMenuRealTime(foodsMenu: IFoodMenu): void {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.foodsMenu.findIndex((x) => x.id === foodsMenu.id);
      if (index != -1) {
        if (!foodsMenu.isNew && foodsMenu.isPublish && !foodsMenu.reject) {
          draft.foodsMenu[index] = foodsMenu;
          this.notifyService.notify('success', 'Yemek Menüsü Güncellendi...');
        } else if (
          !foodsMenu.isNew &&
          !foodsMenu.isPublish &&
          !foodsMenu.reject
        ) {
          draft.foodsMenu.splice(index, 1);
          this.notifyService.notify('success', 'Yemek Menüsü Kaldırıldı...');
        }
      } else {
        draft.foodsMenu.push(foodsMenu);
        this.notifyService.notify('success', 'Yemek Menüsü  eklendi...');
      }
    });
    this.subject.next(updateSubject);
  }

  updateOrAddNewFoodMenuPhotoRealTime(photo: IFoodMenuPhoto) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.foodsMenu.findIndex((x) => x.id === photo.foodMenuId);
      if (index != -1) {
        const photoIndex = draft.foodsMenu[index].foodMenuPhotos.findIndex(
          (x) => x.id === photo.id
        );
        if (photoIndex != -1) {
          if ((!photo.isConfirm && !photo.unConfirm) || photo.unConfirm) {
            draft.foodsMenu[index].foodMenuPhotos.splice(photoIndex, 1);
            this.notifyService.notify(
              'success',
              'Yemek Menüsü için fotoğraf yayından kaldırıldı...'
            );
          }
        } else {
          if (photo.isConfirm && !photo.unConfirm) {
            draft.foodsMenu[index].foodMenuPhotos.push(photo);
            this.notifyService.notify(
              'success',
              'Yemek Menüsü için Yeni fotoğraf yayınlandı...'
            );
          }
        }
      }
    });
    this.subject.next(updateSubject);
  }

  deleteFoodMenuPhotoRealTime(photo: IFoodMenuPhoto) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.foodsMenu.findIndex((x) => x.id === photo.foodMenuId);
      if (index != -1) {
        const photoIndex = draft.foodsMenu[index].foodMenuPhotos.findIndex(
          (x) => x.id === photo.id
        );
        if (photoIndex != -1) {
          draft.foodsMenu[index].foodMenuPhotos.splice(photoIndex, 1);
          this.notifyService.notify(
            'success',
            'Yemek Menüsü için fotoğraf silindi...'
          );
        }
      }
    });
    this.subject.next(updateSubject);
  }

  createFoodMenuSubsCreenRealTime(subScreen: IFoodMenuSubScreen,foodMenu:IFoodMenuForKiosks) {
    const updatesubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.foodsMenu.findIndex(
        (x) => x.id === subScreen.foodMenuId
      );
      if (index != -1) {
        draft.foodsMenu[index].foodMenuSubScreens.push(subScreen);
      }else if(index===-1){
        draft.foodsMenu.push(foodMenu);
      }
    });
    this.subject.next(updatesubject);
    this.notifyService.notify(
      'success',
      'Yemek Menüsü ' +
        subScreen.subScreenName +
        ' adlı ekranda yayına alındı...'
    );
  }

  removeFoodMenuSubscreenRealTime(subscreen: IFoodMenuSubScreen) {
    const updatesubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.foodsMenu.findIndex(
        (x) => x.id === subscreen.foodMenuId
      );
      if (index != -1) {
        const subscreenIndex = draft.foodsMenu[
          index
        ].foodMenuSubScreens.findIndex((x) => x.id === subscreen.id);
        if (subscreenIndex != -1) {
          draft.foodsMenu[index].foodMenuSubScreens.splice(subscreenIndex, 1);
          this.notifyService.notify(
            'success',
            'Yemek Menüsü ' +
              subscreen.subScreenName +
              ' adlı ekrandan kaldırıldı...'
          );
        }
      }
    });
    this.subject.next(updatesubject);
  }

  //FoodMenu Events END

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
