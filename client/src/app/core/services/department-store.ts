

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IDepartment } from 'src/app/shared/models/IDepartment';
import { NotifyService } from './notify-service';
import { map, catchError, tap, delay, shareReplay } from 'rxjs/operators';
import { LoadingService } from './loading-service';

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

    
}