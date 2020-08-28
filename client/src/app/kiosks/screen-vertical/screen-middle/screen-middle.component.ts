import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-screen-middle',
  templateUrl: './screen-middle.component.html',
  styleUrls: ['./screen-middle.component.scss']
})
export class ScreenMiddleComponent implements OnInit {
@Input() subscreenid:number;

  constructor() { }

  ngOnInit(): void {
  }

}
