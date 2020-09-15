import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { LiveTvBroadCastParams } from 'src/app/shared/models/LiveTvBroadCastParams';
import { EditLiveTvDialogComponent } from './edit-live-tv-dialog/edit-live-tv-dialog.component';
import { PageEvent } from '@angular/material/paginator';
import { fromEvent, Observable, Subscription } from 'rxjs';
import { map, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { LiveTvBroadCastStore } from 'src/app/core/services/stores/live-broadcast-store';
import { SubScreenStore } from 'src/app/core/services/stores/subscreen-store';
import { MatDialog } from '@angular/material/dialog';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';

@Component({
  selector: 'app-live-tv',
  templateUrl: './live-tv.component.html',
  styleUrls: ['./live-tv.component.scss'],
})
export class LiveTvComponent implements OnInit {
  @ViewChild('searchInput') searchInput: ElementRef;
  subscription: Subscription = Subscription.EMPTY;

  roleForCreate: string[] = [
    'Sudo',
    'LiveTvBroadCasts.Create,LiveTvBroadCasts.All',
  ];
  subscreens$: Observable<ISubScreen[]>;

  constructor(
    public liveTvBroadCastStore: LiveTvBroadCastStore,
    public subScreenStore: SubScreenStore,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {}

  ngAfterViewInit() {
    this.subscription = fromEvent<any>(this.searchInput.nativeElement, 'keyup')
      .pipe(
        map((event) => event.target.value),
        debounceTime(400),
        distinctUntilChanged()
      )
      .subscribe((result) => {
        const params = this.liveTvBroadCastStore.getParams();
        params.search = result;
        this.liveTvBroadCastStore.setParams(params);
        this.liveTvBroadCastStore.getListByParams();
      });
  }

  onPageChange(event: PageEvent) {
    const params = this.liveTvBroadCastStore.getParams();
    params.pageSize = event.pageSize;
    params.pageIndex = event.pageIndex + 1;
    this.liveTvBroadCastStore.setParams(params);
    this.liveTvBroadCastStore.getListByParams();
  }

  onWaitingForConfirm() {
    const params = this.liveTvBroadCastStore.getParams();
    params.isNew = true;
    this.liveTvBroadCastStore.setParams(params);
    this.liveTvBroadCastStore.getListByParams();
  }

  onCreateNew() {
    this.dialog.open(EditLiveTvDialogComponent, {
      width: '60vw',
      maxHeight: '100vh',
      autoFocus: false,
      data: {
        title: 'Yeni Menü  ekle',
        mode: 'create',
        item: null,
      },
    });
  }

  onReset() {
    const params = new LiveTvBroadCastParams();
    this.liveTvBroadCastStore.setParams(params);
    this.liveTvBroadCastStore.getListByParams();
  }

  filterBySubScreen(id: number) {
    const params = this.liveTvBroadCastStore.getParams();
    params.subScreenId = id;
    this.liveTvBroadCastStore.setParams(params);
    this.liveTvBroadCastStore.getListByParams();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
