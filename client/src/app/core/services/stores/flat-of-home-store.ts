import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { IFlatOfHome } from 'src/app/shared/models/IFlatOfHome';
import { map, catchError, tap, shareReplay } from 'rxjs/operators';
import { NotifyService } from '../notify-service';
import { LoadingService } from '../loading-service';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class FlatOfHomeStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<IFlatOfHome[]>([]);
  flatsofhome$: Observable<IFlatOfHome[]> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private notifyService: NotifyService,
    private loadingService: LoadingService
  ) {
    this.getList();
  }

  private getList() {
    const flatsOfHomeList$ = this.httpClient
      .get<IFlatOfHome[]>(this.apiUrl + 'FlatsOfHome')
      .pipe(
        map((flatsOfHome) => flatsOfHome),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return catchError(error);
        }),
        tap((flatsOfHome) => {
          flatsOfHome.sort((a, b) => a.name.localeCompare(b.name));
          this.subject.next(flatsOfHome);
        }),
        shareReplay()
      );

    this.loadingService.showLoaderUntilCompleted(flatsOfHomeList$).subscribe();
  }

  create(model: IFlatOfHome) {
    const create$ = this.httpClient
      .post<IFlatOfHome>(this.apiUrl + 'flatsofhome', model)
      .pipe(
        map((flatsOfHome) => flatsOfHome),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return catchError(error);
        }),
        tap((flatsOfHome) => {
          const cretaed = produce(this.subject.getValue(), (draft) => {
            draft.push(flatsOfHome);
          });
          this.subject.next(cretaed);
          this.notifyService.notify('success', 'Opsiyon eklendi');
        })
      );

    this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: Partial<IFlatOfHome>) {
    const update$ = this.httpClient
      .put<IFlatOfHome>(this.apiUrl + 'flatsofhome', model)
      .pipe(
        map((flatsOfHome) => flatsOfHome),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return catchError(error);
        }),
        tap((flatsOfHomes) => {
          const updated = produce(this.subject.getValue(), (draft) => {
            const index = draft.findIndex((x) => x.id === flatsOfHomes.id);

            const updatedNew: IFlatOfHome = {
              ...draft[index],
              ...flatsOfHomes,
            };

            draft[index] = updatedNew;
          });
          this.subject.next(updated);
          this.notifyService.notify('success', 'Güncellendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  delete(model: IFlatOfHome) {
    const delete$ = this.httpClient
      .delete<IFlatOfHome>(this.apiUrl + 'flatsofhome/' + model.id)
      .pipe(
        map((flatsOfHome) => flatsOfHome),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return catchError(error);
        }),
        tap((flatsOfHome) => {
          const deleted = produce(this.subject.getValue(), (draft) => {
            const index = draft.findIndex((x) => x.id === flatsOfHome.id);
            if (index != -1) {
              draft.splice(index, 1);
            }
          });
          this.subject.next(deleted);
          this.notifyService.notify('success', 'Silindi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(delete$).subscribe();
  }
}
