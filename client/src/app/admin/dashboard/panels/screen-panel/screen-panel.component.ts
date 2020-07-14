import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-screen-panel',
  templateUrl: './screen-panel.component.html',
  styleUrls: ['./screen-panel.component.scss']
})
export class ScreenPanelComponent implements OnInit {
  allowedRolesForList:string[]=['Sudo','Screens.List'];
  
  constructor() { }

  ngOnInit(): void {
  }

}
