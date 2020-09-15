import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-live-tv-panel',
  templateUrl: './live-tv-panel.component.html',
  styleUrls: ['./live-tv-panel.component.scss']
})
export class LiveTvPanelComponent implements OnInit {
  allowedRolesForList:string[]=['Sudo','LiveTvBroadCasts.List','LiveTvBroadCasts.All']
  constructor() { }

  ngOnInit(): void {
  }

}
