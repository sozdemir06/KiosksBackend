import { Component, OnInit, Input } from '@angular/core';
import { IUserList } from 'src/app/shared/models/IUser';
import { IPagination } from 'src/app/shared/models/IPagination';
export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}


@Component({
  selector: 'app-users-list-card',
  templateUrl: './users-list-card.component.html',
  styleUrls: ['./users-list-card.component.scss']
})
export class UsersListCardComponent implements OnInit {
  displayedColumns: string[] = ['Avatar', 'Name', 'Phone','Campus','Status',"Actions"];
  @Input() dataSource:IUserList[];

  constructor() { }

  ngOnInit(): void {
  }

}
