import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {

  opened: boolean=false;
  panelTitle:string;
  mobileMatches:any;
  largeMatches:any;
  sideNavBehavior:string="side";

  constructor(
   private breakPointObserver:BreakpointObserver
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
