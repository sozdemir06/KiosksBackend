import { Component, OnInit, OnDestroy } from '@angular/core';
import {BreakpointObserver, Breakpoints} from '@angular/cdk/layout';
import { AdminHubService } from 'src/app/core/services/admin-hub-signalr-service';
import { KiosksHubService } from 'src/app/kiosks/store/kiosks-hub';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit,OnDestroy{
  opened: boolean=false;
  panelTitle:string;
  mobileMatches:any;
  largeMatches:any;
  sideNavBehavior:string="side";

  constructor(
   private breakPointObserver:BreakpointObserver,
   private adminHubService:AdminHubService,
   private kiosksHub:KiosksHubService
  ) { }

  ngOnInit(): void {
      this.mobileMatches=this.breakPointObserver.observe([
        Breakpoints.Small
      ]).subscribe(result=>{
        if(result.matches){
          this.opened=false;
          this.sideNavBehavior="over";
        }
      })

      this.largeMatches=this.breakPointObserver.observe([
        Breakpoints.Large
      ]).subscribe(result=>{
        if(result.matches){
          this.opened=true
          this.sideNavBehavior="side";
        }
      })
  }

  ngOnDestroy(){
    this.mobileMatches.unsubscribe();
    this.largeMatches.unsubscribe();
  }

}
