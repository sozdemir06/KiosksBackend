import { Component, OnInit, OnDestroy, AfterViewInit } from '@angular/core';
import { KiosksStore } from './store/kiosks-store';
import { ActivatedRoute } from '@angular/router';
import { ISubScreen } from '../shared/models/ISubScreen';
import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { WheatherForeCastStore } from '../core/services/stores/wheatherforecast-store';
import { ExchangeRateStore } from '../core/services/stores/exchangerate-store';
import { KiosksHubService } from './store/kiosks-hub';
import { FoodMenuBgPhotoStore } from '../core/services/stores/food-menu-bg-photo-store';

@Component({
  selector: 'app-kiosks',
  templateUrl: './kiosks.component.html',
  styleUrls: ['./kiosks.component.scss'],
})
export class KiosksComponent implements OnInit, OnDestroy, AfterViewInit {
  screenId: number;
  top$: Observable<ISubScreen>;
  vmiddle$: Observable<ISubScreen>;
  bottom$: Observable<ISubScreen>;

  left$: Observable<ISubScreen>;
  hmiddle$: Observable<ISubScreen>;
  right$: Observable<ISubScreen>;

  leftTopPhotoUrl: string;
  rightTopPhotoUrl: string;
  leftSub = Subscription.EMPTY;
  rightSub = Subscription.EMPTY;

  constructor(
    public kioksStore: KiosksStore,
    private route: ActivatedRoute,
    public wheatherForeCastsStore$: WheatherForeCastStore,
    public exchangeRateStore$: ExchangeRateStore,
    public kiosksHub: KiosksHubService,
    private foodMenuBgPhotoStore:FoodMenuBgPhotoStore,
  ) {}

  ngAfterViewInit() {
    setTimeout(() => {
      this.screenId = +this.route.snapshot.paramMap.get('id');
      this.kioksStore.getListByScreenId(this.screenId);
      this.top$ = this.kioksStore.kiosks$.pipe(
        map((data) =>
          data.screen.subScreens.find(
            (x) => x.position?.toLowerCase() == 'top' && x.status == true
          )
        )
      );
      this.vmiddle$ = this.kioksStore.kiosks$.pipe(
        map((data) =>
          data.screen.subScreens.find(
            (x) => x.position?.toLowerCase() == 'vmiddle' && x.status == true
          )
        )
      );
      this.bottom$ = this.kioksStore.kiosks$.pipe(
        map((data) =>
          data.screen.subScreens.find(
            (x) => x.position?.toLowerCase() == 'bottom' && x.status == true
          )
        )
      );

      this.left$ = this.kioksStore.kiosks$.pipe(
        map((data) =>
          data.screen.subScreens.find(
            (x) => x.position?.toLowerCase() == 'left' && x.status == true
          )
        )
      );
      this.hmiddle$ = this.kioksStore.kiosks$.pipe(
        map((data) =>
          data.screen.subScreens.find(
            (x) => x.position?.toLowerCase() == 'hmiddle' && x.status == true
          )
        )
      );
      this.right$ = this.kioksStore.kiosks$.pipe(
        map((data) =>
          data.screen.subScreens.find(
            (x) => x.position?.toLowerCase() == 'right' && x.status == true
          )
        )
      );
      //Find left and right header photo
      this.leftSub = this.kioksStore.kiosks$
        .pipe(
          map((data) =>
            data?.screen?.screenHeaderPhotos.find(
              (x) => x.position?.toLowerCase() == 'left' && x.isMain == true
            )
          )
        )
        .subscribe((result) => {
          this.leftTopPhotoUrl = result?.photoUrl;
        });
      this.rightSub = this.kioksStore.kiosks$
        .pipe(
          map((data) =>
            data?.screen?.screenHeaderPhotos.find(
              (x) => x.position?.toLowerCase() == 'right' && x.isMain == true
            )
          )
        )
        .subscribe((result) => {
          this.rightTopPhotoUrl = result?.photoUrl;
        });
    });
  }

  ngOnInit(): void {
    this.kiosksHub.createHubConnection();

    this.kiosksHub.hubConnection.on('OnConnected', (connectionId: string) => {
      this.kiosksHub.onConnected(this.screenId, connectionId);
    });

    this.kiosksHub.kiosksListener();
    //Reload Screen if detect changes START
    this.kiosksHub.hubConnection.on(
      'ReloadScreen',
      (reload: boolean = false) => {
        if (reload) {
          this.kioksStore.getListByScreenId(this.screenId);
          this.foodMenuBgPhotoStore.reloadBgPhoto();
        }
      }
    );
    //Reload Screen if detect changes END
    setInterval(() => {
      this.exchangeRateStore$.getListByInterval();
      this.wheatherForeCastsStore$.getListByInterval();
    }, 120 * 60 * 1000);
  }

  ngOnDestroy() {
    this.leftSub.unsubscribe();
    this.rightSub.unsubscribe();
    this.kiosksHub.stopHubConnection();
  }
}
