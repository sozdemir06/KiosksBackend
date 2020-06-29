import { Component, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-user-panel',
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.scss']
})
export class UserPanelComponent implements OnInit {

  allowedRoles:string[]=['Sudo','User.List']
  
  constructor() { }

  ngOnInit(): void {
  }

  
}
