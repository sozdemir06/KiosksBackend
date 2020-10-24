import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IWheatherForeCast } from 'src/app/shared/models/IWheatherForeCast';
import { map, catchError, tap } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class WheatherForeCastStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<IWheatherForeCast[]>([]);
  forecasts$: Observable<IWheatherForeCast[]> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
      this.getList(); 
  }

  private getList() {
    const list$ = this.httpClient
      .get<IWheatherForeCast[]>(this.apiUrl + 'WheatherForeCasts')
      .pipe(
        map((forecasts) => forecasts),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((forecasts) => {
          forecasts.forEach(el=>{
            const cityname=el.cityName.split(" ");
            el.cityName=cityname[0];
          })
          this.subject.next(forecasts);
        })
      );
    this.loadingService.showLoaderUntilCompleted(list$).subscribe();
  }

  updateRealTime(model:IWheatherForeCast[]){
    model.forEach(el=>{
      const cityname=el.cityName.split(" ");
      el.cityName=cityname[0];
    })
    this.subject.next(model);
    this.notifyService.notify("success","Hava Durumu Güncellendi..");
  }

  getListByInterval(){
    this.getList();
  }
}
