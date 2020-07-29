import { Component, OnInit, Input } from '@angular/core';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { IHomeAnnounceSubScreen } from 'src/app/shared/models/IHomeAnnounceSubScreen';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { HomeAnnounceSubScreenStore } from 'src/app/core/services/stores/home-announce-subscreen-store';

@Component({
  selector: 'app-home-announce-subscreens',
  templateUrl: './home-announce-subscreens.component.html',
  styleUrls: ['./home-announce-subscreens.component.scss']
})
export class HomeAnnounceSubscreensComponent implements OnInit {
@Input() subscreens:ISubScreen[];
displayedColumns:string[]=["Name","Status","Actions"];
  constructor(
    private dialog:MatDialog,
    private homeAnnounceSubscreenStore:HomeAnnounceSubScreenStore
  ) { }

  ngOnInit(): void {
  }

  onDelete(subscreen:IHomeAnnounceSubScreen){
    const dialogRef=this.dialog.open(ConfirmDialogComponent,{
      width:"45rem",
      data:{
        message:"İlanı/Yayını bu ekrandan kaldırmak istiyormusunuz..."
      }
    });
    dialogRef.afterClosed().subscribe(result=>{
      if(result){
        this.homeAnnounceSubscreenStore.delete(subscreen.id);
      }
    })
  }

}
