import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-role-panel',
  templateUrl: './role-panel.component.html',
  styleUrls: ['./role-panel.component.scss']
})
export class RolePanelComponent implements OnInit {
allowedRoles:string[]=["Sudo"];


  constructor() { }

  ngOnInit(): void {
  }

}
