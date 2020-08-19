import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IPagination } from 'src/app/shared/models/IPagination';
import { IFoodMenu } from 'src/app/shared/models/IFoodMenu';
import { IFoodMenuDetail } from 'src/app/shared/models/IFoodMenuDetail';
import { HttpClient, HttpParams } from '@angular/common/http';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { FoodMenuParams } from 'src/app/shared/models/FoodMenuParams';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';
import { IFoodMenuPhoto } from 'src/app/shared/models/IFoodMenuPhoto';
import { IFoodMenuSubScreen } from 'src/app/shared/models/IFoodMenuSubScreen';

@Injectable({ providedIn: 'root' })
export class FoodMenuStore {
  apiUrl: string = environment.apiUrl;

  private subject = new BehaviorSubject<IPagination<IFoodMenu>>(null);
  foodsmenu$: Observable<IPagination<IFoodMenu>> = this.subject.asObservable();
  private detailSubject = new BehaviorSubject<IFoodMenuDetail>(null);
  detail$: Observable<IFoodMenuDetail> = this.detailSubject.asObservable();

  foodMenuParams = new FoodMenuParams();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getList(this.foodMenuParams);
  }

  private getList(foodMenuParams: FoodMenuParams) {
    let params = new HttpParams();
    if (foodMenuParams.search) {
      params = params.append('search', foodMenuParams.search);
    }
    if (foodMenuParams.sort) {
      params = params.append('sort', foodMenuParams.sort);
    }
    if (foodMenuParams.screenId) {
      params = params.append('screenId', foodMenuParams.screenId.toString());
    }
    if (foodMenuParams.subScreenId) {
      params = params.append(
        'subScreenId',
        foodMenuParams.subScreenId.toString()
      );
    }
    if (foodMenuParams.isNew) {
      params = params.append('isNew', foodMenuParams.isNew.toString());
    }
    if (foodMenuParams.isPublish) {
      params = params.append('isPublish', foodMenuParams.isPublish.toString());
    }
    if (foodMenuParams.reject) {
      params = params.append('reject', foodMenuParams.reject.toString());
    }

    params = params.append('pageIndex', foodMenuParams.pageIndex.toString());
    params = params.append('pageSize', foodMenuParams.pageSize.toString());

    const list$ = this.httpClient
      .get<IPagination<IFoodMenu>>(this.apiUrl + 'foodmenu', { params })
      .pipe(
        map((foodsmenu) => foodsmenu),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((foodsmenu) => {
          this.subject.next(foodsmenu);
        })
      );
    this.loadingService.showLoaderUntilCompleted(list$).subscribe();
  }

  create(model: IFoodMenu) {
    const create$ = this.httpClient
      .post<IFoodMenu>(this.apiUrl + 'foodmenu', model)
      .pipe(
        map((foodsmenu) => foodsmenu),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((foodsmenu) => {
          const createSubject = produce(this.subject.getValue(), (draft) => {
            draft.data.push(foodsmenu);
          });
          this.subject.next(createSubject);
          this.notifyService.notify('success', 'Yeni Menu Eklendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: Partial<IFoodMenu>) {
    const update$ = this.httpClient
      .put<IFoodMenu>(this.apiUrl + 'foodmenu', model)
      .pipe(
        map((foodsmenu) => foodsmenu),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((foodsmenu) => {
          const updateSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id == foodsmenu.id);
            if (index != -1) {
              draft.data[index] = foodsmenu;
            }
          });
          this.subject.next(updateSubject);
          this.notifyService.notify('success', 'Menü Güncellendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  delete(model: IFoodMenu) {
    const delete$ = this.httpClient
      .delete<IFoodMenu>(this.apiUrl + 'foodmenu/' + model.id)
      .pipe(
        map((foodsmenu) => foodsmenu),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((foodsmenu) => {
          const deleteSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id == foodsmenu.id);
            if (index != -1) {
              draft.data.slice(index, 1);
            }
          });
          this.subject.next(deleteSubject);
          this.notifyService.notify('success', 'Menu silindi...');
        })
      );
  }
  publish(model: Partial<IFoodMenu>) {
    const publish$ = this.httpClient
      .put<IFoodMenu>(this.apiUrl + 'foodmenu/publish', model)
      .pipe(
        map((foodsmenu) => foodsmenu),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((foodsmenu) => {
          const updateSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id === foodsmenu.id);
            const update: IFoodMenu = {
              ...draft.data[index],
              ...foodsmenu,
            };
            draft.data[index] = update;
          });
          this.subject.next(updateSubject);
          this.notifyService.notify('success', 'İşlem Başarılı...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(publish$).subscribe();
  }

  getDetail(foodMenuId: number) {
    this.detailSubject.next(null);
    const detail$ = this.httpClient
      .get<IFoodMenuDetail>(this.apiUrl + 'foodmenu/detail/' + foodMenuId)
      .pipe(
        map((foodsmenu) => foodsmenu),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((foodsmenu) => {
          this.detailSubject.next(foodsmenu);
        })
      );
    this.loadingService.showLoaderUntilCompleted(detail$).subscribe();
  }

 

  addPhoto(photo: IFoodMenuPhoto) {
    const updatePhoto = produce(this.detailSubject.getValue(), (draft) => {
      draft.foodMenuPhotos.push(photo);
    });
    this.detailSubject.next(updatePhoto);
    this.notifyService.notify('success', 'Dosya Eklendi...');
  }

  updatePhoto(photo: IFoodMenuPhoto) {
    const update$ = this.httpClient
      .put<IFoodMenuPhoto>(this.apiUrl + 'foodmenuphoto', photo)
      .pipe(
        map((result) => result),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((result) => {
          const updateDetailsubject = produce(
            this.detailSubject.getValue(),
            (draft) => {
              const photoIndex = draft.foodMenuPhotos.findIndex(
                (x) => x.id == result.id
              );
              draft.foodMenuPhotos[photoIndex] = result;
            }
          );
          this.detailSubject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Dosya Güncellendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  setPhotoAsBackground(photo:Partial<IFoodMenuPhoto>){
    const update$ = this.httpClient
    .put<IFoodMenuPhoto>(this.apiUrl + 'foodmenuphoto/setbackground', photo)
    .pipe(
      map((result) => result),
      catchError((error) => {
        this.notifyService.notify('error', error);
        return throwError(error);
      }),
      tap((result) => {
        const updateDetailsubject = produce(
          this.detailSubject.getValue(),
          (draft) => {
            const photoIndex = draft.foodMenuPhotos.findIndex(
              (x) => x.id == result.id
            );
            const currentBgIndex=draft.foodMenuPhotos.findIndex(x=>x.isSetBackground==true);
            draft.foodMenuPhotos[currentBgIndex].isSetBackground=false;
            
            draft.foodMenuPhotos[photoIndex] = result;
          }
        );
        this.detailSubject.next(updateDetailsubject);
        this.notifyService.notify('success', 'Arka Plan olarak kaydedildi...');
      })
    );
  this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }
  deletePhoto(model: IFoodMenuPhoto) {
    const deletePhoto$ = this.httpClient
      .delete<IFoodMenuPhoto>(this.apiUrl + 'foodmenuphoto/' + model.id)
      .pipe(
        map((photo) => photo),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((photo) => {
          const updateDetailsubject = produce(
            this.detailSubject.getValue(),
            (draft) => {
              const photoIndex = draft.foodMenuPhotos.findIndex(
                (x) => x.id === photo.id
              );
              if (photoIndex != -1) {
                draft.foodMenuPhotos.splice(photoIndex, 1);
              }
            }
          );
          this.detailSubject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Dosya Silindi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(deletePhoto$).subscribe();
  }

  addSubScreen(model: Partial<IFoodMenuSubScreen>) {
    const addSubScreen$ = this.httpClient
      .post<IFoodMenuSubScreen>(this.apiUrl + 'foodmenusubscreen', model)
      .pipe(
        map((subscreen) => subscreen),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((subscreen) => {
          const updateDetailsubject = produce(
            this.detailSubject.getValue(),
            (draft) => {
              draft.foodMenuSubScreens.push(subscreen);
            }
          );
          this.detailSubject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Yayın Ekranı Eklendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(addSubScreen$).subscribe();
  }

  removeSubScreen(model: IFoodMenuSubScreen) {
    const removeSubScreen$ = this.httpClient
      .delete<IFoodMenuSubScreen>(this.apiUrl + 'FoodMenuSubScreen/' + model.id)
      .pipe(
        map((subscreen) => subscreen),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((subscreen) => {
          const updateDetailsubject = produce(
            this.detailSubject.getValue(),
            (draft) => {
              const index = draft.foodMenuSubScreens.findIndex(
                (x) => x.id === subscreen.id
              );
              if (index != -1) {
                draft.foodMenuSubScreens.splice(index, 1);
              }
            }
          );
          this.detailSubject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Yayın Ekranı Kaldırıldı...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(removeSubScreen$).subscribe();
  }

  getParams(): FoodMenuParams {
    return this.foodMenuParams;
  }

  setParams(params: FoodMenuParams): void {
    this.foodMenuParams = params;
  }

  getListByParams(): void {
    this.getList(this.foodMenuParams);
  }
}
