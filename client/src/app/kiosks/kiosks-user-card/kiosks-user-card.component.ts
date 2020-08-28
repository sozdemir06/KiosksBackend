import { Component, OnInit, Input } from '@angular/core';
import { IUserList } from 'src/app/shared/models/IUser';

@Component({
  selector: 'app-kiosks-user-card',
  templateUrl: './kiosks-user-card.component.html',
  styleUrls: ['./kiosks-user-card.component.scss']
})
export class KiosksUserCardComponent implements OnInit {
  @Input() user:IUserList;
  constructor() { }

  ngOnInit(): void {
  }

}
