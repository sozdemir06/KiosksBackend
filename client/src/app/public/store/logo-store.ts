import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoadingService } from 'src/app/core/services/loading-service';
import { NotifyService } from 'src/app/core/services/notify-service';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IPublicLogo } from 'src/app/shared/models/IPublicLogo';
import { catchError, map, tap } from 'rxjs/operators';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class LogoStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<IPublicLogo>(null);
  logo$: Observable<IPublicLogo> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
      this.getLogo();
  }

  private getLogo() {
    const getLogo$ = this.httpClient
      .get<IPublicLogo>(this.apiUrl + 'publiclogo/main')
      .pipe(
        map((logo) => logo),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((logo) => {
          this.subject.next(logo);
        })
      );
    this.loadingService.showLoaderUntilCompleted(getLogo$).subscribe();
  }

 updateLogoRealTime(logo:IPublicLogo){
     const updateSubject=produce(this.subject.getValue(),draft=>{
          draft.fullPath=logo.fullPath;
     });
     this.subject.next(updateSubject);
 }
}
