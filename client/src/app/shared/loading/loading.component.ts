import { Component, OnInit, Input } from '@angular/core';
import { LoadingService } from 'src/app/core/services/loading-service';

@Component({
  selector: 'app-loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.scss']
})
export class LoadingComponent implements OnInit {


  constructor(
    public loadingService:LoadingService
  ) { }

  ngOnInit(): void {
    
  }

}
