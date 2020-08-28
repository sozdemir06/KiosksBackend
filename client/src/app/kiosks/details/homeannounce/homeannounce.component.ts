import { Component, OnInit, Input } from '@angular/core';
import { IHomeAnnounceForKiosks } from '../../models/IHomeAnnounceForKiosks';

@Component({
  selector: 'app-homeannounce',
  templateUrl: './homeannounce.component.html',
  styleUrls: ['./homeannounce.component.scss']
})
export class HomeannounceComponent implements OnInit {
@Input() homeannounce:IHomeAnnounceForKiosks;

  constructor() { }

  ngOnInit(): void {
  }

}
