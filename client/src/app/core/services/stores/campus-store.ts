import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { ICampus } from 'src/app/shared/models/ICampus';
import { environment } from 'src/environments/environment';
import { map, catchError, tap, shareReplay, delay } from 'rxjs/operators';
import { NotifyService } from '../notify-service';
import { LoadingService } from '../loading-service';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class CampusStore {
  apiUrl = environment.apiUrl;

  private subject = new BehaviorSubject<ICampus[]>([]);
  campus$: Observable<ICampus[]> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private notifyService: NotifyService,
    private loadingService: LoadingService
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

  create(model: ICampus) {
    const create$ = this.httpClient
      .post<ICampus>(this.apiUrl + 'campuses', model)
      .pipe(
        map((campuses) => campuses),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((campuses) => {
          const newItem = produce(this.subject.getValue(), (draft) => {
            draft.push(campuses);
          });
          this.subject.next(newItem);
          this.notifyService.notify('success', 'Yeni opsiyon eklendi...');
        })
      );

    this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: ICampus) {
    const update$ = this.httpClient
      .put<ICampus>(this.apiUrl + 'campuses', model)
      .pipe(
        map((campuses) => campuses),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((campuses) => {
          const updatedItem = produce(this.subject.getValue(), (draft) => {
            const index = draft.findIndex((x) => x.id === campuses.id);

            const updateItem: ICampus = {
              ...draft[index],
              ...campuses,
            };

            draft[index] = updateItem;
          });

          this.subject.next(updatedItem);
          this.notifyService.notify('success', 'Güncelleme işlemi başarılı');
        })
      );

    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  delete(model: ICampus) {
    const deleted$ = this.httpClient
      .delete<ICampus>(this.apiUrl + 'campuses/' + model.id)
      .pipe(
        map((data) => data),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((data) => {
          const deletedItem = produce(this.subject.getValue(), (draft) => {
            const index = draft.findIndex((x) => x.id === data.id);
            if (index !== -1) {
              draft.splice(index, 1);
            }
          });
          this.subject.next(deletedItem);
          this.notifyService.notify('success', 'Silindi...');
        })
      );

    this.loadingService.showLoaderUntilCompleted(deleted$).subscribe();
  }
}
