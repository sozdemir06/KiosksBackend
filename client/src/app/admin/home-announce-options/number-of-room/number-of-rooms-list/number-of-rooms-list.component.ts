import { Component, OnInit, Input } from '@angular/core';
import { INumberOfRoom } from 'src/app/shared/models/INumberOFRoom';
import { MatDialog } from '@angular/material/dialog';
import { EditNumberOfRoomsDialogComponent } from '../edit-number-of-rooms-dialog/edit-number-of-rooms-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { NumberOfroomStore } from 'src/app/core/services/stores/number-of-rrom-store';

@Component({
  selector: 'app-number-of-rooms-list',
  templateUrl: './number-of-rooms-list.component.html',
  styleUrls: ['./number-of-rooms-list.component.scss']
})
export class NumberOfRoomsListComponent implements OnInit {
  displayedColumns: string[] = ["Id",'Name',"Actions"];
  @Input() dataSource:INumberOfRoom[];
  constructor(
    private dialog:MatDialog,
    private numberOfRoomStore:NumberOfroomStore
  ) { }

  ngOnInit(): void {
  }

  onUpdate(element:INumberOfRoom){
    this.dialog.open(EditNumberOfRoomsDialogComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Opsiyon Listesini Güncelle",
        mode:"update",
        item:element
      }
    })
  }

  onDelete(element:INumberOfRoom){
    const dialogRef=this.dialog.open(ConfirmDialogComponent,{
      width:"45rem",
    });

    dialogRef.afterClosed().subscribe(result=>{
      if(result){
        this.numberOfRoomStore.delete(element);
      }
    })
  }

}
