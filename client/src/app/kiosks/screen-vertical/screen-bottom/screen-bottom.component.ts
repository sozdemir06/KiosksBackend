import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-screen-bottom',
  templateUrl: './screen-bottom.component.html',
  styleUrls: ['./screen-bottom.component.scss']
})
export class ScreenBottomComponent implements OnInit {
@Input() subscreenid:number;

  constructor() { }

  ngOnInit(): void {
  }

}
