import { Component, OnInit, Input } from '@angular/core';
import { ILiveTvList } from 'src/app/shared/models/ILiveTvList';
import { MatDialog } from '@angular/material/dialog';
import { LiveTvListStore } from 'src/app/core/services/stores/live-tv-list-store';
import { EditTvOptionsDialogComponent } from '../edit-tv-options-dialog/edit-tv-options-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-live-tv-options-list',
  templateUrl: './live-tv-options-list.component.html',
  styleUrls: ['./live-tv-options-list.component.scss'],
})
export class LiveTvOptionsListComponent implements OnInit {
  displayedColumns: string[] = ['Id', 'Name', 'YoutubeId', 'Actions'];
  @Input() dataSource: ILiveTvList[];
  allowedRoles: string[] = ['Sudo', 'LiveTvBroadCastsOptions.All'];

  constructor(
    private dialog: MatDialog,
    private liveTvListStore: LiveTvListStore
  ) {}

  ngOnInit(): void {}

  onUpdate(element: ILiveTvList) {
    this.dialog.open(EditTvOptionsDialogComponent, {
      width: '45rem',
      maxHeight: '100vh',
      data: {
        title: 'Tv Güncelle',
        mode: 'update',
        item: element,
      },
    });
  }

  onDelete(element: ILiveTvList) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.liveTvListStore.delete(element);
      }
    });
  }
}
