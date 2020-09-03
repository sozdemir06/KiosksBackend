import { Component, OnInit } from '@angular/core';
import { KiosksStore } from './store/kiosks-store';
import { ActivatedRoute } from '@angular/router';
import { ISubScreen } from '../shared/models/ISubScreen';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-kiosks',
  templateUrl: './kiosks.component.html',
  styleUrls: ['./kiosks.component.scss']
})
export class KiosksComponent implements OnInit {
  screenId:number;
  top$:Observable<ISubScreen>;
  vmiddle$:Observable<ISubScreen>;
  bottom$:Observable<ISubScreen>;

  left$:Observable<ISubScreen>;
  hmiddle$:Observable<ISubScreen>;
  right$:Observable<ISubScreen>;

  constructor(
    public kioksStore:KiosksStore,
    private route:ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.screenId=+this.route.snapshot.paramMap.get("id");
    this.kioksStore.getListByScreenId(this.screenId);

    setTimeout(()=>{
      this.top$=this.kioksStore.kiosks$.pipe(
        map(data=>data.screen.subScreens.find(x=>x.position.toLowerCase()=='top' && x.status==true))
      );
      this.vmiddle$=this.kioksStore.kiosks$.pipe(
        map(data=>data.screen.subScreens.find(x=>x.position.toLowerCase()=='vmiddle' && x.status==true))
      );
      this.bottom$=this.kioksStore.kiosks$.pipe(
        map(data=>data.screen.subScreens.find(x=>x.position.toLowerCase()=='bottom' && x.status==true))
      );

      this.left$=this.kioksStore.kiosks$.pipe(
        map(data=>data.screen.subScreens.find(x=>x.position.toLowerCase()=='left' && x.status==true))
      );
      this.hmiddle$=this.kioksStore.kiosks$.pipe(
        map(data=>data.screen.subScreens.find(x=>x.position.toLowerCase()=='hmiddle' && x.status==true))
      );
      this.right$=this.kioksStore.kiosks$.pipe(
        map(data=>data.screen.subScreens.find(x=>x.position.toLowerCase()=='right' && x.status==true))
      );


    })
  }

}
