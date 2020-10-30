import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { INotifyGroup } from 'src/app/shared/models/INotifyGroup';
import { IUserNotifyGroup } from 'src/app/shared/models/IUserNotifyGroup';
import { catchError, map, tap } from 'rxjs/operators';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class UserNotifyStore {
  apiUrl: string = environment.apiUrl;
  private notifyGroupSubject = new BehaviorSubject<INotifyGroup[]>([]);
  notifyGroups$: Observable<
    INotifyGroup[]
  > = this.notifyGroupSubject.asObservable();
  private userNotifyGroupSubject = new BehaviorSubject<IUserNotifyGroup[]>([]);
  userNotifyGroups$: Observable<
    IUserNotifyGroup[]
  > = this.userNotifyGroupSubject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getNotifyGroupList();
  }

  private getNotifyGroupList() {
    const list$ = this.httpClient
      .get<INotifyGroup[]>(this.apiUrl + 'notifygroup')
      .pipe(
        map((groups) => groups),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((groups) => {
          this.notifyGroupSubject.next(groups);
        })
      );
    this.loadingService.showLoaderUntilCompleted(list$).subscribe();
  }

  getUserNotifyGroup(userId: number) {
    this.userNotifyGroupSubject.next([]);
    const list$ = this.httpClient
      .get<IUserNotifyGroup[]>(this.apiUrl + 'usernotifygroups/' + userId)
      .pipe(
        map((groups) => groups),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((groups) => {
          this.userNotifyGroupSubject.next(groups);
        })
      );
    this.loadingService.showLoaderUntilCompleted(list$).subscribe();
  }

  addNewNotifyGroupForUser(model: Partial<IUserNotifyGroup>) {
    const addToGroup$ = this.httpClient
      .post<IUserNotifyGroup>(this.apiUrl + 'usernotifygroups', model)
      .pipe(
        map((group) => group),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),

        tap((group) => {
          const updateSubject = produce(
            this.userNotifyGroupSubject.getValue(),
            (draft) => {
              draft.push(group);
            }
          );
          this.userNotifyGroupSubject.next(updateSubject);
        })
      );
    this.loadingService.showLoaderUntilCompleted(addToGroup$).subscribe();
  }

  removeNotifyGroupForUser(group: IUserNotifyGroup) {
    const remove$ = this.httpClient
      .delete<IUserNotifyGroup>(this.apiUrl + 'usernotifygroups/' + group.id)
      .pipe(
        map((group) => group),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((group) => {
          const updateSubject = produce(
            this.userNotifyGroupSubject.getValue(),
            (draft) => {
              const index = draft.findIndex((x) => x.id == group.id);
              if (index != -1) {
                draft.splice(index, 1);
              }
            }
          );
          this.userNotifyGroupSubject.next(updateSubject);
        })
      );
    this.loadingService.showLoaderUntilCompleted(remove$).subscribe();
  }
}
