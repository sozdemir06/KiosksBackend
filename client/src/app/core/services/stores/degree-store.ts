

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IDegree } from 'src/app/shared/models/IDegree';
import { catchError, tap, map, shareReplay } from 'rxjs/operators';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';


@Injectable({providedIn: 'root'})
export class DegreeStore {
    apiUrl:string=environment.apiUrl;
    private subject=new BehaviorSubject<IDegree[]>([]);
    degrees$:Observable<IDegree[]>=this.subject.asObservable();

    constructor(
        private httpClient: HttpClient,
        private loadingService:LoadingService,
        private notifyService:NotifyService
        
    ) {
        this.getDegrees();
     }

    private getDegrees(){
        const degrees$=this.httpClient.get<IDegree[]>(this.apiUrl+"degrees").pipe(
            map((degrees)=>degrees),
            
            catchError(error=>{
                this.notifyService.notify("error",error);
                return throwError(error);
            }),
            tap((degrees)=>{
                degrees.sort((a,b)=>a.name.localeCompare(b.name));
                this.subject.next(degrees);

            }),

            shareReplay()
        )

        this.loadingService.showLoaderUntilCompleted(degrees$).subscribe();
    }
    
}