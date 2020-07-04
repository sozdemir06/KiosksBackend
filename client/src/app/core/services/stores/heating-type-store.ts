import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IHeatingType } from 'src/app/shared/models/IHeatingType';
import { NotifyService } from '../notify-service';
import { LoadingService } from '../loading-service';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class HeatingTypeStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<IHeatingType[]>([]);
  heatingTypes$: Observable<IHeatingType[]> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private notifyService: NotifyService,
    private loadingService: LoadingService
  ) {
    this.getList();
  }

  private getList() {
    const heatingTypeList$ = this.httpClient
      .get<IHeatingType[]>(this.apiUrl + 'heatingtypes')
      .pipe(
        map((heatingtypes) => heatingtypes),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((heatingtypes) => {
          this.subject.next(heatingtypes);
        })
      );
    this.loadingService.showLoaderUntilCompleted(heatingTypeList$).subscribe();
  }

  create(model: IHeatingType) {
    const create$ = this.httpClient
      .post<IHeatingType>(this.apiUrl + 'heatingtypes', model)
      .pipe(
        map((heatingtypes) => heatingtypes),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((heatingtypes) => {
          const newItem = produce(this.subject.getValue(), (draft) => {
            draft.push(heatingtypes);
          });
          this.subject.next(newItem);
          this.notifyService.notify('success', 'Yeni opsiyon eklendi...');
        })
      );

    this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: IHeatingType) {
    const update$ = this.httpClient
      .put<IHeatingType>(this.apiUrl + 'heatingtypes', model)
      .pipe(
        map((heatingtypes) => heatingtypes),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((heatingtypes) => {
          const updatedItem = produce(this.subject.getValue(), (draft) => {
            const index = draft.findIndex((x) => x.id === heatingtypes.id);

            const updateItem: IHeatingType = {
              ...draft[index],
              ...heatingtypes,
            };

            draft[index] = updateItem;
          });

          this.subject.next(updatedItem);
          this.notifyService.notify('success', 'Güncelleme işlemi başarılı');
        })
      );

    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  delete(model: IHeatingType) {
    const deleted$ = this.httpClient
      .delete<IHeatingType>(this.apiUrl + 'heatingtypes/' + model.id)
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
