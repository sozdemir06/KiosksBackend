import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IPublicLogo } from 'src/app/shared/models/IPublicLogo';
import { catchError, map, tap } from 'rxjs/operators';
import { INotify } from 'src/app/shared/models/INotify';
import { NotifyService } from '../notify-service';
import { LoadingService } from '../loading-service';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class PublicLogoService {
  apiURl: string = environment.apiUrl;

  private logoSubject = new BehaviorSubject<IPublicLogo[]>([]);
  logos$: Observable<IPublicLogo[]> = this.logoSubject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private notifyService: NotifyService,
    private loadingService: LoadingService
  ) {
    this.getLogoList();
  }

  private getLogoList() {
    const list$ = this.httpClient
      .get<IPublicLogo[]>(this.apiURl + 'publiclogo')
      .pipe(
        map((logos) => logos),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((logos) => {
          this.logoSubject.next(logos);
        })
      );
    this.loadingService.showLoaderUntilCompleted(list$).subscribe();
  }

  create(logo:IPublicLogo){
      const create$=produce(this.logoSubject.getValue(),draft=>{
          draft.push(logo);
      });
      this.logoSubject.next(create$);
  }

  makeMainLogo(logo: IPublicLogo) {
    const update$ = this.httpClient
      .put<IPublicLogo>(this.apiURl + 'publiclogo/setmain', logo)
      .pipe(
        map((logo) => logo),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((logo) => {
          const updateLogoSubject = produce(
            this.logoSubject.getValue(),
            (draft) => {
              const index = draft.findIndex((x) => x.id === logo.id);
              if (index != -1) {
                const isMain = draft.find((x) => x.isMain);
                if (isMain) {
                  isMain.isMain = false;
                }
                draft[index] = logo;
              }
            }
          );
          this.logoSubject.next(updateLogoSubject);
          this.notifyService.notify('success', 'Logo güncellendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  update(logo: IPublicLogo) {
    const update$ = this.httpClient
      .put<IPublicLogo>(this.apiURl + 'publiclogo', logo)
      .pipe(
        map((logo) => logo),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((logo) => {
          const updateLogoSubject = produce(
            this.logoSubject.getValue(),
            (draft) => {
              const index = draft.findIndex((x) => x.id === logo.id);
              if (index != -1) {
                draft[index] = logo;
              }
            }
          );
          this.logoSubject.next(updateLogoSubject);
          this.notifyService.notify('success', 'Logo güncellendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  delete(logo: IPublicLogo) {
    const delete$ = this.httpClient
      .delete<IPublicLogo>(this.apiURl + 'publiclogo/' + logo.id)
      .pipe(
        map((logo) => logo),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((logo) => {
          const updateLogoSubject = produce(
            this.logoSubject.getValue(),
            (draft) => {
              const index = draft.findIndex((x) => x.id === logo.id);
              if (index != -1) {
                draft.splice(index, 1);
              }
            }
          );
          this.logoSubject.next(updateLogoSubject);
          this.notifyService.notify('success', 'Logo Silindi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(delete$).subscribe();
  }
}
