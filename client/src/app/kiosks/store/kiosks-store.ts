import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { LoadingService } from 'src/app/core/services/loading-service';
import { NotifyService } from 'src/app/core/services/notify-service';
import { BehaviorSubject, Observable, throwError, timer } from 'rxjs';
import { IKiosks } from '../models/IKiosks';
import { map, catchError, tap, delay } from 'rxjs/operators';
import { IAnnounce } from 'src/app/shared/models/IAnnounce';

@Injectable({ providedIn: 'root' })
export class KiosksStore {
  apUrl: string = environment.apiUrl;

  private subject = new BehaviorSubject<IKiosks>(null);
  kiosks$: Observable<IKiosks> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {}

  getListByScreenId(screenId: number) {
    const list$ = this.httpClient
      .get<IKiosks>(this.apUrl + 'kiosks/' + screenId)
      .pipe(
        map((kiosks) => kiosks),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((kioks) => {
          this.subject.next(kioks);
        })
      );
    this.loadingService.showLoaderUntilCompleted(list$).subscribe();
  }

}
