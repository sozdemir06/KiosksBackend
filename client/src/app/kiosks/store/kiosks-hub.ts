import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder} from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';
import { ExchangeRateStore } from 'src/app/core/services/stores/exchangerate-store';
import { WheatherForeCastStore } from 'src/app/core/services/stores/wheatherforecast-store';
import { IExchangeRate } from 'src/app/shared/models/IExchangeRate';
import { IScreenFooter } from 'src/app/shared/models/IScreenFooter';
import { IScreenHeader } from 'src/app/shared/models/IScreenHeader';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { IWheatherForeCast } from 'src/app/shared/models/IWheatherForeCast';
import { environment } from 'src/environments/environment';
import { IScreenForKiosks } from '../models/IScreenForKiosks';
import { KiosksStore } from './kiosks-store';

@Injectable({ providedIn: 'root' })
export class KiosksHubService {
  hubUrl: string = environment.hubUrl;
  hubConnection: HubConnection;
  private statusSubject = new BehaviorSubject<string>(null);
  status$: Observable<string> = this.statusSubject.asObservable();
  private onlineScreenSubject = new BehaviorSubject<number[]>([]);
  onlineScreens$: Observable<
    number[]
  > = this.onlineScreenSubject.asObservable();

  constructor(
    private kiosksStore: KiosksStore,
    private exChangeRateStore: ExchangeRateStore,
    private wheatherForeCastStore: WheatherForeCastStore,
  ) {

  }

  createHubConnection() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'KiosksHub')
      .withAutomaticReconnect()
      .build();

    //start conneciton
    this.hubConnection
      .start()
      .then(() => {
        this.statusSubject.next(this.hubConnection.state);
      })
      .catch((error) => {
        this.statusSubject.next(this.hubConnection.state);
      });

    //Catch Reconneciton State
    this.hubConnection.onreconnected(() => {
      this.statusSubject.next(this.hubConnection.state);
    });
    //Catch Reconneciton State End
    this.hubConnection.onreconnecting(() => {
      this.statusSubject.next(this.hubConnection.state);
    });

    this.hubConnection.on('OnlineScreens', (onlineScreens: number[]) => {
      this.onlineScreenSubject.next(onlineScreens);
    });
  }

  kiosksListener() {
    //ExchangeRate START
    this.hubConnection.on(
      'ReceiveExchangeRate',
      (exChangeRate: IExchangeRate[]) => {
        this.exChangeRateStore.updateRealTime(exChangeRate);
      }
    );
    //ExchangeRate END
    //WhetherForeCastStart Start
    this.hubConnection.on(
      'ReceiveWheatherForeCast',
      (model: IWheatherForeCast[]) => {
        this.wheatherForeCastStore.updateRealTime(model);
      }
    );
    //WheatherForeCast END

    //Kiosks Screen Header START
    this.hubConnection.on('ReceiveScreenHeader', (header: IScreenHeader) => {
      this.kiosksStore.updateScreenHeaderRealTime(header);
    });
    //Kiosks Screen Header END
    //Kiosks Screen Footer START
    this.hubConnection.on('ReceiveScreenFooter', (footer: IScreenFooter) => {
      this.kiosksStore.updateScreenFooterRealTime(footer);
    });
    //Kiosks Screen Footer END
    //Kiosks Screen Header Photo START
    this.hubConnection.on("ReceiveScreenHeaderPhoto",(photo)=>{
       this.kiosksStore.updateScreenPhotoRealTime(photo);
    })
    //Kiosks Screen Header Photo END

    //Update Screen START
    this.hubConnection.on("ReceiveScreen",(screen:IScreenForKiosks)=>{
       this.kiosksStore.updateScreenRealTime(screen);
    })
    //Update Screen END

     //Update Screen START
     this.hubConnection.on("ReceiveSubScreen",(screen:ISubScreen)=>{
      this.kiosksStore.updateSubScreenRealTime(screen);
   })
   //Update Screen END

  }

  onConnected(screendId: number, connectionId: string) {
    this.hubConnection.invoke('ConnectScreen', screendId, connectionId);
  }

  stopHubConnection() {
    this.hubConnection.stop().catch((error) => {
      this.statusSubject.next(this.hubConnection.state);
    });
  }
}
