import { Component, OnInit, Input } from '@angular/core';
import { IBuildingAge } from 'src/app/shared/models/IBuildingAge';

@Component({
  selector: 'app-building-age-list',
  templateUrl: './building-age-list.component.html',
  styleUrls: ['./building-age-list.component.scss']
})
export class BuildingAgeListComponent implements OnInit {

  displayedColumns: string[] = ["Id",'Name',"Actions"];
  @Input() dataSource:IBuildingAge[];

  constructor() { }

  ngOnInit(): void {
  }

  onUpdate(element:IBuildingAge){

  }

  onDelete(element:IBuildingAge){

  }

}
