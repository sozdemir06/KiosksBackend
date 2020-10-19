import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpEvent,
  HttpHandler,
  HttpRequest,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/shared/models/IUser';
import { AuthStore } from 'src/app/auth/auth.store';
import { Router } from '@angular/router';

const AUTH_DATA = 'auth_data';
@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(
    private router:Router
  ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    let currentUser: IUser = JSON.parse(localStorage.getItem(AUTH_DATA));
    if (currentUser) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token}`,
        },
      });
    }
    return next.handle(req);
  }
}

export const AuthInterceptors = {
  provide: HTTP_INTERCEPTORS,
  useClass: AuthInterceptor,
  multi: true,
};
