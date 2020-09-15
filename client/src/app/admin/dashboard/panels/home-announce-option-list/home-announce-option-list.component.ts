import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home-announce-option-list',
  templateUrl: './home-announce-option-list.component.html',
  styleUrls: ['./home-announce-option-list.component.scss']
})
export class HomeAnnounceOptionListComponent implements OnInit {
allowedRoles:string[]=['Sudo','HomeAnnounceOptions.All'];

  constructor() { }

  ngOnInit(): void {
  }

}
