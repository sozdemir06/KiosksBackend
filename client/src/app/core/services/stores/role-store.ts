import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { RoleParams } from 'src/app/shared/models/RoleParams';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IRole } from 'src/app/shared/models/IRole';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { map, catchError, tap, shareReplay, delay } from 'rxjs/operators';
import { IPagination } from 'src/app/shared/models/IPagination';
import { produce } from 'immer';

@Injectable({ providedIn: 'root' })
export class RoleStore {
  apiUrl: string = environment.apiUrl;

  private subject = new BehaviorSubject<IPagination<IRole>>(null);
  roles$: Observable<IPagination<IRole>> = this.subject.asObservable();

  roleParams = new RoleParams();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getRoles(this.roleParams);
  }

  private getRoles(roleParams: RoleParams) {
    let params = new HttpParams();

    if (roleParams.search) {
      params = params.append('search', roleParams.search);
    }
    if (roleParams.sort) {
      params = params.append('sort', roleParams.sort);
    }
    if (roleParams.roleCategoryId) {
      params = params.append(
        'roleCategoryId',
        roleParams.roleCategoryId.toString()
      );
    }

    params = params.append('pageSize', roleParams.pageSize.toString());
    params = params.append('pageIndex', roleParams.pageIndex.toString());

    const roles$ = this.httpClient
      .get<IPagination<IRole>>(this.apiUrl + 'roles', { params })
      .pipe(
        delay(1000),
        map((roles) => roles),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((roles) => {
          roles.data.sort((a, b) => a.name.localeCompare(b.name));
          this.subject.next(roles);
        }),
        shareReplay()
      );

    this.loadingService.showLoaderUntilCompleted(roles$).subscribe();
  }

  create(model: IRole) {
    const role$ = this.httpClient
      .post<IRole>(this.apiUrl + 'roles', model)
      .pipe(
        map((role) => role),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((role) => {
          const newRole = produce(this.subject.getValue(), (draft) => {
            draft.data.push(role);
          });
          this.subject.next(newRole);
          this.notifyService.notify('success', 'Yeni role eklendi...');
        })
      );

    this.loadingService.showLoaderUntilCompleted(role$).subscribe();
  }

  update(model:Partial<IRole>) {
    const updatedRole$ = this.httpClient
      .put<IRole>(this.apiUrl + 'roles', model)
      .pipe(
        map((role) => role),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((role) => {
          const updatedSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id == model.id);

            const updateRole: IRole = {
              ...draft.data[index],
              ...role,
            };
            draft.data[index] = updateRole;
          });

          this.subject.next(updatedSubject);
          this.notifyService.notify('success', 'Yetki Güncellendi...');
        })
      );

      this.loadingService.showLoaderUntilCompleted(updatedRole$).subscribe();
  }

  getroleParams(): RoleParams {
    return this.roleParams;
  }

  setRoleParams(roleParams: RoleParams) {
    this.roleParams = roleParams;
  }

  onGetRoles() {
    return this.getRoles(this.roleParams);
  }
}
