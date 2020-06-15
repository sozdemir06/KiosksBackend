import {
  Component,
  OnInit,
  OnDestroy,
  AfterViewInit,
  ViewChild,
  ElementRef,
} from '@angular/core';
import { IUserList } from 'src/app/shared/models/IUser';
import { UserParams } from 'src/app/shared/models/UserParams';
import { UserService } from 'src/app/core/services/user-service';
import { IPagination } from 'src/app/shared/models/IPagination';
import { fromEvent } from 'rxjs';
import { map, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
})
export class UsersComponent implements OnInit, AfterViewInit, OnDestroy {
  unSubscribeFromSearchEvent: any;

  filters: any[] = [
    { id: 1, name: 'Aktif Kullanıcılar', value: true },
    { id: 2, name: 'Pasif Kullanıcılar', value: false },
  ];
  users: IUserList[];
  userParams = new UserParams();
  totalCount: number;

  @ViewChild('searchInput', { static: true }) Input: ElementRef;

  constructor(public userService: UserService) {}

  ngOnInit(): void {
    this.loadAllUsers();
  }

  loadAllUsers() {}

  ngAfterViewInit() {
    this.unSubscribeFromSearchEvent = fromEvent<any>(
      this.Input.nativeElement,
      'keyup'
    )
      .pipe(
        map((event) => event.target.value),
        debounceTime(400),
        distinctUntilChanged()
      )
      .subscribe((data) => {
        const params = this.userService.getUserParams();
        params.search = data;
        params.pageIndex = 1;
        this.userService.onGetUsers();
      });
  }

  onPageChange(event: PageEvent) {
    const params = this.userService.getUserParams();
    params.pageIndex = event.pageIndex + 1;
    params.pageSize=event.pageSize;
    this.userService.setUserParams(params);
    this.userService.onGetUsers();
  }

  onWaitingConfirm() {}

  onCreateNew() {}

  filterBy(event: boolean) {}

  ngOnDestroy() {
    this.unSubscribeFromSearchEvent.unsubscribe();
  }
}
