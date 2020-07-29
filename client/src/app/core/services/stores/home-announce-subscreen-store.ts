import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { catchError, map, tap } from 'rxjs/operators';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { throwError, BehaviorSubject, Observable } from 'rxjs';
import { IHomeAnnounceSubScreen } from 'src/app/shared/models/IHomeAnnounceSubScreen';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class HomeAnnounceSubScreenStore {
  apiUr: string = environment.apiUrl;
  private subject = new BehaviorSubject<IHomeAnnounceSubScreen[]>([]);
  homeAnnounceSubScreens$: Observable<
    IHomeAnnounceSubScreen[]
  > = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
    this.setSubjectAsNull();
  }

  private setSubjectAsNull() {
    this.subject.next([]);
  }

  getSubScreenByAnnounceId(announceId: number) {
    const list$ = this.httpClient
      .get<IHomeAnnounceSubScreen[]>(
        this.apiUr + 'HomeAnnounceSubScreens/' + announceId
      )
      .pipe(
        map((subscreens) => subscreens),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((subScreens) => {
          this.subject.next(subScreens);
        })
      );
    this.loadingService.showLoaderUntilCompleted(list$).subscribe();
  }

  create(announceId: number, subscreen: Partial<IHomeAnnounceSubScreen>) {
    const create$ = this.httpClient
      .post<IHomeAnnounceSubScreen>(
        this.apiUr + 'homeannouncesubscreens',
        subscreen
      )
      .pipe(
        map((subscreen) => subscreen),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((subscreen) => {
          const createSubject = produce(this.subject.getValue(), (draft) => {
            draft.push(subscreen);
          });
          this.subject.next(createSubject);
          this.notifyService.notify('success', 'Yayın Ekranı Eklendi');
        })
      );
    this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  delete(homeAnnouncesubScreenId: number) {
    const delete$ = this.httpClient
      .delete<IHomeAnnounceSubScreen>(
        this.apiUr + 'homeannouncesubscreens/' + homeAnnouncesubScreenId
      )
      .pipe(
        map((subscreen) => subscreen),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((subscreen) => {
          const deleteSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.findIndex((x) => x.id == subscreen.id);
            if (index != -1) {
              draft.splice(index, 1);
            }
          });
          this.subject.next(deleteSubject);
          this.notifyService.notify('success', 'Yayın Ekranı Kaldırıldı');
        })
      );
    this.loadingService.showLoaderUntilCompleted(delete$).subscribe();
  }
}
