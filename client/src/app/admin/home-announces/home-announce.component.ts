import {
  Component,
  OnInit,
  ViewChild,
  ElementRef,
  AfterViewInit,
  OnDestroy,
} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EditHomeAnnounceDialogComponent } from './edit-home-announce-dialog/edit-home-announce-dialog.component';
import { HomeAnnounceStore } from 'src/app/core/services/stores/home-announce-store';
import { SubScreenStore } from 'src/app/core/services/stores/subscreen-store';
import { Observable, fromEvent } from 'rxjs';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { HomeAnnounceParams } from 'src/app/shared/models/HomeAnnounceParams';
import { PageEvent } from '@angular/material/paginator';
import { map, debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-home-announce',
  templateUrl: './home-announce.component.html',
  styleUrls: ['./home-announce.component.scss'],
})
export class HomeAnnounceComponent implements OnInit, AfterViewInit, OnDestroy {
  subscreens$: Observable<ISubScreen[]>;
  @ViewChild('searchInput') searchInput: ElementRef;
  unSubsCribeFromSearchInput: any;
  roleForCreate:string[]=['Sudo','HomeAnnounces.Create,HomeAnnounces.All']
  constructor(
    private dialog: MatDialog,
    public homeAnnounceStore: HomeAnnounceStore,
    public subscreenStore: SubScreenStore
  ) {}

  ngOnInit(): void {
    this.subscreens$ = this.subscreenStore.getScreenListForFilters();
  }

  ngAfterViewInit() {
    this.unSubsCribeFromSearchInput = fromEvent<any>(
      this.searchInput.nativeElement,
      'keyup'
    )
      .pipe(
        map((event) => event.target.value),
        debounceTime(400),
        distinctUntilChanged()
      )
      .subscribe((result) => {
        const params = this.homeAnnounceStore.getHomeAnnounceParams();
        params.search = result;
        this.homeAnnounceStore.setHomeAnnounceParams(params);
        this.homeAnnounceStore.invokeGetlistWithParams();
      });
  }

  filterBySubScreen(subScreenId: number) {
    const params = this.homeAnnounceStore.getHomeAnnounceParams();
    params.subScreenId = subScreenId;
    this.homeAnnounceStore.setHomeAnnounceParams(params);
    this.homeAnnounceStore.invokeGetlistWithParams();
  }

  onWaitingForConfirm(){
    const params = this.homeAnnounceStore.getHomeAnnounceParams();
    params.isNew=true;
    this.homeAnnounceStore.setHomeAnnounceParams(params);
    this.homeAnnounceStore.invokeGetlistWithParams();
  }

  onCreateNew() {
    this.dialog.open(EditHomeAnnounceDialogComponent, {
      width: '55rem',
      maxHeight: '100vh',
      data: {
        title: 'Yeni Ev ilanı Ekle',
        mode: 'create',
        item: null,
      },
    });
  }

  onPageChange(event: PageEvent) {
    const params = this.homeAnnounceStore.getHomeAnnounceParams();
    params.pageSize = event.pageSize;
    params.pageIndex = event.pageIndex + 1;
    this.homeAnnounceStore.setHomeAnnounceParams(params);
    this.homeAnnounceStore.invokeGetlistWithParams();
  }

  onReset() {
    const params = new HomeAnnounceParams();
    this.homeAnnounceStore.setHomeAnnounceParams(params);
    this.homeAnnounceStore.invokeGetlistWithParams();
  }

  ngOnDestroy() {
    this.unSubsCribeFromSearchInput?.unsubscribe();
  }
}
