import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-role-panel',
  templateUrl: './role-panel.component.html',
  styleUrls: ['./role-panel.component.scss']
})
export class RolePanelComponent implements OnInit {
allowedRoles:string[]=["Sudo","Roles.List","Roles.All","Roles.Update","Roles.Create"];


  constructor() { }

  ngOnInit(): void {
  }

}
