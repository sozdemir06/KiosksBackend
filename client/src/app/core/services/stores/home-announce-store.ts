import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError, partition } from 'rxjs';
import { IHomeAnnounce } from 'src/app/shared/models/IHomeAnnounce';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { HomeAnnounceParams } from 'src/app/shared/models/HomeAnnounceParams';
import { map, catchError, tap, publish } from 'rxjs/operators';
import produce from 'immer';
import { IPagination } from 'src/app/shared/models/IPagination';
import { IHomeAnnouncePhoto } from 'src/app/shared/models/IHomeAnnouncePhoto';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { IHomeAnnounceSubScreen } from 'src/app/shared/models/IHomeAnnounceSubScreen';
import { IHomeAnnounceDetail } from 'src/app/shared/models/IHomeAnnounceDetail';

@Injectable({ providedIn: 'root' })
export class HomeAnnounceStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<IPagination<IHomeAnnounce>>(null);
  homeAnnounces$: Observable<
    IPagination<IHomeAnnounce>
  > = this.subject.asObservable();

  private detailSubject = new BehaviorSubject<IHomeAnnounceDetail>(null);
  detail$: Observable<IHomeAnnounceDetail> = this.detailSubject.asObservable();

  announceparams = new HomeAnnounceParams();

  constructor(
    private httpClient: HttpClient,
    private loadingservice: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getList(this.announceparams);
  }

  private getList(announceparams: HomeAnnounceParams) {
    let params = new HttpParams();
    if (announceparams.search) {
      params = params.append('search', announceparams.search);
    }
    if (announceparams.sort) {
      params = params.append('sort', announceparams.sort);
    }
    if (announceparams.screenId) {
      params = params.append('screenId', announceparams.screenId.toString());
    }
    if (announceparams.subScreenId) {
      params = params.append(
        'subScreenId',
        announceparams.subScreenId.toString()
      );
    }
    if (announceparams.isNew) {
      params = params.append('isNew', announceparams.isNew.toString());
    }
    if (announceparams.isPublish) {
      params = params.append('isPublish', announceparams.isPublish.toString());
    }
    if (announceparams.reject) {
      params = params.append('reject', announceparams.reject.toString());
    }

    params = params.append('pageIndex', announceparams.pageIndex.toString());
    params = params.append('pageSize', announceparams.pageSize.toString());

    const announceList$ = this.httpClient
      .get<IPagination<IHomeAnnounce>>(this.apiUrl + 'homeannounces', {
        params,
      })
      .pipe(
        map((announces) => announces),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announces) => {
          this.subject.next(announces);
        })
      );
    this.loadingservice.showLoaderUntilCompleted(announceList$).subscribe();
  }

  getNewAnnounceLength(): Observable<number> {
    return this.homeAnnounces$.pipe(
      map((announces) => announces?.data.filter((x) => x.isNew == true).length)
    );
  }

  create(model: IHomeAnnounce) {
    const create$ = this.httpClient
      .post<IHomeAnnounce>(this.apiUrl + 'homeannounces', model)
      .pipe(
        map((announces) => announces),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announces) => {
          const newItem = produce(this.subject.getValue(), (draft) => {
            draft.data.push(announces);
          });
          this.subject.next(newItem);
          this.notifyService.notify('success', 'Yeni Ev ilanı Eklendi...');
        })
      );
    this.loadingservice.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: Partial<IHomeAnnounce>) {
    const update$ = this.httpClient
      .put<IHomeAnnounce>(this.apiUrl + 'homeannounces', model)
      .pipe(
        map((announce) => announce),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announce) => {
          const updatedSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id === announce.id);
            const update: IHomeAnnounce = {
              ...draft.data[index],
              ...announce,
            };
            draft.data[index] = update;
          });
          this.subject.next(updatedSubject);
          this.notifyService.notify('success', 'Ev ilanı güncellendi...');
        })
      );
    this.loadingservice.showLoaderUntilCompleted(update$).subscribe();
  }

  publish(model: Partial<IHomeAnnounce>) {
    const update$ = this.httpClient
      .put<IHomeAnnounce>(this.apiUrl + 'homeannounces/publish', model)
      .pipe(
        map((announce) => announce),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announce) => {
          const updatedSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id === announce.id);
            const update: IHomeAnnounce = {
              ...draft.data[index],
              ...announce,
            };
            draft.data[index] = update;
          });
          this.subject.next(updatedSubject);
          this.notifyService.notify('success', 'ilan yayına alındı...');
        })
      );
    this.loadingservice.showLoaderUntilCompleted(update$).subscribe();
  }

  getDetail(announceId: number) {
    this.detailSubject.next(null);
    const detail$ = this.httpClient
      .get<IHomeAnnounceDetail>(
        this.apiUrl + 'homeannounces/detail/' + announceId
      )
      .pipe(
        map((detail) => detail),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((detail) => {
          this.detailSubject.next(detail);
        })
      );
    this.loadingservice.showLoaderUntilCompleted(detail$).subscribe();
  }

  //Add new photo to announce
  addPhoto(photo: IHomeAnnouncePhoto) {
    const updatePhoto = produce(this.detailSubject.getValue(), (draft) => {
      draft.homeAnnouncePhotos.push(photo);
    });
    this.detailSubject.next(updatePhoto);
    this.notifyService.notify('success', 'Resim Eklendi...');
  }

  //update photo for announce
  updatePhoto(photo: IHomeAnnouncePhoto) {
    const update$ = this.httpClient
    .put<IHomeAnnouncePhoto>(this.apiUrl + 'HomeAnnouncePhoto', photo)
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
            const photoIndex = draft.homeAnnouncePhotos.findIndex(
              (x) => x.id == result.id
            );
            draft.homeAnnouncePhotos[photoIndex] = result;
          }
        );
        this.detailSubject.next(updateDetailsubject);
        this.notifyService.notify('success', 'Fotoğraf Güncellendi...');
      })
    );
    this.loadingservice.showLoaderUntilCompleted(update$).subscribe();
  }
  //delete photo from announce
  deletePhoto( model: IHomeAnnouncePhoto) {
    const deletePhoto$ = this.httpClient
    .delete<IHomeAnnouncePhoto>(
      this.apiUrl + 'HomeAnnouncePhoto/' + model.id
    )
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
            const photoIndex = draft.homeAnnouncePhotos.findIndex(
              (x) => x.id === photo.id
            );
            if (photoIndex != -1) {
              draft.homeAnnouncePhotos.splice(photoIndex, 1);
            }
          }
        );
        this.detailSubject.next(updateDetailsubject);
        this.notifyService.notify('success', 'Fotoğraf Silindi...');
      })
    );
  this.loadingservice.showLoaderUntilCompleted(deletePhoto$).subscribe();
  }
  addSubScreen(model: Partial<IHomeAnnounceSubScreen>) {
    const addSubScreen$ = this.httpClient
      .post<IHomeAnnounceSubScreen>(
        this.apiUrl + 'homeannouncesubscreens',
        model
      )
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
              draft.homeAnnounceSubScreens.push(subscreen);
            }
          );
          this.detailSubject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Yayın Ekranı Eklendi...');
        })
      );
    this.loadingservice.showLoaderUntilCompleted(addSubScreen$).subscribe();
  }

  removeSubScreen(model:IHomeAnnounceSubScreen){
    const removeSubScreen$=this.httpClient.delete<IHomeAnnounceSubScreen>(this.apiUrl+"homeannouncesubscreens/"+model.id)
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
            const index=draft.homeAnnounceSubScreens.findIndex(x=>x.id===subscreen.id);
            if(index!=-1){
               draft.homeAnnounceSubScreens.splice(index,1);
            }
          }
        );
        this.detailSubject.next(updateDetailsubject);
        this.notifyService.notify('success', 'Yayın Ekranı Kaldırıldı...');
      })
    );
    this.loadingservice.showLoaderUntilCompleted(removeSubScreen$).subscribe();
  }

  getHomeAnnounceParams(): HomeAnnounceParams {
    return this.announceparams;
  }

  setHomeAnnounceParams(params: HomeAnnounceParams) {
    this.announceparams = params;
  }

  invokeGetlistWithParams(): void {
    this.getList(this.announceparams);
  }
}
