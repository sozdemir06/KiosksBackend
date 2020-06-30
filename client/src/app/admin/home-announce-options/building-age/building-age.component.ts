import { Component, OnInit } from '@angular/core';
import { BuildingAgeStore } from 'src/app/core/services/stores/building-age-store';

@Component({
  selector: 'app-building-age',
  templateUrl: './building-age.component.html',
  styleUrls: ['./building-age.component.scss']
})
export class BuildingAgeComponent implements OnInit {
  toolbarTitle:string="Binanın Yaşı";
  toolbarSearchInputPlaceholder:string="Arama kapalı";
  allowedRoles:string[]=["Sudo","BuildingAge.Create"];

  constructor(
    public buildingAgeStore:BuildingAgeStore
  ) { }

  ngOnInit(): void {
  }

  onCreate(){

  }

}
