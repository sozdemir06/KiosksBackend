import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';
import { IUser, IUserList } from 'src/app/shared/models/IUser';
import { IAnnounce } from 'src/app/shared/models/IAnnounce';
import { AnnounceStore } from './stores/announce-store';
import { IAnnouncePhoto } from 'src/app/shared/models/IAnnouncePhoto';
import { IHomeAnnounce } from 'src/app/shared/models/IHomeAnnounce';
import { HomeAnnounceStore } from './stores/home-announce-store';
import { IHomeAnnouncePhoto } from 'src/app/shared/models/IHomeAnnouncePhoto';
import { IVehicleAnnounceList } from 'src/app/shared/models/IVehicleAnnounceList';
import { VehilceAnnounceStore } from './stores/vehicle-announce-store';
import { IVehicleAnnouncePhoto } from 'src/app/shared/models/IVehicleAnnouncePhoto';
import { UserStore } from './stores/user-store';
import { IUserPhoto } from 'src/app/shared/models/IUserPhoto';
import { INews } from 'src/app/shared/models/INews';
import { NewsStore } from './stores/news-store';
import { INewsPhoto } from 'src/app/shared/models/INewsPhoto';
import { IFoodMenu } from 'src/app/shared/models/IFoodMenu';
import { IFoodMenuPhoto } from 'src/app/shared/models/IFoodMenuPhoto';
import { FoodMenuStore } from './stores/food-menu-store';

@Injectable({ providedIn: 'root' })
export class AdminHubService {
  hubUrl: string = environment.hubUrl;
  hubConnection: HubConnection;
  private statusSubject = new BehaviorSubject<string>(null);
  status$: Observable<string> = this.statusSubject.asObservable();

  constructor(
    private announcestore: AnnounceStore,
    private homeAnnouncestore: HomeAnnounceStore,
    private vehicleAnnounceStore: VehilceAnnounceStore,
    private userStore: UserStore,
    private newsStore: NewsStore,
    private foodMenuStore:FoodMenuStore
  ) {}

  createHubConneciton(user: IUser) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'AdminHub', {
        accessTokenFactory: () => user.token,
      })
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
  }

  onListenersForAdmin() {
    //Announce Public To Admin
    this.hubConnection.on('ReceiveNewAnnounce', (announce: IAnnounce) => {
      this.announcestore.addNewAnnounceRealTime(announce);
    });
    this.hubConnection.on('ReceiveUpdateAnnounce', (announce: IAnnounce) => {
      this.announcestore.updateAnnounceRealTime(announce);
    });
    this.hubConnection.on(
      'ReceiveNewPhotoAnnounce',
      (photo: IAnnouncePhoto,eventType:string) => {
        const type = eventType.toLowerCase();
        if (type == 'create') {
          this.announcestore.addNewPhotoRealTime(photo);
        } else if (type == 'update') {
          this.announcestore.updatePhotoRealTime(photo);
        } else if (type == 'delete') {
          this.announcestore.removePhotoRealTime(photo);
        }
      }
    );
    //Announce Public To Admin End

    //HomeAnnounce Public To Admin Start
    this.hubConnection.on(
      'ReceiveNewHomeAnnounce',
      (homeAnnounce: IHomeAnnounce) => {
        this.homeAnnouncestore.createNewHomeAnnounceRealTime(homeAnnounce);
      }
    );
    this.hubConnection.on(
      'ReceiveUpdateHomeAnnounce',
      (homeAnnounce: IHomeAnnounce) => {
        this.homeAnnouncestore.updateHomeAnnounceRealTime(homeAnnounce);
      }
    );
    this.hubConnection.on(
      'ReceiveNewHomeAnnouncePhoto',
      (photo: IHomeAnnouncePhoto,eventType:string) => {
        const type = eventType.toLowerCase();
        if (type == 'create') {
          this.homeAnnouncestore.addNewPhotoRealTime(photo);
        } else if (type == 'update') {
          this.homeAnnouncestore.updatePhotoRealTime(photo);
        } else if (type == 'delete') {
          this.homeAnnouncestore.removePhotoRealTime(photo);
        }
      }
    );
    //HomeAnnounce Public To Admin End

    //VehicleAnnounce Public To Admin Start
    this.hubConnection.on(
      'ReceiveNewVehicleannounce',
      (vehicleAnnounce: IVehicleAnnounceList) => {
        this.vehicleAnnounceStore.createNewVehicleAnnounce(vehicleAnnounce);
      }
    );
    this.hubConnection.on(
      'ReceiveUpdateVehicleannounce',
      (vehicleAnnounce: IVehicleAnnounceList) => {
        this.vehicleAnnounceStore.updateVehicleannounce(vehicleAnnounce);
      }
    );
    this.hubConnection.on(
      'ReceiveNewVehicleannouncePhoto',
      (photo: IVehicleAnnouncePhoto,eventType:string) => {
        const type = eventType.toLowerCase();
        if (type == 'create') {
          this.vehicleAnnounceStore.addNewPhotoRealTime(photo);
        } else if (type == 'update') {
          this.vehicleAnnounceStore.updatePhotoRealTime(photo);
        } else if (type == 'delete') {
          this.vehicleAnnounceStore.removePhotoRealTime(photo);
        }
      }
    );
    //Vehicleannounce Public To Admin End

    //Register New User Start
    this.hubConnection.on('ReceiveNewUser', (user: IUserList) => {
      this.userStore.create(user);
    });
    this.hubConnection.on(
      'ReceiveNewUserProfilePhoto',
      (userphoto: IUserPhoto) => {
        this.userStore.addPhoto(userphoto);
      }
    );
    //Register New User End

    //News Start
    this.hubConnection.on('ReceiveNewNews', (news: INews) => {
      this.newsStore.createNewNews(news);
    });
    this.hubConnection.on('ReceiveUpdateNews', (news: INews) => {
      this.newsStore.updateNews(news);
    });
    this.hubConnection.on(
      'ReceiveNewsPhoto',
      (photo: INewsPhoto, eventType: string) => {
        const type = eventType.toLowerCase();
        if (type == 'create') {
          this.newsStore.addNewPhotoRealTime(photo);
        } else if (type == 'update') {
          this.newsStore.updatePhotoRealTime(photo);
        } else if (type == 'delete') {
          this.newsStore.removePhotoRealTime(photo);
        }
      }
    );

    //News End
    //FoodMenu Start
    this.hubConnection.on('ReceiveNewFoodMenu', (foodMenu: IFoodMenu) => {
        this.foodMenuStore.createNewFoodMenuRealTime(foodMenu);
      });
      this.hubConnection.on('ReceiveUpdateFoodMenu', (foodMenu: IFoodMenu) => {
        this.foodMenuStore.updateFoodMenuRealTime(foodMenu);
      });
      this.hubConnection.on(
        'ReceiveFoodMenuPhoto',
        (photo: IFoodMenuPhoto, eventType: string) => {
          const type = eventType.toLowerCase();
          if (type == 'create') {
            this.foodMenuStore.addNewPhotoRealTime(photo);
          } else if (type == 'update') {
            this.foodMenuStore.updatePhotoRealTime(photo);
          } else if (type == 'delete') {
            this.foodMenuStore.removePhotoRealTime(photo);
          }
        }
      );
  
      //FoodMenu End


  }

  stopHubConnection() {
    this.hubConnection.stop().catch((error) => {
      this.statusSubject.next(this.hubConnection.state);
    });
  }
}
