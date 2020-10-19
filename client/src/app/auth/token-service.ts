import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { IUser } from '../shared/models/IUser';
import { AuthStore } from './auth.store';

@Injectable({providedIn: 'root'})
export class TokenService {
    constructor(
        private authStore:AuthStore
    ) { }



    getUser():Observable<IUser>{
        return this.authStore.user$.pipe(take(1));
    }
    
}