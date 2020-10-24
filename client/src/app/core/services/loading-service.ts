import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { tap, concatMap, finalize } from 'rxjs/operators';

@Injectable()
export class LoadingService {
    private subject=new BehaviorSubject<boolean>(false);
    loading$:Observable<boolean>=this.subject.asObservable();

  constructor() {
  
  }

  showLoaderUntilCompleted<T>(obs$:Observable<T>):Observable<T>{
    return of(null).pipe(
         tap(()=>this.loadingOn()),
         concatMap(()=>obs$),
         finalize(()=>this.loadingOff())
    )
  }

  loadingOn(){
      this.subject.next(true);
  }

  loadingOff(){
      this.subject.next(false);
  }
}
