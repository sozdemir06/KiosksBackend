import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IUserList } from 'src/app/shared/models/IUser';
import { IPagination } from 'src/app/shared/models/IPagination';
import { UserParams } from 'src/app/shared/models/UserParams';
import { environment } from 'src/environments/environment';
import { map, catchError, tap, finalize, delay } from 'rxjs/operators';
import { NotifyService } from './notify-service';

@Injectable({ providedIn: 'root' })
export class UserStore {
  apiUrl = environment.apiUrl;

  private userSubject = new BehaviorSubject<IPagination<IUserList>>(null);
  private loadingSubject = new BehaviorSubject<boolean>(false);
  loading$: Observable<boolean> = this.loadingSubject.asObservable();
  users$: Observable<IPagination<IUserList>> = this.userSubject.asObservable();

  userParams = new UserParams();

  constructor(
    private httpClient: HttpClient,
    private notificationService: NotifyService
  ) {
    this.getUsers(this.userParams);
  }

  getUsers(userParams: UserParams) {
    this.loadingSubject.next(true);
    let params = new HttpParams();

    if (userParams.search) {
      params = params.append('search', userParams.search);
    }
    if (userParams.sort) {
      params = params.append('sort', userParams.sort);
    }

    //params=params.append('status',userParams.status.toString());
    params = params.append('pageIndex', userParams.pageIndex.toString());
    params = params.append('pageSize', userParams.pageSize.toString());

    return this.httpClient
      .get<IPagination<IUserList>>(this.apiUrl + 'users', { params })
      .pipe(
        delay(1000),
        map((result) => result),
        catchError((error) => {
          this.notificationService.notify('error', error);
          return throwError(error);
        }),
        tap((users) => this.userSubject.next(users)),
        finalize(() => this.loadingSubject.next(false))
      )
      .subscribe();
  }

  getUserParams():UserParams{
    return this.userParams;
  }

  setUserParams(userParams:UserParams){
    this.userParams=userParams;
  }

  onGetUsers(){
    this.getUsers(this.userParams);
  }
}
