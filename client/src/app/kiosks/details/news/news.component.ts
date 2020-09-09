import { Component, OnInit, Input } from '@angular/core';
import { INewsForKiosks } from '../../models/INewsForKiosks';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.scss']
})
export class NewsComponent implements OnInit {
@Input() news:INewsForKiosks;
@Input() position:string;
@Input() height:number;
@Input() width:number;

  constructor() { }

  ngOnInit(): void {
  }

}
