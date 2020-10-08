import { Component, OnInit } from '@angular/core';
import { AdminHubService } from 'src/app/core/services/admin-hub-signalr-service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  constructor(
    public adminHubService:AdminHubService
  ) { }

  ngOnInit(): void {
  }

}
