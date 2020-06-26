import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IRole } from 'src/app/shared/models/IRole';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class UserRoleStore {
  apiUrl: string = environment.apiUrl;

  private subject = new BehaviorSubject<IRole[]>([]);
  userRoles$: Observable<IRole[]> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {}

  getUserRoles(userId?: number) {
    const userroles$ = this.httpClient
      .get<IRole[]>(this.apiUrl + 'users/roles/' + userId)
      .pipe(
        map((userRoles) => userRoles),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((userRoles) => {
          this.subject.next(userRoles);
        })
      ).subscribe()
    
  }

  create(userId: number, roleId: number) {
    const createUserRole$ = this.httpClient
      .post<IRole>(this.apiUrl + 'users/' + userId + '/addrole/' + roleId, {})
      .pipe(
        map((role) => role),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((role) => {
          const newuserrole = produce(this.subject.getValue(), (draft) => {
            draft.push(role);
          });
          this.subject.next(newuserrole);
          this.notifyService.notify('success', 'Role eklendi');
        })
      );

      this.loadingService.showLoaderUntilCompleted(createUserRole$).subscribe();
  }

  delete(userId:number,roleId:number){
    const deleteRoleFromUser$=this.httpClient.delete<IRole>(this.apiUrl+"users/"+userId+"/delete/"+roleId)
                .pipe(
                  map(role=>role),
                  catchError((error) => {
                    this.notifyService.notify('error', error);
                    return throwError(error);
                  }),
                  tap(role=>{
                    const deletedRole=produce(this.subject.getValue(),draft=>{
                       const index=draft.findIndex(x=>x.id==roleId);
                       if(index!==-1)
                       {
                         draft.splice(index,1);
                       }
                    });
                    this.subject.next(deletedRole);
                    this.notifyService.notify("success","Role Silindi...");
                  })
                );

              this.loadingService.showLoaderUntilCompleted(deleteRoleFromUser$).subscribe();

                
  }
}
