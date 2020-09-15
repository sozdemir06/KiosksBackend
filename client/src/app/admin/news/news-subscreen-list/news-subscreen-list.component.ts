import { Component, OnInit, Input } from '@angular/core';
import { INewsSubScreen } from 'src/app/shared/models/INewsSubScreen';
import { MatDialog } from '@angular/material/dialog';
import { AnnounceStore } from 'src/app/core/services/stores/announce-store';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { NewsStore } from 'src/app/core/services/stores/news-store';

@Component({
  selector: 'app-news-subscreen-list',
  templateUrl: './news-subscreen-list.component.html',
  styleUrls: ['./news-subscreen-list.component.scss']
})
export class NewsSubscreenListComponent implements OnInit {

  @Input() subscreens:INewsSubScreen[];
  displayedColumns:string[]=["Name","Actions"];
  roleForRemove:string[]=['Sudo','News.Delete','News.All']
  constructor(
    private dialog:MatDialog,
    private newsStore:NewsStore
  ) { }

  ngOnInit(): void {
  }

  onDelete(element:INewsSubScreen){
    const dialogRef=this.dialog.open(ConfirmDialogComponent,{
      width:"45rem",
      data:{
        message:`Bu Haberi ${element.subScreenName} adlı ekrandan kaldırmak istoyormusunuz?`
      }
    });
    dialogRef.afterClosed().subscribe(result=>{
      if(result){
        this.newsStore.removeSubScreen(element);
      }
    })
  }

}
