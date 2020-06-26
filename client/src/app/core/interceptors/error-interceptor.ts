import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpEvent,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router} from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error) => {
        if (error instanceof HttpErrorResponse) {
          if (error.status === 401) {
            return throwError(error.statusText);
          }
          if (error.status === 404) {
            this.router.navigateByUrl('/not-found');
          }
          if (error.status === 403) {
            this.router.navigateByUrl('/not-found');
          }
          if (error.status === 500) {
            this.router.navigateByUrl('/not-found');
          }

          const serverError = error.error.errors || error.error;
          let modalStateErrors = '';
          if (serverError && typeof serverError === 'object') {
            for (const key in serverError) {
              if (serverError[key]) {
                modalStateErrors += serverError[key] + '\n';
              }
            }
          }
          return throwError(modalStateErrors || serverError || 'Server Error');
        }
      })
    );
  }
}

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true,
};
