import { Component, OnInit, Input, ViewChild, ElementRef } from '@angular/core';
import { IAnnounceDetail } from 'src/app/shared/models/IAnnounceDetail';

@Component({
  selector: 'app-announce',
  templateUrl: './announce.component.html',
  styleUrls: ['./announce.component.scss'],
})
export class AnnounceComponent implements OnInit {
  @Input() announce: IAnnounceDetail;
  @Input() position: string;
  @Input() height: number;
  @Input() width: number;

  constructor() {}

  ngOnInit(): void {}

  getAnnounceType(contentType: string): string {
    let returnType: string;
    switch (contentType.toLowerCase()) {
      case 'deathannounce':
        returnType = 'Vefat Duyurusu';
        break;
      case 'bloodannounce':
        returnType = 'Acil Kan İhtiyacı';
        break;
      case 'generalannounce':
        returnType = 'Genel Duyuru';
      default:
        returnType = 'Duyuru';
        break;
    }
    return returnType;
  }
}
