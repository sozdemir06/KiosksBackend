import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IPagination } from 'src/app/shared/models/IPagination';
import { LoadingService } from 'src/app/core/services/loading-service';
import { NotifyService } from 'src/app/core/services/notify-service';
import { IUser } from 'src/app/shared/models/IUser';
import { catchError, map, tap } from 'rxjs/operators';
import { IHomeAnnounceForPublic } from '../models/IHomeAnnounceForPublic';
import { HomeAnnounceParams } from 'src/app/shared/models/HomeAnnounceParams';
import produce from 'immer';
import { IHomeAnnouncePhoto } from 'src/app/shared/models/IHomeAnnouncePhoto';

const AUTH_DATA="auth_data";

@Injectable({ providedIn: 'root' })
export class UserHomeAnnounceStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<IPagination<IHomeAnnounceForPublic>>(
    null
  );
  homeannounces$: Observable<
    IPagination<IHomeAnnounceForPublic>
  > = this.subject.asObservable();

  homeAnnounceParams=new HomeAnnounceParams();
  user:IUser;

  constructor(
    private httpClient: HttpClient,
    private laodingService: LoadingService,
    private notifyService: NotifyService
  ) {
      const user:IUser=JSON.parse(localStorage.getItem(AUTH_DATA));
      if(user){
        this.getList(user,this.homeAnnounceParams);
        this.user=user;
      }
  }

 private getList(user: IUser,announceparams:HomeAnnounceParams) {
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
    const list$ = this.httpClient
      .get<IPagination<IHomeAnnounceForPublic>>(
        this.apiUrl + 'PublicUserAnnounce/homeannounces/' + user.userId,{params}
      )
      .pipe(
        map((homeannounces) => homeannounces),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((homeannounces) => {
          this.subject.next(homeannounces);
        })
      );
    this.laodingService.showLoaderUntilCompleted(list$).subscribe();
  }

  create(model:IHomeAnnounceForPublic,userId:number){
    const create$ = this.httpClient
    .post<IHomeAnnounceForPublic>(
      this.apiUrl + 'homeannounces/createforuser/' + userId,
      model
    )
    .pipe(
      map((homeannounce) => homeannounce),
      catchError((error) => {
        this.notifyService.notify('error', error);
        return throwError(error);
      }),
      tap((homeannounce) => {
        const updateSubject = produce(this.subject.getValue(), (draft) => {
          draft.data.push(homeannounce);
        });
        this.subject.next(updateSubject);
        this.notifyService.notify('success', 'Yeni Ev ilanı eklendi...');
      })
    );
  this.laodingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model:IHomeAnnounceForPublic,userId:number){
    const update$ = this.httpClient
      .put<IHomeAnnounceForPublic>(
        this.apiUrl + 'homeannounces/updateforuser/' + userId,
        model
      )
      .pipe(
        map((homeannounce) => homeannounce),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((homeannounce) => {
          const updateSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id === homeannounce.id);
            if (index != -1) {
              draft.data[index] = homeannounce;
            }
          });
          this.subject.next(updateSubject);
          this.notifyService.notify('success', 'Ev ilanı güncellendi...');
        })
      );
    this.laodingService.showLoaderUntilCompleted(update$).subscribe();
  }

  addPhoto(model: IHomeAnnouncePhoto, announceId: number) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.data.findIndex((x) => x.id === announceId);
      if (index != -1) {
        draft.data[index].homeAnnouncePhotos.push(model);
      }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', 'Fotoğraf Eklendi...');
  }


  getParams():HomeAnnounceParams{
      return this.homeAnnounceParams;
  }

  setParams(params:HomeAnnounceParams):void{
      this.homeAnnounceParams=params;
  }

  getListByParams(){
      this.getList(this.user,this.homeAnnounceParams);
  }
}
