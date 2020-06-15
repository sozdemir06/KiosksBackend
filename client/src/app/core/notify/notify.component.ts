import { Component, OnInit, Inject } from '@angular/core';
import { MAT_SNACK_BAR_DATA } from '@angular/material/snack-bar';
import { INotify } from 'src/app/shared/models/INotify';

@Component({
  selector: 'app-notify',
  templateUrl: './notify.component.html',
  styleUrls: ['./notify.component.scss']
})
export class NotifyComponent implements OnInit {

  constructor(
    @Inject(MAT_SNACK_BAR_DATA) public data:INotify
  ) { }

  ngOnInit(): void {
  }

}
