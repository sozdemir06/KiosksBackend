import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';

@Injectable()
export class ErrorMessagesService {

  private subject=new BehaviorSubject<string[]>(null);
  errors$:Observable<string[]>=this.subject.asObservable().pipe(
        filter(messages=>messages && messages.length>0)
  );

  constructor() { }

  showErrors(...errors:string[]){
    this.subject.next(errors);
  }
}
