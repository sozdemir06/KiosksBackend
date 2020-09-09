import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { ICity } from 'src/app/shared/models/ICity';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';
import { ICurrency } from 'src/app/shared/models/ICurrency';

@Injectable({ providedIn: 'root' })
export class CurrencyStore {
  apiUrl: string = environment.apiUrl;

  private subject = new BehaviorSubject<ICurrency[]>([]);
  currencies$: Observable<ICurrency[]> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getList();
  }

  private getList() {
    const list$ = this.httpClient.get<ICurrency[]>(this.apiUrl + 'Currencies').pipe(
      map((currencies) => currencies),
      catchError((error) => {
        this.notifyService.notify('error', error);
        return throwError(error);
      }),
      tap((currencies) => {
        currencies.sort((a, b) => a.name.localeCompare(b.name));
        this.subject.next(currencies);
      })
    );
    this.loadingService.showLoaderUntilCompleted(list$).subscribe();
  }

  update(model: ICurrency) {
    const update$ = this.httpClient
      .put<ICurrency>(this.apiUrl + 'Currencies', model)
      .pipe(
        map((currency) => currency),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((currency) => {
          const updatedSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.findIndex((x) => x.id === currency.id);
            if (index != -1) {
              draft[index] = currency;
            }
          });
          this.subject.next(updatedSubject);
          this.notifyService.notify('success', 'Güncellendi...');
        })
      );
      this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }
}