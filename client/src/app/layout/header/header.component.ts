import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthStore } from 'src/app/auth/auth.store';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  @Output() toggleSidenav=new EventEmitter();
  constructor(
    public authStore:AuthStore,
    private router:Router
  ) { }

  ngOnInit(): void {
  }

  onToogleSidenav(){
    this.toggleSidenav.emit();
  }

  logout(){
    this.authStore.logOut();
    this.router.navigateByUrl("/");
  }

}
