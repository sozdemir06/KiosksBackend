import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NotifyService } from './notify-service';

@Injectable({ providedIn: 'root' })
export class HelperService {
  constructor(
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
      dt1=new Date(dt1);
      dt2=new Date(dt2);
      if(dt1>dt2){
          this.notifyService.notify("error","Başlangıç tarihi bitiş tarihinden büyük olamaz...");
          passCheck=false;
          return;
      }
      if(dt1==dt2){
        this.notifyService.notify("error","Başlangıç ve Bitiş tarihleri birbirine eşit olamaz...");
        passCheck=false;
        return;
      }
      
      const dateNow=new Date();
      if(dateNow>dt2){
        this.notifyService.notify("error","Yayın için ileri bir tarih seçmelisiniz...");
        passCheck=false;
        return;
      }
      return passCheck;
  }
}
