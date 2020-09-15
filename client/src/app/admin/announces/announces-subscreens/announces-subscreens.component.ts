import { Component, OnInit, Input } from '@angular/core';
import { IAnnounceSubScreen } from 'src/app/shared/models/IAnnounceSubScreen';
import { MatDialog } from '@angular/material/dialog';
import { AnnounceStore } from 'src/app/core/services/stores/announce-store';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-announces-subscreens',
  templateUrl: './announces-subscreens.component.html',
  styleUrls: ['./announces-subscreens.component.scss']
})
export class AnnouncesSubscreensComponent implements OnInit {

  @Input() subscreens:IAnnounceSubScreen[];
  displayedColumns:string[]=["Name","Actions"];
  roleForRemove:string[]=['Sudo','Announces.Delete','Announces.All']
  constructor(
    private dialog:MatDialog,
    private announceStore:AnnounceStore
  ) { }

  ngOnInit(): void {
  }

  onDelete(element:IAnnounceSubScreen){
    const dialogRef=this.dialog.open(ConfirmDialogComponent,{
      width:"45rem",
      data:{
        message:`Bu Duyuruyu ${element.subScreenName} adlı ekrandan kaldırmak istoyormusunuz?`
      }
    });
    dialogRef.afterClosed().subscribe(result=>{
      if(result){
        this.announceStore.removeSubScreen(element);
      }
    })
  }
}
