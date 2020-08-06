import { Component, OnInit, Input } from '@angular/core';
import { IHomeAnnounceSubScreen } from 'src/app/shared/models/IHomeAnnounceSubScreen';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { HomeAnnounceStore } from 'src/app/core/services/stores/home-announce-store';

@Component({
  selector: 'app-home-announce-subscreens',
  templateUrl: './home-announce-subscreens.component.html',
  styleUrls: ['./home-announce-subscreens.component.scss']
})
export class HomeAnnounceSubscreensComponent implements OnInit {
@Input() subscreens:IHomeAnnounceSubScreen[];
displayedColumns:string[]=["Name","Actions"];
roleForRemove:string[]=['Sudo','HomeAnnounceSubScreens.Delete','HomeAnnounces.All'];

  constructor(
    private dialog:MatDialog,
    private homeAnnouncestore:HomeAnnounceStore
  ) { }

  ngOnInit(): void {
  }

  onDelete(subscreen:IHomeAnnounceSubScreen){
    const dialogRef=this.dialog.open(ConfirmDialogComponent,{
      width:"45rem",
      data:{
        message:`Bu Ev ilanını ${subscreen.subScreenName} adlı ekrandan kaldırmak istiyormusunuz?`
      }
    });
    dialogRef.afterClosed().subscribe(result=>{
      if(result){
        this.homeAnnouncestore.removeSubScreen(subscreen);
      }
    })
  }

}
