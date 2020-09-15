import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-announce-option-list',
  templateUrl: './vehicle-announce-option-list.component.html',
  styleUrls: ['./vehicle-announce-option-list.component.scss'],
})
export class VehicleAnnounceOptionListComponent implements OnInit {
  alloweRoles: string[] = [
    'Sudo',
    'VehicleAnnounceOptions.All',
  ];


  constructor() {}

  ngOnInit(): void {}
}
