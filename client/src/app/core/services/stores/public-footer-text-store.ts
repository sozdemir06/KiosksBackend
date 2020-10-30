import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IPublicFooterText } from 'src/app/shared/models/IPublicFooterText';
import { catchError, map, tap } from 'rxjs/operators';
import { error } from 'protractor';
import { ta } from 'date-fns/locale';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class PublicFooterTextStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<IPublicFooterText>(null);
  footerText$: Observable<IPublicFooterText> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getFooterText();
  }

  private getFooterText() {
    const getFooterText$ = this.httpClient
      .get<IPublicFooterText>(this.apiUrl + 'publicfooterText')
      .pipe(
        map((footertext) => footertext),
        catchError((error) => {
          return throwError(error);
        }),
        tap((footerText) => {
          this.subject.next(footerText);
        })
      );
    this.loadingService.showLoaderUntilCompleted(getFooterText$).subscribe();
  }

  cretaeOrUpdate(footerText: IPublicFooterText) {
    const createOrUpdate$ = this.httpClient
      .post<IPublicFooterText>(this.apiUrl + 'publicfootertext', footerText)
      .pipe(
        map((text) => text),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((text) => {
          const udpate = produce(this.subject.getValue(), (draft) => {
            draft.footerText = text?.footerText;
            draft.contentPhoneNumber = text?.contentPhoneNumber;
          });
          this.subject.next(udpate);
        })
      );
    this.loadingService.showLoaderUntilCompleted(createOrUpdate$).subscribe();
  }

  updateTextRealTime(footerText: IPublicFooterText) {
    const udpate = produce(this.subject.getValue(), (draft) => {
      draft.footerText = footerText?.footerText;
      draft.contentPhoneNumber = footerText?.contentPhoneNumber;
    });
    this.subject.next(udpate);
  }
}
