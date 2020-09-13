import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ILiveTvList } from 'src/app/shared/models/ILiveTvList';
import { Observable, BehaviorSubject, throwError } from 'rxjs';
import { NotifyService } from '../notify-service';
import { LoadingService } from '../loading-service';
import { map, catchError, tap, shareReplay } from 'rxjs/operators';
import produce from 'immer';

@Injectable({providedIn: 'root'})
export class LiveTvListStore {
    apiUrl:string=environment.apiUrl;

    private subject = new BehaviorSubject<ILiveTvList[]>([]);
    livetvlists$: Observable<ILiveTvList[]> = this.subject.asObservable();
  
    constructor(
      private httpClient: HttpClient,
      private notifyService: NotifyService,
      private loadingService: LoadingService
    ) {
      this.getList();
    }
  
    private getList() {
      const livetvList$ = this.httpClient
        .get<ILiveTvList[]>(this.apiUrl + 'livetvlist')
        .pipe(
          map((livetvList) => livetvList),
          catchError((error) => {
            this.notifyService.notify('error', error);
            return throwError(error);
          }),
          tap((livetvList) => {
            livetvList.sort((a, b) => a.tvName.localeCompare(b.tvName));
            this.subject.next(livetvList);
          }),
          shareReplay()
        );
  
      this.loadingService.showLoaderUntilCompleted(livetvList$).subscribe();
    }

    create(model: ILiveTvList) {
        const create$ = this.httpClient
          .post<ILiveTvList>(this.apiUrl + 'livetvlist', model)
          .pipe(
            map((livetvList) => livetvList),
            catchError((error) => {
              this.notifyService.notify('error', error);
              return throwError(error);
            }),
            tap((livetvList) => {
              const newItem = produce(this.subject.getValue(), (draft) => {
                draft.push(livetvList);
              });
              this.subject.next(newItem);
              this.notifyService.notify('success', 'Yeni opsiyon eklendi...');
            })
          );
    
        this.loadingService.showLoaderUntilCompleted(create$).subscribe();
      }
    
      update(model: ILiveTvList) {
        const update$ = this.httpClient
          .put<ILiveTvList>(this.apiUrl + 'livetvlist', model)
          .pipe(
            map((livetvList) => livetvList),
            catchError((error) => {
              this.notifyService.notify('error', error);
              return throwError(error);
            }),
            tap((livetvList) => {
              const updatedItem = produce(this.subject.getValue(), (draft) => {
                const index = draft.findIndex((x) => x.id === livetvList.id);
    
                const updateItem: ILiveTvList = {
                  ...draft[index],
                  ...livetvList,
                };
    
                draft[index] = updateItem;
              });
    
              this.subject.next(updatedItem);
              this.notifyService.notify('success', 'Güncelleme işlemi başarılı');
            })
          );
    
        this.loadingService.showLoaderUntilCompleted(update$).subscribe();
      }
    
      delete(model: ILiveTvList) {
        const deleted$ = this.httpClient
          .delete<ILiveTvList>(this.apiUrl + 'livetvlist/' + model.id)
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