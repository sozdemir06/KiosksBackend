import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IAnnounceForPublic } from '../models/IAnnounceForPublic';
import { IPagination } from 'src/app/shared/models/IPagination';
import { LoadingService } from 'src/app/core/services/loading-service';
import { NotifyService } from 'src/app/core/services/notify-service';
import { IUser } from 'src/app/shared/models/IUser';
import { catchError, map, tap } from 'rxjs/operators';
import { AnnounceParams } from 'src/app/shared/models/AnnounceParams';
import produce from 'immer';
import { IAnnouncePhoto } from 'src/app/shared/models/IAnnouncePhoto';

const AUTH_DATA = 'auth_data';

@Injectable({ providedIn: 'root' })
export class UserAnnounceStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<IPagination<IAnnounceForPublic>>(null);
  announces$: Observable<
    IPagination<IAnnounceForPublic>
  > = this.subject.asObservable();
  announceParams = new AnnounceParams();
  user: IUser;

  constructor(
    private httpClient: HttpClient,
    private laodingService: LoadingService,
    private notifyService: NotifyService
  ) {
    const user: IUser = JSON.parse(localStorage.getItem(AUTH_DATA));
    if (user) {
      this.getList(user, this.announceParams);
      this.user = user;
    }
  }

  private getList(user: IUser, announceParams: AnnounceParams) {
    let params = new HttpParams();
    if (announceParams.search) {
      params = params.append('search', announceParams.search);
    }
    if (announceParams.sort) {
      params = params.append('sort', announceParams.sort);
    }
    if (announceParams.screenId) {
      params = params.append('screenId', announceParams.screenId.toString());
    }
    if (announceParams.subScreenId) {
      params = params.append(
        'subScreenId',
        announceParams.subScreenId.toString()
      );
    }
    if (announceParams.isNew) {
      params = params.append('isNew', announceParams.isNew.toString());
    }
    if (announceParams.isPublish) {
      params = params.append('isPublish', announceParams.isPublish.toString());
    }
    if (announceParams.reject) {
      params = params.append('reject', announceParams.reject.toString());
    }

    params = params.append('pageIndex', announceParams.pageIndex.toString());
    params = params.append('pageSize', announceParams.pageSize.toString());

    const list$ = this.httpClient
      .get<IPagination<IAnnounceForPublic>>(
        this.apiUrl + 'PublicUserAnnounce/announces/' + user.userId,
        { params }
      )
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
    this.laodingService.showLoaderUntilCompleted(list$).subscribe();
  }

  create(model: IAnnounceForPublic, userId: number) {
    const create$ = this.httpClient
      .post<IAnnounceForPublic>(
        this.apiUrl + 'announces/createforuser/' + userId,
        model
      )
      .pipe(
        map((announce) => announce),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announce) => {
          const updateSubject = produce(this.subject.getValue(), (draft) => {
            draft.data.push(announce);
          });
          this.subject.next(updateSubject);
          this.notifyService.notify('success', 'Yeni duyuru eklendi...');
        })
      );
    this.laodingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: IAnnounceForPublic, userId: number) {
    const update$ = this.httpClient
      .put<IAnnounceForPublic>(
        this.apiUrl + 'announces/updateforuser/' + userId,
        model
      )
      .pipe(
        map((announce) => announce),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announce) => {
          const updateSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id === announce.id);
            if (index != -1) {
              draft.data[index] = announce;
            }
          });
          this.subject.next(updateSubject);
          this.notifyService.notify('success', 'Duyuru güncellendi...');
        })
      );
    this.laodingService.showLoaderUntilCompleted(update$).subscribe();
  }

  addPhoto(model: IAnnouncePhoto, announceId: number) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.data.findIndex((x) => x.id === announceId);
      if (index != -1) {
        draft.data[index].announcePhotos.push(model);
      }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', 'Fotoğraf Eklendi...');
  }

  getParams(): AnnounceParams {
    return this.announceParams;
  }

  setParams(params: AnnounceParams): void {
    this.announceParams = params;
  }

  getListByParams() {
    this.getList(this.user, this.announceParams);
  }
}
