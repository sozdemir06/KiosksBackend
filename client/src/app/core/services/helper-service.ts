import { Injectable } from '@angular/core';
import { NotifyService } from './notify-service';
import { BehaviorSubject, Observable } from 'rxjs';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class HelperService {
  constructor(private notifyService: NotifyService) {}

  dateToLocaleFormat(date: Date): any {
    const offsetMs = date.getTimezoneOffset() * 60 * 1000;
    const msLocal = date.getTime() - offsetMs;
    const dateLocal = new Date(msLocal);
    const iso = dateLocal.toISOString();
    const isoLocal = iso.slice(0, 19);
    return isoLocal;
  }

  checkPublishDate(dt1: Date, dt2: Date): boolean {
    let passCheck: boolean = true;
    dt1 = new Date(dt1);
    dt2 = new Date(dt2);
    if (dt1 > dt2) {
      this.notifyService.notify(
        'error',
        'Başlangıç tarihi bitiş tarihinden büyük olamaz...'
      );
      passCheck = false;
      return;
    }
    if (dt1 == dt2) {
      this.notifyService.notify(
        'error',
        'Başlangıç ve Bitiş tarihleri birbirine eşit olamaz...'
      );
      passCheck = false;
      return;
    }

    const dateNow = new Date();
    if (dateNow > dt2) {
      this.notifyService.notify(
        'error',
        'Yayın için ileri bir tarih seçmelisiniz...'
      );
      passCheck = false;
      return;
    }
    return passCheck;
  }

  checkContentType(contentType: string): string {
    let type: string = '';

    switch (contentType.toLowerCase()) {
      case 'image':
        type = 'Fotoğraf';
        break;
      case 'video':
        type = 'Video';
        break;
      case 'deathannounce':
        type = 'Vefat Duyurusu';
        break;
      case 'bloodannounce':
        type = 'Kan Duyurusu';
        break;
      case 'generalannounce':
        type = 'Genel Duyuru';
        break;
      case 'textandimage':
        type = 'Metin ve Fotoğraf';
        break;
      case 'text':
        type = 'Sadece Metin';
        break;
      default:
        type = 'Yok';
        break;
    }
    return type;
  }

  quillToolbarOptions(): object {
    const modules = {
      toolbar: [
        ['bold', 'italic', 'underline', 'strike'], // toggled buttons
        ['blockquote', 'code-block'],

        [{ header: 1 }, { header: 2 }], // custom button values
        [{ list: 'ordered' }, { list: 'bullet' }],
        [{ script: 'sub' }, { script: 'super' }], // superscript/subscript
        [{ indent: '-1' }, { indent: '+1' }], // outdent/indent
        [{ direction: 'rtl' }], // text direction

        [{ size: ['small', false, 'large', 'huge'] }], // custom dropdown
        [{ header: [1, 2, 3, 4, 5, 6, false] }],

        [{ color: [] }, { background: [] }], // dropdown with defaults from theme
        [{ font: [] }],
        [{ align: [] }],

        ['clean'], // remove formatting button
      ],
    };
    return modules;
  }

  checkExpire(finishDate: Date): boolean {
    let isExpire: boolean = false;
    const _finishDate = new Date(finishDate);
    const dateNow = new Date();

    if (dateNow > _finishDate) {
      isExpire = true;
    }

    return isExpire;
  }
}
