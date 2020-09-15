import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home-announces-panel',
  templateUrl: './home-announces-panel.component.html',
  styleUrls: ['./home-announces-panel.component.scss'],
})
export class HomeAnnouncesPanelComponent implements OnInit {
  allowedRolesForList: string[] = [
    'Sudo',
    'HomeAnnounces.List',
    'HomeAnnounces.All',
    'HomeAnnounces.Create',
    'HomeAnnounces.Update',
  ];
  constructor() {}

  ngOnInit(): void {}
}
