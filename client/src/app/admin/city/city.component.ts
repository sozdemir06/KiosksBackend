import { Component, OnInit } from '@angular/core';
import { CityStore } from 'src/app/core/services/stores/city-store';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.scss']
})
export class CityComponent implements OnInit {
toolbarTitle:string="Hava Durumu Şehir Seç"
toolbarSearchInputPlaceholder:string="Şehir Adına Göre ara";
allowedRoles:string[]=["Sudo","AddCityForWheatherForeCast"];

  constructor(
    public cityStore$:CityStore
  ) { }

  ngOnInit(): void {
  }

}
