import { Component, OnInit, Output } from '@angular/core';
import { UserStore } from 'src/app/core/services/stores/user-store';

@Component({
  selector: 'app-user-panel',
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.scss'],
})
export class UserPanelComponent implements OnInit {
  allowedRoles: string[] = [
    'Sudo',
    'User.All',
    'User.Create',
    'User.Update',
    'User.Delete',
    'User.Publish',
  ];

  constructor(public userStore: UserStore) {}

  ngOnInit(): void {}
}
