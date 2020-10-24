import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-announce-options-panel',
  templateUrl: './announce-options-panel.component.html',
  styleUrls: ['./announce-options-panel.component.scss']
})
export class AnnounceOptionsPanelComponent implements OnInit {
  allowedRolesForList:string[]=['Sudo','AnnounceOptons.All']
  constructor() { }

  ngOnInit(): void {
  }

}
