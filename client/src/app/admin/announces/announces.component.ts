import {
  Component,
  OnInit,
  AfterViewInit,
  OnDestroy,
  ElementRef,
  ViewChild,
} from '@angular/core';
import { AnnounceStore } from 'src/app/core/services/stores/announce-store';
import { PageEvent } from '@angular/material/paginator';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { Observable, Subscription, fromEvent } from 'rxjs';
import { SubScreenStore } from 'src/app/core/services/stores/subscreen-store';
import { map, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { EditAnnouncesDialogComponent } from './edit-announces-dialog/edit-announces-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { AnnounceParams } from 'src/app/shared/models/AnnounceParams';

@Component({
  selector: 'app-announces',
  templateUrl: './announces.component.html',
  styleUrls: ['./announces.component.scss'],
})
export class AnnouncesComponent implements OnInit, AfterViewInit, OnDestroy {
  unSubsCribeFromSearchInput: Subscription = Subscription.EMPTY;
  @ViewChild('searchInput') searchInput: ElementRef;
  roleForCreate: string[] = ['Sudo', 'Announces.Create,Announces.All'];
  constructor(
    public announceStore: AnnounceStore,
    public subScreenStore: SubScreenStore,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {}

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
        const params = this.announceStore.getParams();
        params.search = result;
        this.announceStore.setParams(params);
        this.announceStore.getListByParams();
      });
  }

  onPageChange(event: PageEvent) {
    const params = this.announceStore.getParams();
    params.pageSize = event.pageSize;
    params.pageIndex = event.pageIndex + 1;
    this.announceStore.setParams(params);
    this.announceStore.getListByParams();
  }

  onWaitingForConfirm() {
    const params = this.announceStore.getParams();
    params.isNew = true;
    this.announceStore.setParams(params);
    this.announceStore.getListByParams();
  }

  onCreateNew() {
    this.dialog.open(EditAnnouncesDialogComponent, {
      width: '60vw',
      maxHeight: '100vh',
      autoFocus: false,
      data: {
        title: 'Yeni Duyuru  ekle',
        mode: 'create',
        item: null,
      },
    });
  }

  onReset() {
    const params = new AnnounceParams();
    this.announceStore.setParams(params);
    this.announceStore.getListByParams();
  }

  filterBySubScreen(id: number) {
    const params = this.announceStore.getParams();
    params.subScreenId = id;
    this.announceStore.setParams(params);
    this.announceStore.getListByParams();
  }

  ngOnDestroy() {
    this.unSubsCribeFromSearchInput.unsubscribe();
  }
}
