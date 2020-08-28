import { Component, OnInit } from '@angular/core';
import { KiosksStore } from './store/kiosks-store';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-kiosks',
  templateUrl: './kiosks.component.html',
  styleUrls: ['./kiosks.component.scss']
})
export class KiosksComponent implements OnInit {
  screenId:number;

  constructor(
    public kioksStore:KiosksStore,
    private route:ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.screenId=+this.route.snapshot.paramMap.get("id");
    this.kioksStore.getListByScreenId(this.screenId);
  }

}
