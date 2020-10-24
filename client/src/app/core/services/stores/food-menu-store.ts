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
 
  private bgPhotoSubject = new BehaviorSubject<IFoodMenuPhoto[]>([]);
  bgPhotos$: Observable<IFoodMenuPhoto[]> = this.bgPhotoSubject.asObservable();

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

  getDetailById(foodMenuId: number) {
    return this.foodsmenu$.pipe(
      map((foodmenu) => foodmenu?.data?.find((x) => x.id == foodMenuId))
    );
  }

  addPhoto(photo: IFoodMenuPhoto) {
    const updatePhoto = produce(this.subject.getValue(), (draft) => {
       const index=draft.data.findIndex(x=>x.id==photo.foodMenuId);
       if(index!=-1){
         draft.data[index].foodMenuPhotos.push(photo);
       }
    });
    this.subject.next(updatePhoto);
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
            this.subject.getValue(),
            (draft) => {
              const index=draft.data.findIndex(x=>x.id==result.foodMenuId);
              if(index!=-1){
                const photoIndex=draft.data[index].foodMenuPhotos.findIndex(x=>x.id==result.id);
                if(photoIndex!=-1){
                  draft.data[index].foodMenuPhotos[photoIndex]=result;
                }
              }
            }
          );
          this.subject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Dosya Güncellendi...');
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
            this.subject.getValue(),
            (draft) => {
              const index=draft.data.findIndex(x=>x.id==photo.foodMenuId);
              if(index!=-1){
                const photoIndex=draft.data[index].foodMenuPhotos.findIndex(x=>x.id==photo.id);
                if(photoIndex!=-1){
                  draft.data[index].foodMenuPhotos.splice(photoIndex,1);
                }
              }
            }
          );
          this.subject.next(updateDetailsubject);
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
            this.subject.getValue(),
            (draft) => {
              const index=draft.data.findIndex(x=>x.id===subscreen.foodMenuId);
              if(index!=-1){
                draft.data[index].foodMenuSubScreens.push(subscreen);
              }
            }
          );
          this.subject.next(updateDetailsubject);
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
            this.subject.getValue(),
            (draft) => {
              const index=draft.data.findIndex(x=>x.id==subscreen.foodMenuId);
              if(index!=-1){
                const subscreenIndex=draft.data[index].foodMenuSubScreens.findIndex(x=>x.id===subscreen.id);
                if(subscreenIndex!=-1){
                  draft.data[index].foodMenuSubScreens.splice(subscreenIndex,1);
                }
              }
            }
          );
          this.subject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Yayın Ekranı Kaldırıldı...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(removeSubScreen$).subscribe();
  }

  getNewAnnounceCount(): Observable<number> {
    return this.foodsmenu$.pipe(
      map((announces) => {
        const newanouncecount = announces?.data.filter(
          (x) => x.isNew == true && x.isPublish == false && x.reject == false
        ).length;
        
        let photoCount=0;
        announces?.data?.forEach(x=>{
           x.foodMenuPhotos.forEach(x=>{
              if(x.isConfirm==false && x.unConfirm==false){
                photoCount++;
              }
           })
        })

        return newanouncecount + photoCount;
      })
    );
  }

  //SignalR Events
  createNewFoodMenuRealTime(model:IFoodMenu):void{
    const updateSubject=produce(this.subject.getValue(),draft=>{
          draft.data.push(model);
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', `${model.created} tarihli yeni bir yemek menüsü eklendi...`);
  }

  updateFoodMenuRealTime(model:IFoodMenu):void{
    const updateSubject=produce(this.subject.getValue(),draft=>{
      const index=draft.data.findIndex(x=>x.id===model.id);
      if(index!=-1){
        draft.data[index]=model;
      }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', `${model.created} tarihli yemek menüsü güncellendi...`);
  }

  addNewPhotoRealTime(photo:IFoodMenuPhoto):void{
    const updateSubject=produce(this.subject.getValue(),draft=>{
      const index=draft.data.findIndex(x=>x.id===photo.foodMenuId);
      if(index!=-1){
         draft.data[index].foodMenuPhotos.push(photo);
      }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', `${this.getFoodMenuCreatedById(photo.foodMenuId)} tarihli menü için yeni fotoğraf eklendi...`);
  }

  updatePhotoRealTime(photo:IFoodMenuPhoto):void{
    const updateSubject=produce(this.subject.getValue(),draft=>{
      const index=draft.data.findIndex(x=>x.id===photo.foodMenuId);
      if(index!=-1){
        const photoIndex=draft.data[index].foodMenuPhotos.findIndex(x=>x.id==photo.id);
        if(photoIndex!=-1){
          draft.data[index].foodMenuPhotos[photoIndex]=photo;
        }
        
      }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', `${this.getFoodMenuCreatedById(photo.foodMenuId)} tarihli menü için fotoğraf güncellendi...`);
  }

  removePhotoRealTime(photo:IFoodMenuPhoto):void{
    const updateSubject=produce(this.subject.getValue(),draft=>{
      const index=draft.data.findIndex(x=>x.id===photo.foodMenuId);
      if(index!=-1){
        const photoIndex=draft.data[index].foodMenuPhotos.findIndex(x=>x.id==photo.id);
        if(photoIndex!=-1){
          draft.data[index].foodMenuPhotos.splice(photoIndex,1);
        }
      }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', `${this.getFoodMenuCreatedById(photo.foodMenuId)} tarihli menü fotoğraf silindi...`);
  }

 private getFoodMenuCreatedById(id:number):Date{
    let foodMenu:IFoodMenu;
    produce(this.subject.getValue(),draft=>{
       foodMenu=draft.data.find(x=>x.id===id);
    });
    return foodMenu.created;
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
