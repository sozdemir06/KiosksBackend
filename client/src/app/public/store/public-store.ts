import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoadingService } from 'src/app/core/services/loading-service';
import { NotifyService } from 'src/app/core/services/notify-service';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IPublic } from '../models/IPublic';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class PublicStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<IPublic>(null);
  allannounces$: Observable<IPublic> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getList();
  }

  private getList() {
    const list$ = this.httpClient.get<IPublic>(this.apiUrl + 'public/all').pipe(
      map((data) => data),
      catchError((error) => {
        this.notifyService.notify('error', error);
        return throwError(error);
      }),
      tap((data) => {
        this.subject.next(data);
      })
    );
    this.loadingService.showLoaderUntilCompleted(list$).subscribe();
  }
}
