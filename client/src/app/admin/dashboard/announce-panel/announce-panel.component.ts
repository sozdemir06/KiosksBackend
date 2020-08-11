import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-announce-panel',
  templateUrl: './announce-panel.component.html',
  styleUrls: ['./announce-panel.component.scss']
})
export class AnnouncePanelComponent implements OnInit {
  allowedRolesForList:string[]=['Sudo','Announces.List','Announces.All']
  constructor() { }

  ngOnInit(): void {
  }

}
