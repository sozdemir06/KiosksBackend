import { Component, OnInit } from '@angular/core';
import { UserStore } from './core/services/stores/user-store';
import { AuthStore } from './auth/auth.store';
import { zip, from, interval, Observable, of, combineLatest, timer } from 'rxjs';
import {
  repeat,
  map,
  take,
  delay,
  concatAll,
  concatMap,
  toArray,
  switchMap,
} from 'rxjs/operators';
import { FlatOfHomeStore } from './core/services/stores/flat-of-home-store';
import { IFlatOfHome } from './shared/models/IFlatOfHome';
import { FormArray } from '@angular/forms';
import { HeatingTypeStore } from './core/services/stores/heating-type-store';
import { time } from 'console';

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
