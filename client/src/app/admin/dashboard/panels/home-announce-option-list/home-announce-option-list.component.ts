import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home-announce-option-list',
  templateUrl: './home-announce-option-list.component.html',
  styleUrls: ['./home-announce-option-list.component.scss']
})
export class HomeAnnounceOptionListComponent implements OnInit {
allowedRolesForNumberOfroomList:string[]=['Sudo','NumberOfRoom.List'];
allowedRolesForbuildingAgeList:string[]=['Sudo','BuildingsAge.List'];
allowedRolesForFlatsOfHomeList:string[]=['Sudo','FlatsOfHome.List'];
allowedRolesForHeatingTypesList:string[]=['Sudo','HeatingTypes.List'];
  constructor() { }

  ngOnInit(): void {
  }

}
