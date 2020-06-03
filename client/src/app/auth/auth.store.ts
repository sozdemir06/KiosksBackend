import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, tap, shareReplay } from "rxjs/operators";

import { IUser } from '../core/models/IUser';

const AUTH_DATA="auth_data";

@Injectable({
  providedIn: 'root'
})
export class AuthStore {
 apiUrl:string=environment.apiUrl;
 
 private subject=new BehaviorSubject<IUser>(null);

 user$:Observable<IUser>=this.subject.asObservable();
 isLoggedIn$:Observable<boolean>;
 isLoggedOut$:Observable<boolean>;

  constructor(
    private http:HttpClient
  ) { 
    this.isLoggedIn$=this.user$.pipe(map(user=>!!user));
    this.isLoggedOut$=this.isLoggedIn$.pipe(map(isLoggedIn=>!isLoggedIn));

    const user=localStorage.getItem(AUTH_DATA);
    if(user){
      this.subject.next(JSON.parse(user));
    }

  

  }


  login(email:string,password:string):Observable<IUser>{
      return this.http.post<IUser>(this.apiUrl+"auth/login",{email,password})
      .pipe(
          tap(user=>{
            this.subject.next(user);
            localStorage.setItem(AUTH_DATA,JSON.stringify(user));
          }),
          shareReplay()
      )
  }


  logOut():void{
    this.subject.next(null);
    localStorage.removeItem(AUTH_DATA)
  }

  isMatchRoles(allowedRoles:string[]):boolean{
    let isMatch:boolean=false;
    let payLoad=JSON.parse(window.atob(localStorage.getItem(AUTH_DATA).split('.')[1]));
    const userRole=payLoad.role;

    allowedRoles.forEach(element=>{
      if(userRole==element){
        isMatch=true;
        return false;
      }
    });

    return isMatch;

  }
}
