import { Component, OnInit, Input } from '@angular/core';
import { IKiosks } from '../models/IKiosks';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';

@Component({
  selector: 'app-screen-vertical',
  templateUrl: './screen-vertical.component.html',
  styleUrls: ['./screen-vertical.component.scss']
})
export class ScreenVerticalComponent implements OnInit {
@Input() kiosks:IKiosks;
top:ISubScreen;
middle:ISubScreen;
bottom:ISubScreen;


  constructor() { }

  ngOnInit(): void {
    this.top=this.kiosks.screen.subScreens.find(x=>x.position.toLowerCase()=='top' && x.status==true);
    this.middle=this.kiosks.screen.subScreens.find(x=>x.position.toLowerCase()=='middle' && x.status==true);
    this.bottom=this.kiosks.screen.subScreens.find(x=>x.position.toLowerCase()=='bottom' && x.status==true);
  }

  checkScreenPosition(position:string):boolean{
    return this.kiosks.screen.subScreens.some(x=>x.position.toLowerCase()==position.toLowerCase() && x.status==true);
  }

}
