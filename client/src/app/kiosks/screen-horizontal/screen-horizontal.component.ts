import { Component, OnInit, Input } from '@angular/core';
import { IKiosks } from '../models/IKiosks';

@Component({
  selector: 'app-screen-horizontal',
  templateUrl: './screen-horizontal.component.html',
  styleUrls: ['./screen-horizontal.component.scss']
})
export class ScreenHorizontalComponent implements OnInit {
  @Input() kiosks:IKiosks;
  constructor() { }

  ngOnInit(): void {
  }

}
