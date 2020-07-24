import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { throwError } from 'rxjs';
import { IHomeAnnouncePhoto } from 'src/app/shared/models/IHomeAnnouncePhoto';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { map, catchError, tap } from 'rxjs/operators';
import { HomeAnnounceStore } from './home-announce-store';

@Injectable({ providedIn: 'root' })
export class HomeAnnouncePhotoStore {
  apiUrl: string = environment.apiUrl;


  constructor(
    private httpClient: HttpClient,
    private homeAnnounceStore:HomeAnnounceStore,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {}


  create(announceId:number, photo: IHomeAnnouncePhoto) {
     this.homeAnnounceStore.addPhoto(announceId,photo);
  }

  update(announceId:number, photo: IHomeAnnouncePhoto) {
    const update$ = this.httpClient
      .put<IHomeAnnouncePhoto>(this.apiUrl + 'homeannouncephoto', photo)
      .pipe(
        map((photo) => photo),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((photo) => {
          this.homeAnnounceStore.updatePhoto(announceId,photo);
          this.notifyService.notify('success', 'Fotoğraf Güncellendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  delete(announceId:number, photo: IHomeAnnouncePhoto) {
    const delete$ = this.httpClient
      .delete<IHomeAnnouncePhoto>(this.apiUrl + 'homeannouncephoto/' + photo.id)
      .pipe(
        map((photo) => photo),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((photo) => {
          this.homeAnnounceStore.deletePhoto(announceId,photo)
          this.notifyService.notify('success', 'Fotoğraf Silindi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(delete$).subscribe();
  }
}
