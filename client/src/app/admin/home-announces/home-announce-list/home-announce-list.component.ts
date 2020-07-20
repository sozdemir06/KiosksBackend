import { Component, OnInit, Input } from '@angular/core';
import { IHomeAnnounce } from 'src/app/shared/models/IHomeAnnounce';

@Component({
  selector: 'app-home-announce-list',
  templateUrl: './home-announce-list.component.html',
  styleUrls: ['./home-announce-list.component.scss']
})
export class HomeAnnounceListComponent implements OnInit {
displayedColumns:string[]=['Image','Header','Created','PublishDates','Price','PublishStatus','Actions'];
@Input() dataSource:IHomeAnnounce[];

  constructor() { }

  ngOnInit(): void {
  }

}
