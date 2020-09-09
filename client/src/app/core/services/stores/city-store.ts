import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { ICity } from 'src/app/shared/models/ICity';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class CityStore {
  apiUrl: string = environment.apiUrl;

  private subject = new BehaviorSubject<ICity[]>([]);
  cities$: Observable<ICity[]> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getList();
  }

  private getList() {
    const list$ = this.httpClient.get<ICity[]>(this.apiUrl + 'Cities').pipe(
      map((cities) => cities),
      catchError((error) => {
        this.notifyService.notify('error', error);
        return throwError(error);
      }),
      tap((cities) => {
        cities.sort((a, b) => a.name.localeCompare(b.name));
        this.subject.next(cities);
      })
    );
    this.loadingService.showLoaderUntilCompleted(list$).subscribe();
  }

  update(model: ICity) {
    const update$ = this.httpClient
      .put<ICity>(this.apiUrl + 'cities', model)
      .pipe(
        map((city) => city),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((city) => {
          const updatedSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.findIndex((x) => x.id === city.id);
            if (index != -1) {
              draft[index] = city;
            }
          });
          this.subject.next(updatedSubject);
          this.notifyService.notify('success', 'Güncellendi...');
        })
      );
      this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }
}
