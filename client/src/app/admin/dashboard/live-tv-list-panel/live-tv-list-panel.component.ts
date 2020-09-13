import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-live-tv-list-panel',
  templateUrl: './live-tv-list-panel.component.html',
  styleUrls: ['./live-tv-list-panel.component.scss']
})
export class LiveTvListPanelComponent implements OnInit {
  allowedRolesForList:string[]=['Sudo','LiveTvList.List','LiveTvList.All']
  constructor() { }

  ngOnInit(): void {
  }

}
