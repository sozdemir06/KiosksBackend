import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { LiveTvBroadCastStore } from 'src/app/core/services/stores/live-broadcast-store';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { ILiveTvBroadCastSubScreen } from 'src/app/shared/models/ILiveTvBroadCastSubScreen';

@Component({
  selector: 'app-edit-live-tv-subscreens',
  templateUrl: './edit-live-tv-subscreens.component.html',
  styleUrls: ['./edit-live-tv-subscreens.component.scss']
})
export class EditLiveTvSubscreensComponent implements OnInit {
  @Input() subscreens:ILiveTvBroadCastSubScreen[];
  displayedColumns:string[]=["Name","Actions"];
  roleForRemove:string[]=['Sudo','LiveTvBroadCastSubScreens.Delete','LiveTvBroadCast.All'];
  
    constructor(
      private dialog:MatDialog,
      private liveTvBroadCastStore:LiveTvBroadCastStore
    ) { }
  
    ngOnInit(): void {
    }
  
    onDelete(subscreen:ILiveTvBroadCastSubScreen){
      const dialogRef=this.dialog.open(ConfirmDialogComponent,{
        width:"45rem",
        data:{
          message:`Bu yayını ${subscreen.subScreenName} adlı ekrandan kaldırmak istiyormusunuz?`
        }
      });
      dialogRef.afterClosed().subscribe(result=>{
        if(result){
          this.liveTvBroadCastStore.removeSubScreen(subscreen);
        }
      })
    }
  
}
