import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { INumberOfRoom } from 'src/app/shared/models/INumberOFRoom';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';
import { sortByName } from 'src/app/shared/helpers/sort-by-name';

@Injectable({ providedIn: 'root' })
export class NumberOfroomStore {
  apiUrl: string = environment.apiUrl;

  private subject = new BehaviorSubject<INumberOfRoom[]>([]);
  numberOfRooms$: Observable<INumberOfRoom[]> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getList();
  }

  private getList() {
    const numberOfRooms$ = this.httpClient
      .get<INumberOfRoom[]>(this.apiUrl + 'numberofrooms')
      .pipe(
        map((rooms) => rooms.sort()),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((rooms) => {
          rooms.sort(sortByName);
          this.subject.next(rooms)
        })
      );

    this.loadingService.showLoaderUntilCompleted(numberOfRooms$).subscribe();
  }

  create(model: INumberOfRoom) {
    const create$ = this.httpClient
      .post<INumberOfRoom>(this.apiUrl + 'numberofrooms', model)
      .pipe(
        map((rooms) => rooms),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((rooms) => {
          const newItem = produce(this.subject.getValue(), (draft) => {
            draft.push(rooms);
          });

          this.subject.next(newItem);
          this.notifyService.notify('success', 'Ekleme işlemi başarılı');
        })
      );

    this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: Partial<INumberOfRoom>) {
    const update$ = this.httpClient
      .put<INumberOfRoom>(this.apiUrl + 'numberofrooms', model)
      .pipe(
        map((rooms) => rooms),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((rooms) => {
          const updatedItem = produce(this.subject.getValue(), (draft) => {
            const index = draft.findIndex((x) => x.id === rooms.id);

            const updateItem: INumberOfRoom = {
              ...draft[index],
              ...rooms,
            };

            draft[index] = updateItem;
          });

          this.subject.next(updatedItem);
          this.notifyService.notify('success', 'Güncelleme işlemi başarılı');
        })
      );

    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  delete(model: INumberOfRoom) {
    const deleted$ = this.httpClient
      .delete<INumberOfRoom>(this.apiUrl + 'numberofrooms/' + model.id)
      .pipe(
        map((rooms) => rooms),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap(rooms=>{
            const deletedItem=produce(this.subject.getValue(),draft=>{
                const index=draft.findIndex(x=>x.id===rooms.id);
                if(index!==-1){
                    draft.splice(index,1);
                }
            });
            this.subject.next(deletedItem);
            this.notifyService.notify("success","Silindi...");
        })
      );

      this.loadingService.showLoaderUntilCompleted(deleted$).subscribe();
  }
}
