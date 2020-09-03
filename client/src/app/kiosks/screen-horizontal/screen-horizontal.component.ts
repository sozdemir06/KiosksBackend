import { Component, OnInit, Input } from '@angular/core';
import { IKiosks } from '../models/IKiosks';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';

@Component({
  selector: 'app-screen-horizontal',
  templateUrl: './screen-horizontal.component.html',
  styleUrls: ['./screen-horizontal.component.scss']
})
export class ScreenHorizontalComponent implements OnInit {
  @Input() kiosks:IKiosks;
  left:ISubScreen;
  middle:ISubScreen;
  right:ISubScreen;
  constructor() { }

  ngOnInit(): void {
    this.left=this.kiosks.screen.subScreens.find(x=>x.position.toLowerCase()=='left' && x.status==true);
    this.middle=this.kiosks.screen.subScreens.find(x=>x.position.toLowerCase()=='hmiddle' && x.status==true);
    this.right=this.kiosks.screen.subScreens.find(x=>x.position.toLowerCase()=='right' && x.status==true);
  }

}
