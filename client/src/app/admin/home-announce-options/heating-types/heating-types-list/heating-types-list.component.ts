import { Component, OnInit, Input } from '@angular/core';
import { IHeatingType } from 'src/app/shared/models/IHeatingType';
import { MatDialog } from '@angular/material/dialog';
import { EditHeatingTypesDialogComponent } from '../edit-heating-types-dialog/edit-heating-types-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { HeatingTypeStore } from 'src/app/core/services/stores/heating-type-store';

@Component({
  selector: 'app-heating-types-list',
  templateUrl: './heating-types-list.component.html',
  styleUrls: ['./heating-types-list.component.scss'],
})
export class HeatingTypesListComponent implements OnInit {
  displayedColumns: string[] = ['Id', 'Name', 'Actions'];
  @Input() dataSource: IHeatingType[];
  alloweRolesHeatingTypesForUpdate:string[]=['Sudo','HeatingTypes.Create'];
  alloweRolesHeatingTypesForDelete:string[]=['Sudo','HeatingTypes.Delete'];
  constructor(
    private dialog: MatDialog,
    private heatingTypeStore:HeatingTypeStore
    
    ) {}

  ngOnInit(): void {}

  onUpdate(element: IHeatingType) {
    this.dialog.open(EditHeatingTypesDialogComponent, {
      width: '45rem',
      maxHeight: '100vh',
      data: {
        title: 'Opsiyon güncelle',
        mode: 'update',
        item: element,
      },
    });
  }

  onDelete(element: IHeatingType) {
    const dialogRef=this.dialog.open(ConfirmDialogComponent,{
      width:"45rem",

    });

    dialogRef.afterClosed().subscribe(result=>{
      if(result){
        this.heatingTypeStore.delete(element);
      }
    })
  }
}
