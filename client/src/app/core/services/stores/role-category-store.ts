import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IRoleCategory } from 'src/app/shared/models/IRoleCategory';
import { map, catchError, tap, shareReplay, delay } from 'rxjs/operators';
import { NotifyService } from '../notify-service';
import { LoadingService } from '../loading-service';
import produce from 'immer';
import { IRole } from 'src/app/shared/models/IRole';

@Injectable({ providedIn: 'root' })
export class RoleCategorStore {
  apiUrl: string = environment.apiUrl;

  private subject = new BehaviorSubject<IRoleCategory[]>([]);
  rolesCategories$: Observable<IRoleCategory[]> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private notifyService: NotifyService,
    private loadingService: LoadingService
  ) {
    this.getRolesCategories();
  }

  private getRolesCategories() {
    const roleCategories$ = this.httpClient
      .get<IRoleCategory[]>(this.apiUrl + 'rolescategory')
      .pipe(
        delay(1000),
        map((rolecategories) => rolecategories),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((rolesCategories) => this.subject.next(rolesCategories)),
        shareReplay()
      );

    this.loadingService.showLoaderUntilCompleted(roleCategories$).subscribe();
  }

  create(model: IRoleCategory) {
    const roleCategory$ = this.httpClient
      .post<IRoleCategory>(this.apiUrl + 'rolescategory', model)
      .pipe(
        map((rolecategory) => rolecategory),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((rolecategory) => {
          const newRoleCategory = produce(this.subject.getValue(), (draft) => {
            draft.push(rolecategory);
          });

          this.subject.next(newRoleCategory);
          this.notifyService.notify('success', 'Yetki Kategori Eklendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(roleCategory$).subscribe();
  }

  update(model: Partial<IRoleCategory>) {
    const updateRoleCategory$ = this.httpClient
      .put<IRoleCategory>(this.apiUrl + 'rolescategory', model)
      .pipe(
        map((rolecategory) => rolecategory),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((rolecategory) => {
          const updatedState = produce(this.subject.getValue(), (draft) => {
            const index = draft.findIndex((x) => x.id == model.id);

            const updateRoleCategory: IRoleCategory = {
              ...draft[index],
              ...rolecategory,
            };

            draft[index] = updateRoleCategory;
          });
          this.subject.next(updatedState);
          this.notifyService.notify('success', 'Yetki kategori güncellendi...');
        })
      );
    this.loadingService
      .showLoaderUntilCompleted(updateRoleCategory$)
      .subscribe();
  }
}
