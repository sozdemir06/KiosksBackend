import { Component, OnInit } from '@angular/core';
import { HomeAnnounceStore } from 'src/app/core/services/stores/home-announce-store';

@Component({
  selector: 'app-home-announces-panel',
  templateUrl: './home-announces-panel.component.html',
  styleUrls: ['./home-announces-panel.component.scss'],
})
export class HomeAnnouncesPanelComponent implements OnInit {
  allowedRolesForList: string[] = [
    'Sudo',
    'HomeAnnounces.All',
    'HomeAnnounces.Create',
    'HomeAnnounces.Update',
  ];
  constructor(
    public homeAnnounceStore:HomeAnnounceStore
  ) {}

  ngOnInit(): void {}
}
