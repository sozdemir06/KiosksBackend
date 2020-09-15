import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-options-panel',
  templateUrl: './user-options-panel.component.html',
  styleUrls: ['./user-options-panel.component.scss'],
})
export class UserOptionsPanelComponent implements OnInit {
  allowedRolesForList: string[] = [
    'Sudo',
    'UserOptions.All',
   
  ];
  constructor() {}

  ngOnInit(): void {}
}
