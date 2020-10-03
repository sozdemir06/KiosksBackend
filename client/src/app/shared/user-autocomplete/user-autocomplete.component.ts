import {
  Component,
  OnInit,
  AfterViewInit,
  OnDestroy,
  Output,
  Input,
  EventEmitter,
} from '@angular/core';
import { UserStore } from 'src/app/core/services/stores/user-store';
import { IUserList } from '../models/IUser';
import { FormControl } from '@angular/forms';
import { map, debounceTime, distinctUntilChanged } from 'rxjs/operators';


@Component({
  selector: 'app-user-autocomplete',
  templateUrl: './user-autocomplete.component.html',
  styleUrls: ['./user-autocomplete.component.scss'],
})
export class UserAutocompleteComponent
  implements OnInit, AfterViewInit, OnDestroy {
  stateControl = new FormControl();
  unsubscribeFromAutoComplete: any;
  @Output() getSelectedUser=new EventEmitter<IUserList>();
  @Input() mode: 'create' | 'update';
  @Input() user: IUserList;
  @Input() placeholder:string="İlan Sahibi";

  constructor(public userStore: UserStore) {}

  ngOnInit(): void {
    if(this.mode=="update"){
      this.stateControl.setValue(this.user?.firstName +" "+this.user?.lastName)
    }
     
  }

  onUserSelectionChange(user: IUserList) {
    this.getSelectedUser.emit(user);
  }

  ngAfterViewInit() {
  this.unsubscribeFromAutoComplete=  this.stateControl.valueChanges
      .pipe(
        map((searchkey) => searchkey),
        debounceTime(400),
        distinctUntilChanged()
      )
      .subscribe((result) => {
        const userParams = this.userStore.getUserParams();
        userParams.search = result;
        this.userStore.setUserParams(userParams);
        this.userStore.onGetUsers();
      });
  }



  ngOnDestroy() {
    this.unsubscribeFromAutoComplete.unsubscribe();
  }
}
