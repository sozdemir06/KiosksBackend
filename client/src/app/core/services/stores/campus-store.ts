import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { ICampus } from 'src/app/shared/models/ICampus';
import { environment } from 'src/environments/environment';
import { map, catchError, tap, shareReplay, delay } from 'rxjs/operators';
import { NotifyService } from '../notify-service';
import { LoadingService } from '../loading-service';


@Injectable({ providedIn: 'root' })
export class CampusStore {
  apiUrl = environment.apiUrl;

  private subject = new BehaviorSubject<ICampus[]>([]);
  campus$: Observable<ICampus[]> = this.subject.asObservable();


  constructor(
    private httpClient: HttpClient,
    private notifyService: NotifyService,
    private loadingService: LoadingService,
  ) {
    this.getList();
  }

  private getList() {
    const campuses$ = this.httpClient
      .get<ICampus[]>(this.apiUrl + 'campuses')
      .pipe(
        map((campuses) => campuses),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((campuses) => {
          campuses.sort((a, b) => a.name.localeCompare(b.name));
          this.subject.next(campuses);
        }),
        shareReplay()
      );
    this.loadingService.showLoaderUntilCompleted(campuses$).subscribe();
  }
}
