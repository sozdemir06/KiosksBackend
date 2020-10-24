import { Component, OnInit } from '@angular/core';
import { NewsStore } from 'src/app/core/services/stores/news-store';

@Component({
  selector: 'app-news-panel',
  templateUrl: './news-panel.component.html',
  styleUrls: ['./news-panel.component.scss']
})
export class NewsPanelComponent implements OnInit {
  allowedRolesForList:string[]=['Sudo','News.All','News.Create','News.Update'];
  constructor(
    public newsStore:NewsStore
  ) { }

  ngOnInit(): void {
  }

}
