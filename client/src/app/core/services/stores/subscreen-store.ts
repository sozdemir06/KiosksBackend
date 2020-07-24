import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';
import { ActivatedRoute } from '@angular/router';
import { error } from 'protractor';

@Injectable({ providedIn: 'root' })
export class SubScreenStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<ISubScreen[]>([]);
  subScreens$: Observable<ISubScreen[]> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingservice: LoadingService,
    private notifyService: NotifyService,
    private route:ActivatedRoute
  ) {
      
    
  }

 getList(screenId:number) {
    const list$ = this.httpClient
      .get<ISubScreen[]>(this.apiUrl + 'subscreens/')
      .pipe(
        map((subscreens) => subscreens),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((subscreens) => {
          this.subject.next(subscreens);
        })
      );

    this.loadingservice.showLoaderUntilCompleted(list$).subscribe();
  }

  create(model: ISubScreen) {
    const create$ = this.httpClient
      .post<ISubScreen>(this.apiUrl + 'subscreens', model)
      .pipe(
        map((subscreens) => subscreens),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),

        tap((subscreens) => {
          const addNewItem = produce(this.subject.getValue(), (draft) => {
            draft.push(subscreens);
          });

          this.subject.next(addNewItem);
          this.notifyService.notify('success', 'Yeni Alt Ekran Eklendi...');
        })
      );
    this.loadingservice.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: Partial<ISubScreen>) {
    const update$ = this.httpClient
      .put<ISubScreen>(this.apiUrl + 'subscreens/', model)
      .pipe(
        map((subscreens) => subscreens),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),

        tap((subscreens) => {
          const updateItem = produce(this.subject.getValue(), (draft) => {
            const index = draft.findIndex((x) => x.id == subscreens.id);
            const updatedItem: ISubScreen = {
              ...draft[index],
              ...subscreens,
            };
            draft[index] = updatedItem;
          });

          this.subject.next(updateItem);
          this.notifyService.notify('success', 'Alt Ekran Güncellendi...');
        })
      );
    this.loadingservice.showLoaderUntilCompleted(update$).subscribe();
  }

  delete(model: ISubScreen) {
    const delete$ = this.httpClient
      .delete<ISubScreen>(this.apiUrl + 'subscreens/' + model.id)
      .pipe(
        map((subscreens) => subscreens),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((subscreens) => {
          const updateItem = produce(this.subject.getValue(), (draft) => {
            const index = draft.findIndex((x) => x.id == subscreens.id);
            if (index != -1) {
              draft.splice(index, 1);
            }
          });

          this.subject.next(updateItem);
          this.notifyService.notify('success', 'Alt Ekran Silindi...');
        })
      );
    this.loadingservice.showLoaderUntilCompleted(delete$).subscribe();
  }

  getByScreenId(screenId: number){
    const list$ = this.httpClient
      .get<ISubScreen[]>(this.apiUrl + 'subscreens/'+screenId)
      .pipe(
        map((subscreens) => subscreens),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((subscreens) => {
          this.subject.next(subscreens);
        })
      );

    this.loadingservice.showLoaderUntilCompleted(list$).subscribe();
  }

  getScreenListForFilters():Observable<ISubScreen[]>{
    return this.httpClient.get<ISubScreen[]>(this.apiUrl+"subscreens").pipe(
      map(subscreens=>subscreens.filter(x=>x.status==true)),
      catchError(error=>{
        this.notifyService.notify('error', error);
        return throwError(error);
      })
    )
  }
}
