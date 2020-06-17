import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ErrorMessagesService } from 'src/app/core/services/error-messages.service';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-error-messages',
  templateUrl: './error-messages.component.html',
  styleUrls: ['./error-messages.component.scss']
})
export class ErrorMessagesComponent implements OnInit {
  
  showMessage:boolean=false;
  errors$:Observable<string[]>;

  constructor(
    private errorMessagesService:ErrorMessagesService
  ) { }

  ngOnInit(): void {
    this.errors$=this.errorMessagesService.errors$.pipe(
      tap(()=>this.showMessage=true)
    )
  }


  onClose(){
    this.showMessage=false;
  }

}
