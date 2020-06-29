import { Component, OnInit } from '@angular/core';
import { UserStore } from './core/services/stores/user-store';
import { AuthStore } from './auth/auth.store';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'client';
  constructor(
    private authStore:AuthStore
  ){}


  ngOnInit(){
   
  }
}
