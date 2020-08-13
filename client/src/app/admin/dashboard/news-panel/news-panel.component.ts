import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-news-panel',
  templateUrl: './news-panel.component.html',
  styleUrls: ['./news-panel.component.scss']
})
export class NewsPanelComponent implements OnInit {
  allowedRolesForList:string[]=['Sudo','News.List','News.All']
  constructor() { }

  ngOnInit(): void {
  }

}
