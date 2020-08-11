import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IAnnounceContentType } from 'src/app/shared/models/IAnnounceContentType';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class AnnounceContentTypeStore {
  apiUrl: string = environment.apiUrl;

  private subject = new BehaviorSubject<IAnnounceContentType[]>([]);
  announcecontenttypes$: Observable<
    IAnnounceContentType[]
  > = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getList();
  }

  private getList() {
    const list$ = this.httpClient
      .get<IAnnounceContentType[]>(this.apiUrl + 'AnnounceContentType')
      .pipe(
        map((contentypes) => contentypes),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((contentypes) => {
          this.subject.next(contentypes);
        })
      );
    this.loadingService.showLoaderUntilCompleted(list$).subscribe();
  }

  create(model: IAnnounceContentType) {
    const create$ = this.httpClient
      .post<IAnnounceContentType>(this.apiUrl + 'AnnounceContentType', model)
      .pipe(
        map((contenttype) => contenttype),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((contenttype) => {
          const updateSubject = produce(this.subject.getValue(), (draft) => {
            draft.push(contenttype);
          });
          this.subject.next(updateSubject);
          this.notifyService.notify('success', 'Opsion Eklendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: Partial<IAnnounceContentType>) {
    const update$ = this.httpClient
      .put<IAnnounceContentType>(this.apiUrl + 'AnnounceContentType', model)
      .pipe(
        map((contenttype) => contenttype),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return catchError(error);
        }),
        tap((contenttype) => {
          const updated = produce(this.subject.getValue(), (draft) => {
            const index = draft.findIndex((x) => x.id === contenttype.id);

            const updatedNew: IAnnounceContentType = {
              ...draft[index],
              ...contenttype,
            };

            draft[index] = updatedNew;
          });
          this.subject.next(updated);
          this.notifyService.notify('success', 'Güncellendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  delete(model: IAnnounceContentType) {
    const delete$ = this.httpClient
      .delete<IAnnounceContentType>(this.apiUrl + 'AnnounceContentType/' + model.id)
      .pipe(
        map((contenttype) => contenttype),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return catchError(error);
        }),
        tap((contenttype) => {
          const deleted = produce(this.subject.getValue(), (draft) => {
            const index = draft.findIndex((x) => x.id === contenttype.id);
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
