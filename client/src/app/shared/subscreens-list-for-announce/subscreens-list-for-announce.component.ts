import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ISubScreen } from '../models/ISubScreen';
import { Observable } from 'rxjs';
import { SubScreenStore } from 'src/app/core/services/stores/subscreen-store';

@Component({
  selector: 'app-subscreens-list-for-announce',
  templateUrl: './subscreens-list-for-announce.component.html',
  styleUrls: ['./subscreens-list-for-announce.component.scss']
})
export class SubscreensListForAnnounceComponent implements OnInit {
  subscreens$:Observable<ISubScreen[]>;
  displayedColumns:string[]=["Name","Status","Actions"];
  @Output() onSelectSubScreen=new EventEmitter<ISubScreen>();
  constructor(
    private subScreenStore:SubScreenStore
  ) { }

  ngOnInit(): void {
    this.subscreens$=this.subScreenStore.getScreenListForFilters();
  }

  onSelectScreen(subscreen:ISubScreen){
    this.onSelectSubScreen.emit(subscreen);
  }

}
