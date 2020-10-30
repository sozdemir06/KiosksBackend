import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-public-logo-panel',
  templateUrl: './public-logo-panel.component.html',
  styleUrls: ['./public-logo-panel.component.scss']
})
export class PublicLogoPanelComponent implements OnInit {
  allowedRolesForList:string[]=['Sudo']
  constructor() { }

  ngOnInit(): void {
  }

}
