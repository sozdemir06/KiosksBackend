import { Component, OnInit } from '@angular/core';
import { AnnounceStore } from 'src/app/core/services/stores/announce-store';

@Component({
  selector: 'app-announce-panel',
  templateUrl: './announce-panel.component.html',
  styleUrls: ['./announce-panel.component.scss']
})
export class AnnouncePanelComponent implements OnInit {
  allowedRolesForList:string[]=['Sudo','Announces.List','Announces.All',"Announces.Create","Announces.Update"]
  constructor(
    public announceStore:AnnounceStore
  ) { }

  ngOnInit(): void {
  }

}
