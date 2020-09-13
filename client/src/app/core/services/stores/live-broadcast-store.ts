import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { LiveTvBroadCastParams } from 'src/app/shared/models/LiveTvBroadCastParams';
import { ILiveTvBroadCast } from 'src/app/shared/models/ILiveTvBroadCast';
import { ILiveTvBroadCastDetail } from 'src/app/shared/models/ILiveTvBroadCastDetail';
import { Observable, BehaviorSubject, throwError } from 'rxjs';
import { IPagination } from 'src/app/shared/models/IPagination';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';
import { ILiveTvBroadCastSubScreen } from 'src/app/shared/models/ILiveTvBroadCastSubScreen';
import { element } from 'protractor';

@Injectable({ providedIn: 'root' })
export class LiveTvBroadCastStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<IPagination<ILiveTvBroadCast>>(null);
  livetvbroadcasts$: Observable<
    IPagination<ILiveTvBroadCast>
  > = this.subject.asObservable();

  private detailSubject = new BehaviorSubject<ILiveTvBroadCastDetail>(null);
  detail$: Observable<
    ILiveTvBroadCastDetail
  > = this.detailSubject.asObservable();

  liveTvParams = new LiveTvBroadCastParams();

  constructor(
    private httpClient: HttpClient,
    private loadingservice: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getList(this.liveTvParams);
  }

  private getList(livetvparams: LiveTvBroadCastParams) {
    let params = new HttpParams();
    if (livetvparams.search) {
      params = params.append('search', livetvparams.search);
    }
    if (livetvparams.sort) {
      params = params.append('sort', livetvparams.sort);
    }
    if (livetvparams.screenId) {
      params = params.append('screenId', livetvparams.screenId.toString());
    }
    if (livetvparams.subScreenId) {
      params = params.append(
        'subScreenId',
        livetvparams.subScreenId.toString()
      );
    }
    if (livetvparams.isNew) {
      params = params.append('isNew', livetvparams.isNew.toString());
    }
    if (livetvparams.isPublish) {
      params = params.append('isPublish', livetvparams.isPublish.toString());
    }
    if (livetvparams.reject) {
      params = params.append('reject', livetvparams.reject.toString());
    }

    params = params.append('pageIndex', livetvparams.pageIndex.toString());
    params = params.append('pageSize', livetvparams.pageSize.toString());
    const announceList$ = this.httpClient
      .get<IPagination<ILiveTvBroadCast>>(this.apiUrl + 'livetvbroadcast', {
        params,
      })
      .pipe(
        map((announces) => announces),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announces) => {
          announces.data.forEach((elem) => {
            elem.slideIntervalTime = elem.slideIntervalTime / 1000 / 60;
          });
          this.subject.next(announces);
        })
      );
    this.loadingservice.showLoaderUntilCompleted(announceList$).subscribe();
  }

  getNewAnnounceLength(): Observable<number> {
    return this.livetvbroadcasts$.pipe(
      map((announces) => announces?.data.filter((x) => x.isNew == true).length)
    );
  }

  create(model: ILiveTvBroadCast) {
    const create$ = this.httpClient
      .post<ILiveTvBroadCast>(this.apiUrl + 'livetvbroadcast', model)
      .pipe(
        map((announces) => announces),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announces) => {
          announces.slideIntervalTime = announces.slideIntervalTime / 1000 / 60;

          const newItem = produce(this.subject.getValue(), (draft) => {
            draft?.data.push(announces);
          });
          this.subject.next(newItem);
          this.notifyService.notify('success', 'Yeni Yayın Eklendi...');
        })
      );
    this.loadingservice.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: Partial<ILiveTvBroadCast>) {
    const update$ = this.httpClient
      .put<ILiveTvBroadCast>(this.apiUrl + 'livetvbroadcast', model)
      .pipe(
        map((announce) => announce),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announce) => {
          announce.slideIntervalTime = announce.slideIntervalTime / 1000 / 60;
          const updatedSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id === announce.id);
            const update: ILiveTvBroadCast = {
              ...draft.data[index],
              ...announce,
            };
            draft.data[index] = update;
          });
          this.subject.next(updatedSubject);
          this.notifyService.notify('success', 'Yayın güncellendi...');
        })
      );
    this.loadingservice.showLoaderUntilCompleted(update$).subscribe();
  }

  publish(model: Partial<ILiveTvBroadCast>) {
    const update$ = this.httpClient
      .put<ILiveTvBroadCast>(this.apiUrl + 'livetvbroadcast/publish', model)
      .pipe(
        map((announce) => announce),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announce) => {
          announce.slideIntervalTime = announce.slideIntervalTime / 1000 / 60;
          const updatedSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id === announce.id);
            const update: ILiveTvBroadCast = {
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
      .get<ILiveTvBroadCastDetail>(
        this.apiUrl + 'livetvbroadcast/detail/' + announceId
      )
      .pipe(
        map((detail) => detail),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((detail) => {
          detail.slideIntervalTime = detail.slideIntervalTime / 1000 / 60;
          this.detailSubject.next(detail);
        })
      );
    this.loadingservice.showLoaderUntilCompleted(detail$).subscribe();
  }

  addSubScreen(model: Partial<ILiveTvBroadCastSubScreen>) {
    const addSubScreen$ = this.httpClient
      .post<ILiveTvBroadCastSubScreen>(
        this.apiUrl + 'livetvbroadcastSubScreens',
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
              draft.liveTvBroadCastSubScreens.push(subscreen);
            }
          );
          this.detailSubject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Yayın Ekranı Eklendi...');
        })
      );
    this.loadingservice.showLoaderUntilCompleted(addSubScreen$).subscribe();
  }

  removeSubScreen(model: ILiveTvBroadCastSubScreen) {
    const removeSubScreen$ = this.httpClient
      .delete<ILiveTvBroadCastSubScreen>(
        this.apiUrl + 'livetvbroadcastSubScreens/' + model.id
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
              const index = draft.liveTvBroadCastSubScreens.findIndex(
                (x) => x.id === subscreen.id
              );
              if (index != -1) {
                draft.liveTvBroadCastSubScreens.splice(index, 1);
              }
            }
          );
          this.detailSubject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Yayın Ekranı Kaldırıldı...');
        })
      );
    this.loadingservice.showLoaderUntilCompleted(removeSubScreen$).subscribe();
  }

  getParams(): LiveTvBroadCastParams {
    return this.liveTvParams;
  }

  setParams(params: LiveTvBroadCastParams) {
    this.liveTvParams = params;
  }

  getListByParams(): void {
    this.getList(this.liveTvParams);
  }
}
