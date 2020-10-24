import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { IToolbarFilterList } from 'src/app/shared/models/toolbar-filter-list';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { UserEditDialogComponent } from './user-edit-dialog/user-edit-dialog.component';
import { UserParams } from 'src/app/shared/models/UserParams';
import { UserStore } from 'src/app/core/services/stores/user-store';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
})
export class UsersComponent implements OnInit {
  toolbarTitle: string = 'Kullanıcı adı,soyadı,birim ve ünvana göre arama...';
  allowdRolesForCreate: string[] = ['Sudo', 'User.Create', 'User.All'];

  constructor(
    public userService: UserStore, 
    private dialog: MatDialog) {}

  ngOnInit(): void {}

  onSearch(eventData: string) {
    const params = this.userService.getUserParams();
    params.search = eventData;
    params.pageIndex = 1;
    this.userService.setUserParams(params);
    this.userService.onGetUsers();
  }

  onPageChange(event: PageEvent) {
    const params = this.userService.getUserParams();
    params.pageIndex = event.pageIndex + 1;
    params.pageSize = event.pageSize;
    this.userService.setUserParams(params);
    this.userService.onGetUsers();
  }

  onWaitingConfirm() {
    const params = this.userService.getUserParams();
    params.statusPassive = 'passive';
    params.pageIndex = 1;
    this.userService.setUserParams(params);
    this.userService.onGetUsers();
  }

  onCreateNew() {
    const dialogRef = this.dialog.open(UserEditDialogComponent, {
      width: '45rem',
      maxHeight: '100vh',
      data: {
        title: 'Yeni Kullanıcı Ekle',
        mode: 'create',
      },
    });
  }

  onReset() {
    const params = new UserParams();
    this.userService.setUserParams(params);
    this.userService.onGetUsers();
  }

  filterBy(event: any) {
    console.log(event);
  }
}
