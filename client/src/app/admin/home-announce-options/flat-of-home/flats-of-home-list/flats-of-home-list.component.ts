import { Component, OnInit, Input } from '@angular/core';
import { IFlatOfHome } from 'src/app/shared/models/IFlatOfHome';
import { MatDialog } from '@angular/material/dialog';
import { EditFlatsOfHomeDialogComponent } from '../edit-flats-of-home-dialog/edit-flats-of-home-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { FlatOfHomeStore } from 'src/app/core/services/stores/flat-of-home-store';

@Component({
  selector: 'app-flats-of-home-list',
  templateUrl: './flats-of-home-list.component.html',
  styleUrls: ['./flats-of-home-list.component.scss']
})
export class FlatsOfHomeListComponent implements OnInit {
  displayedColumns: string[] = ["Id",'Name',"Actions"];
  @Input() dataSource:IFlatOfHome[];
  constructor(
      private dialog:MatDialog,
      private flatOfHomeStore:FlatOfHomeStore
  ) { }

  ngOnInit(): void {
  }

  onUpdate(element:IFlatOfHome){
    this.dialog.open(EditFlatsOfHomeDialogComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Opsiyon güncelle",
        mode:"update",
        item:element
      }
    })
  }

  onDelete(element:IFlatOfHome){
    const dialogRef=this.dialog.open(ConfirmDialogComponent,{
      width:"45rem"
    });

    dialogRef.afterClosed().subscribe(result=>{
      if(result){
        this.flatOfHomeStore.delete(element);
      }
    })
  }

}
