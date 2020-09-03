import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { map, tap, shareReplay, finalize, delay } from 'rxjs/operators';
import { IUser } from '../shared/models/IUser';
import { IUserForLogin } from '../shared/models/IUserForLogin';
import { Router } from '@angular/router';

const AUTH_DATA = 'auth_data';
const AUTH_TOKEN = 'auth_token';

@Injectable({
  providedIn: 'root',
})
export class AuthStore {
  apiUrl: string = environment.apiUrl;
  cleartimeOut:any;

  private subject = new BehaviorSubject<IUser>(null);
  private loadingSubject = new Subject<boolean>();

  user$: Observable<IUser> = this.subject.asObservable();
  loading$: Observable<boolean> = this.loadingSubject.asObservable();

  isLoggedIn$: Observable<boolean>;
  isLoggedOut$: Observable<boolean>;

  constructor(
    private http: HttpClient,
    private router:Router
    
    ) {
    this.isLoggedIn$ = this.user$.pipe(map((user) => !!user));
    this.isLoggedOut$ = this.isLoggedIn$.pipe(map((isLoggedIn) => !isLoggedIn));

    const user = localStorage.getItem(AUTH_DATA);

    if (user) {
      this.subject.next(JSON.parse(user));
      this.autoLogout();
    }
  }

  login(model: IUserForLogin): Observable<IUser> {
    this.loadingSubject.next(true);
    return this.http.post<IUser>(this.apiUrl + 'auth/login', model).pipe(
      tap((user) => {
        this.subject.next(user);
        localStorage.setItem(AUTH_DATA, JSON.stringify(user));
        localStorage.setItem(AUTH_TOKEN, user.token);
        this.autoLogout();
      }),
      finalize(() => this.loadingSubject.next(false)),
      shareReplay()
    );
  }


  autoLogout(): void {
    if(this.cleartimeOut){
      window.clearTimeout(this.cleartimeOut);
    }

    const token = localStorage.getItem(AUTH_TOKEN);
    let payLoad = JSON.parse(
      window?.atob(localStorage.getItem(AUTH_TOKEN)?.split('.')[1])
    );
    const now=Date.now();
    const expireTime = payLoad.exp * 1000 - now;

  this.cleartimeOut=setTimeout(() => {
      this.logOut();
    }, +expireTime);
  }



  logOut(): void {
    this.subject.next(null);
    this.router.navigateByUrl("/");
    localStorage.removeItem(AUTH_DATA);
    localStorage.removeItem(AUTH_TOKEN);
  }

  isMatchRoles(allowedRoles: string[]): boolean {
    let isMatch: boolean = false;
    const token = localStorage.getItem(AUTH_TOKEN);
    if (token) {
      let payLoad = JSON.parse(
        window?.atob(localStorage.getItem(AUTH_TOKEN)?.split('.')[1])
      );
      const userRole = payLoad.role as Array<string>;

      allowedRoles.forEach((element) => {
        if (userRole.includes(element)) {
          isMatch = true;
          return false;
        }
      });
    }

    return isMatch;
  }

  getUserRoles(): string[] {
    const token = localStorage.getItem(AUTH_TOKEN);
    if (token) {
      let payLoad = JSON.parse(
        window?.atob(localStorage.getItem(AUTH_TOKEN)?.split('.')[1])
      );
      return payLoad.role as Array<string>;
    }
  }
}
