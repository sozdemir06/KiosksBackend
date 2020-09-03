import { Component, OnInit, Input } from '@angular/core';
import { INewsForKiosks } from '../../models/INewsForKiosks';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.scss']
})
export class NewsComponent implements OnInit {
@Input() news:INewsForKiosks;

  constructor() { }

  ngOnInit(): void {
  }

}
