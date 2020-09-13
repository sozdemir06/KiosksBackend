import { Component, OnInit, Input } from '@angular/core';
import { ILiveTvList } from 'src/app/shared/models/ILiveTvList';
import { MatDialog } from '@angular/material/dialog';
import { LiveTvListStore } from 'src/app/core/services/stores/live-tv-list-store';
import { EditTvOptionsDialogComponent } from './edit-tv-options-dialog/edit-tv-options-dialog.component';

@Component({
  selector: 'app-live-tv-options',
  templateUrl: './live-tv-options.component.html',
  styleUrls: ['./live-tv-options.component.scss']
})
export class LiveTvOptionsComponent implements OnInit {
  toolbarTitle:string="Tv Listesi";
  toolbarSearchInputPlaceholder:string="Arama kapalı";
  allowedRoles:string[]=["Sudo","BuildingsAge.Create"];

  constructor(
    public liveTvListStore:LiveTvListStore,
    private dialog:MatDialog
  ) { }

  ngOnInit(): void {
  }

  onCreate(){
    this.dialog.open(EditTvOptionsDialogComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Yeni Tv Ekle",
        mode:"create",
        item:null
      }
    })
  }

}
