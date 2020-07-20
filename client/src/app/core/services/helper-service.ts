import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NotifyService } from './notify-service';

@Injectable({ providedIn: 'root' })
export class HelperService {
  constructor(
      private httpClient: HttpClient,
      private notifyService:NotifyService
      ) {}



  dateToLocaleFormat(date: Date): any {
    const offsetMs = date.getTimezoneOffset() * 60 * 1000;
    const msLocal = date.getTime() - offsetMs;
    const dateLocal = new Date(msLocal);
    const iso = dateLocal.toISOString();
    const isoLocal = iso.slice(0, 19);
    return isoLocal;
  }

  checkPublishDate(dt1:Date,dt2:Date):boolean{
      let passCheck:boolean=true;
      if(dt1>dt2){
          this.notifyService.notify("error","Başlangıç tarihi bitiş tarihinden büyük olamaz...")
          passCheck=false;
      }
      if(dt1==dt2){
        this.notifyService.notify("error","Başlangıç ve Bitiş tarihleri birbirine eşit olamaz...");
        passCheck=false;
      }
      
      const dateNow=this.dateToLocaleFormat(new Date());
      if(dateNow>dt2){
        this.notifyService.notify("error","Yayın için ileri bir tarih seçmelisiniz...");
        passCheck=false;
      }
      return passCheck;
  }
}
