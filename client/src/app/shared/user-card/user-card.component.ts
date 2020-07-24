import { Component, OnInit, Input } from '@angular/core';
import { IUserList } from '../models/IUser';

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.scss']
})
export class UserCardComponent implements OnInit {
@Input() user:IUserList;

  constructor() { }

  ngOnInit(): void {
  }

}
