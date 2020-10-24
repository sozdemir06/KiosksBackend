import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';
import { IExchangeRate } from 'src/app/shared/models/IExchangeRate';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class ExchangeRateStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<IExchangeRate[]>([]);
  exchangerates$: Observable<IExchangeRate[]> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {

      this.getList();
     
  }

  private getList() {
    const list$ = this.httpClient
      .get<IExchangeRate[]>(this.apiUrl + 'exchangerates')
      .pipe(
        map((forecasts) => forecasts),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((forecasts) => {
          this.subject.next(forecasts);
        })
      );
    this.loadingService.showLoaderUntilCompleted(list$).subscribe();
  }


  updateRealTime(exchangeRate:IExchangeRate[]){
     this.subject.next(exchangeRate);
     this.notifyService.notify("success","Yeni Kur Eklendi...");
  }

  getListByInterval(){
    this.getList();
  }
}
