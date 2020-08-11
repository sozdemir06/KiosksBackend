import { Component, OnInit, Input } from '@angular/core';
import { IAnnounceContentType } from 'src/app/shared/models/IAnnounceContentType';
import { MatDialog } from '@angular/material/dialog';
import { EditAnnounceContentTypeDialogComponent } from '../edit-announce-content-type-dialog/edit-announce-content-type-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { AnnounceContentTypeStore } from 'src/app/core/services/stores/announce-content-type-store';

@Component({
  selector: 'app-announce-content-type-list',
  templateUrl: './announce-content-type-list.component.html',
  styleUrls: ['./announce-content-type-list.component.scss']
})
export class AnnounceContentTypeListComponent implements OnInit {
displayedColumns: string[] = ["Id",'Name',"Description","Actions"];
@Input() dataSource:IAnnounceContentType[];
allowedRoleFlatsOfHomeForUpdate:string[]=['Sudo','AnnounceContentTypes.Create'];
allowedRoleFlatsOfHomeForDelete:string[]=['Sudo','AnnounceContentTypes.Delete'];
  constructor(
    private dialog:MatDialog,
    private announceContentTypeStore:AnnounceContentTypeStore
  ) { }

  ngOnInit(): void {
  }

  onDelete(item:IAnnounceContentType){
    const dialogRef=this.dialog.open(ConfirmDialogComponent,{
      width:"45rem",
    });
    dialogRef.afterClosed().subscribe(result=>{
      if(result){
        this.announceContentTypeStore.delete(item);
      }
    })
  }

  onUpdate(item:IAnnounceContentType){
    this.dialog.open(EditAnnounceContentTypeDialogComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Opsiyon güncelle",
        mode:"update",
        item:item
      }
    })
  }

}
