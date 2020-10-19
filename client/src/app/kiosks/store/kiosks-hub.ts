import { Identifiers } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';
import { IAnnounce } from 'src/app/shared/models/IAnnounce';
import { IAnnouncePhoto } from 'src/app/shared/models/IAnnouncePhoto';
import { IAnnounceSubScreen } from 'src/app/shared/models/IAnnounceSubScreen';
import { IFoodMenu } from 'src/app/shared/models/IFoodMenu';
import { IFoodMenuPhoto } from 'src/app/shared/models/IFoodMenuPhoto';
import { IFoodMenuSubScreen } from 'src/app/shared/models/IFoodMenuSubScreen';
import { IHomeAnnounce } from 'src/app/shared/models/IHomeAnnounce';
import { IHomeAnnouncePhoto } from 'src/app/shared/models/IHomeAnnouncePhoto';
import { IHomeAnnounceSubScreen } from 'src/app/shared/models/IHomeAnnounceSubScreen';
import { INews } from 'src/app/shared/models/INews';
import { INewsPhoto } from 'src/app/shared/models/INewsPhoto';
import { INewsSubScreen } from 'src/app/shared/models/INewsSubScreen';
import { IVehicleAnnounceList } from 'src/app/shared/models/IVehicleAnnounceList';
import { IVehicleAnnouncePhoto } from 'src/app/shared/models/IVehicleAnnouncePhoto';
import { IVehicleAnnounceSubScreen } from 'src/app/shared/models/IVehicleAnnounceSubScreen';
import { environment } from 'src/environments/environment';
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

  constructor(private kiosksStore: KiosksStore) {}

  createHubConnection(screenId: number = 0) {
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
    //Announce Events Start
    this.hubConnection.on('ReceiveAnnounce', (announce: IAnnounce) => {
      this.kiosksStore.updateOrCreateAnnounceRealTime(announce);
    });
    this.hubConnection.on(
      'ReceiveAnnouncePhoto',
      (photo: IAnnouncePhoto, eventType: string) => {
        if (eventType.toLowerCase() == 'update') {
          this.kiosksStore.updateOrAddNewAnnouncePhotoRealTime(photo);
        } else if (eventType.toLowerCase() == 'delete') {
          this.kiosksStore.deleteannouncePhotoRealTime(photo);
        }
      }
    );
    this.hubConnection.on(
      'ReceiveAnnounceSubScreen',
      (subscreen: IAnnounceSubScreen, eventType: string) => {
        if (eventType.toLowerCase() == 'create') {
          this.kiosksStore.createSubsCreenRealTime(subscreen);
        } else if (eventType.toLowerCase() == 'delete') {
          this.kiosksStore.removeSubscreenRealTime(subscreen);
        }
      }
    );
    //Announce Events END

    //HomeAnnounce Events Start
    this.hubConnection.on('ReceiveHomeAnnounce', (announce: IHomeAnnounce) => {
      this.kiosksStore.updateOrCreateHomeAnnounceRealTime(announce);
    });
    this.hubConnection.on(
      'ReceiveHomeAnnouncePhoto',
      (photo: IHomeAnnouncePhoto, eventType: string) => {
        if (eventType.toLowerCase() == 'update') {
          this.kiosksStore.updateOrAddNewHomeAnnouncePhotoRealTime(photo);
        } else if (eventType.toLowerCase() == 'delete') {
          this.kiosksStore.deleteHomeAnnouncePhotoRealTime(photo);
        }
      }
    );
    this.hubConnection.on(
      'ReceiveHomeAnnounceSubScreen',
      (subscreen: IHomeAnnounceSubScreen, eventType: string) => {
        if (eventType.toLowerCase() == 'create') {
          this.kiosksStore.createHomeAnnounceSubsCreenRealTime(subscreen);
        } else if (eventType.toLowerCase() == 'delete') {
          this.kiosksStore.removeHomeAnnounceSubscreenRealTime(subscreen);
        }
      }
    );
    //HomeAnnounce Events END

    //VehicleAnnounce Events START
    this.hubConnection.on(
      'ReceiveVehicleAnnounce',
      (announce: IVehicleAnnounceList) => {
        this.kiosksStore.updateOrCreateVehicleAnnounceRealTime(announce);
      }
    );
    this.hubConnection.on(
      'ReceiveVehicleAnnouncePhoto',
      (photo: IVehicleAnnouncePhoto, eventType: string) => {
        if (eventType.toLowerCase() == 'update') {
          this.kiosksStore.updateOrAddNewVehicleAnnouncePhotoRealTime(photo);
        } else if (eventType.toLowerCase() == 'delete') {
          this.kiosksStore.deleteVehicleAnnouncePhotoRealTime(photo);
        }
      }
    );
    this.hubConnection.on(
      'ReceiveVehicleAnnounceSubScreen',
      (subscreen: IVehicleAnnounceSubScreen, eventType: string) => {
        if (eventType.toLowerCase() == 'create') {
          this.kiosksStore.createVehicleAnnounceSubsCreenRealTime(subscreen);
        } else if (eventType.toLowerCase() == 'delete') {
          this.kiosksStore.removeVehicleAnnounceSubscreenRealTime(subscreen);
        }
      }
    );
    //VehicleAnnounce Events END
    //News Events START
    this.hubConnection.on('ReceiveNews', (announce: INews) => {
      this.kiosksStore.updateOrCreateNewsRealTime(announce);
    });
    this.hubConnection.on(
      'ReceiveNewsPhoto',
      (photo: INewsPhoto, eventType: string) => {
        if (eventType.toLowerCase() == 'update') {
          this.kiosksStore.updateOrAddNewNewsPhotoRealTime(photo);
        } else if (eventType.toLowerCase() == 'delete') {
          this.kiosksStore.deleteNewsPhotoRealTime(photo);
        }
      }
    );
    this.hubConnection.on(
      'ReceiveNewsSubScreen',
      (subscreen: INewsSubScreen, eventType: string) => {
        if (eventType.toLowerCase() == 'create') {
          this.kiosksStore.createNewsSubsCreenRealTime(subscreen);
        } else if (eventType.toLowerCase() == 'delete') {
          this.kiosksStore.removeNewsSubscreenRealTime(subscreen);
        }
      }
    );
    //News Events END

    //FoodMenu Events START
    this.hubConnection.on('ReceiveFoodMenu', (announce: IFoodMenu) => {
      this.kiosksStore.updateOrCreateFoodMenuRealTime(announce);
    });

    this.hubConnection.on(
      'ReceiveFoodMenuPhoto',
      (photo: IFoodMenuPhoto, eventType: string) => {
        console.log(photo);
        console.log(eventType);
        if (eventType.toLowerCase() == 'update') {
          this.kiosksStore.updateOrAddNewFoodMenuPhotoRealTime(photo);
        } else if (eventType.toLowerCase() == 'delete') {
          this.kiosksStore.deleteFoodMenuPhotoRealTime(photo);
        }
      }
    );
    this.hubConnection.on(
      'ReceiveFoodMenuSubScreen',
      (subscreen: IFoodMenuSubScreen, eventType: string) => {
        if (eventType.toLowerCase() == 'create') {
          this.kiosksStore.createFoodMenuSubsCreenRealTime(subscreen);
        } else if (eventType.toLowerCase() == 'delete') {
          this.kiosksStore.removeFoodMenuSubscreenRealTime(subscreen);
        }
      }
    );
    //FoodMenu Events END
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
