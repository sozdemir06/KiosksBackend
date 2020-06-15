

import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthStore } from 'src/app/auth/auth.store';
import { first, flatMap } from 'rxjs/operators';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor(
        private authStore:AuthStore
    ){}
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return this.authStore.user$.pipe(
            first(),
            flatMap(user=>{
                if(!user){
                    return next.handle(req);
                }

                const authReq = !!user.token ? req.clone({
                    setHeaders: { Authorization: 'Bearer ' + user.token },
                  }) : req;
                 
                  return next.handle(authReq);
            })
        )
    }
}

export const AuthInterceptors={
    provide:HTTP_INTERCEPTORS,
    useClass:AuthInterceptor,
    multi:true
}