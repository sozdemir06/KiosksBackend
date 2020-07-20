import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IHomeAnnounce } from 'src/app/shared/models/IHomeAnnounce';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { HomeAnnounceParams } from 'src/app/shared/models/HomeAnnounceParams';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';
import { IPagination } from 'src/app/shared/models/IPagination';

@Injectable({ providedIn: 'root' })
export class HomeAnnounceStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<IPagination<IHomeAnnounce>>(null);
  homeAnnounces$: Observable<IPagination<IHomeAnnounce>> = this.subject.asObservable();

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
      .get<IPagination<IHomeAnnounce>>(this.apiUrl + 'homeannounces', { params })
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
}
