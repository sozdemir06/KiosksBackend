import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IFoodMenuBgPhoto } from 'src/app/shared/models/IFoodMenuBgPhoto';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class FoodMenuBgPhotoStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<IFoodMenuBgPhoto[]>([]);
  bgphotos$: Observable<IFoodMenuBgPhoto[]> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getList();
  }

  private getList() {
    const list$ = this.httpClient
      .get<IFoodMenuBgPhoto[]>(this.apiUrl + 'foodmenubgphotos')
      .pipe(
        map((photos) => photos),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((photos) => {
          this.subject.next(photos);
        })
      );
    this.loadingService.showLoaderUntilCompleted(list$).subscribe();
  }

  addBgPhoto(photo: IFoodMenuBgPhoto) {
    const updatePhoto = produce(this.subject.getValue(), (draft) => {
      draft.push(photo);
    });
    this.subject.next(updatePhoto);
    this.notifyService.notify('success', 'Arka Plan Resmi Yüklendi...');
  }

  updateBgPhoto(photo: IFoodMenuBgPhoto) {
    const update$ = this.httpClient
      .put<IFoodMenuBgPhoto>(this.apiUrl + 'foodmenubgphotos', photo)
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
              const photoIndex = draft.findIndex((x) => x.id == result.id);
              draft[photoIndex] = result;
            }
          );
          this.subject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Dosya Güncellendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  deleteBgPhoto(model: IFoodMenuBgPhoto) {
    const deletePhoto$ = this.httpClient
      .delete<IFoodMenuBgPhoto>(this.apiUrl + 'foodmenubgphotos/' + model.id)
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
              const photoIndex = draft.findIndex((x) => x.id === photo.id);
              if (photoIndex != -1) {
                draft.splice(photoIndex, 1);
              }
            }
          );
          this.subject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Dosya Silindi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(deletePhoto$).subscribe();
  }

  setPhotoAsBackground(photo: Partial<IFoodMenuBgPhoto>) {
    const update$ = this.httpClient
      .put<IFoodMenuBgPhoto>(this.apiUrl + 'foodmenubgphotos/setbg', photo)
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
              const photoIndex = draft.findIndex((x) => x.id == result.id);
              const currentBgIndex = draft.findIndex(
                (x) => x.isSetBackground == true
              );
              if (currentBgIndex>=0) {
                draft[currentBgIndex].isSetBackground = false;
              }
              draft[photoIndex] = result;
            }
          );
          this.subject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Resim Güncellendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }
}
