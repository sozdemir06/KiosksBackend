

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IDepartment } from 'src/app/shared/models/IDepartment';
import { map, catchError, tap, delay, shareReplay } from 'rxjs/operators';
import { NotifyService } from '../notify-service';
import { LoadingService } from '../loading-service';
import produce from 'immer';


@Injectable({
    providedIn:"root"
})
export class DepartmentStore {
    apiUrl=environment.apiUrl;

    private subject=new BehaviorSubject<IDepartment[]>([]);
    departments$:Observable<IDepartment[]>=this.subject.asObservable();

    constructor(
        private httpClient: HttpClient,
        private notifyService:NotifyService,
        private loadingService:LoadingService
        
    ) { 
        this.getDepartments();
    }


    private getDepartments(){
       const department$= this.httpClient.get<IDepartment[]>(this.apiUrl+"departments").pipe(
            delay(1000),
            map((departments)=>departments),
            catchError(error=>{
                this.notifyService.notify("error",error);
                return throwError(error);
            }),
            tap((departments)=>{
                departments.sort((a,b)=>a.name.localeCompare(b.name));
                this.subject.next(departments);

            }),
            shareReplay()
        )
         this.loadingService.showLoaderUntilCompleted(department$).subscribe();   
    }

    create(model: IDepartment) {
        const create$ = this.httpClient
          .post<IDepartment>(this.apiUrl + 'departments', model)
          .pipe(
            map((departments) => departments),
            catchError((error) => {
              this.notifyService.notify('error', error);
              return throwError(error);
            }),
            tap((departments) => {
              const newItem = produce(this.subject.getValue(), (draft) => {
                draft.push(departments);
              });
              this.subject.next(newItem);
              this.notifyService.notify('success', 'Yeni birim eklendi...');
            })
          );
    
        this.loadingService.showLoaderUntilCompleted(create$).subscribe();
      }
    
      update(model: IDepartment) {
        const update$ = this.httpClient
          .put<IDepartment>(this.apiUrl + 'departments', model)
          .pipe(
            map((departments) => departments),
            catchError((error) => {
              this.notifyService.notify('error', error);
              return throwError(error);
            }),
            tap((departments) => {
              const updatedItem = produce(this.subject.getValue(), (draft) => {
                const index = draft.findIndex((x) => x.id === departments.id);
    
                const updateItem: IDepartment = {
                  ...draft[index],
                  ...departments,
                };
    
                draft[index] = updateItem;
              });
    
              this.subject.next(updatedItem);
              this.notifyService.notify('success', 'Birim güncellendi...');
            })
          );
    
        this.loadingService.showLoaderUntilCompleted(update$).subscribe();
      }
    
      delete(model: IDepartment) {
        const deleted$ = this.httpClient
          .delete<IDepartment>(this.apiUrl + 'departments/' + model.id)
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