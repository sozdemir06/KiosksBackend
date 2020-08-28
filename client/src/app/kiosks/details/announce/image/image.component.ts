import { Component, OnInit, Input } from '@angular/core';
import { IAnnouncePhoto } from 'src/app/shared/models/IAnnouncePhoto';

@Component({
  selector: 'app-image',
  templateUrl: './image.component.html',
  styleUrls: ['./image.component.scss']
})
export class ImageComponent implements OnInit {
@Input() image:IAnnouncePhoto;
  constructor() { }

  ngOnInit(): void {
  }

}
