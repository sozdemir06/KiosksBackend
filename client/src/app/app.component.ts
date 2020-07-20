import { Component, OnInit } from '@angular/core';
import { AuthStore } from './auth/auth.store';
import { HeatingTypeStore } from './core/services/stores/heating-type-store';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'client';

  constructor(
    private authStore: AuthStore,
    private heatingTypeStore: HeatingTypeStore
  ) {}

  ngOnInit() {
    
     
      
  }
}
