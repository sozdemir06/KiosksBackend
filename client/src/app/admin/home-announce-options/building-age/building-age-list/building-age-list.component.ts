import { Component, OnInit, Input } from '@angular/core';
import { IBuildingAge } from 'src/app/shared/models/IBuildingAge';
import { MatDialog } from '@angular/material/dialog';
import { EditBuildingAgeDialogComponent } from '../edit-building-age-dialog/edit-building-age-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { BuildingAgeStore } from 'src/app/core/services/stores/building-age-store';

@Component({
  selector: 'app-building-age-list',
  templateUrl: './building-age-list.component.html',
  styleUrls: ['./building-age-list.component.scss']
})
export class BuildingAgeListComponent implements OnInit {
  displayedColumns: string[] = ["Id",'Name',"Actions"];
  @Input() dataSource:IBuildingAge[];
  allowedRoles:string[]=['Sudo','HomeAnnounceOptions.All'];


  constructor(
    private dialog:MatDialog,
    private buildingStore:BuildingAgeStore
  ) { }

  ngOnInit(): void {
  }

  onUpdate(element:IBuildingAge){
      this.dialog.open(EditBuildingAgeDialogComponent,{
        width:"45rem",
        maxHeight:"100vh",
        data:{
          title:"Opsiyon Güncelle",
          mode:"update",
          item:element
        }
      });
  }

  onDelete(element:IBuildingAge){
      const dialogRef=this.dialog.open(ConfirmDialogComponent,{
        width:"45rem"
      })

      dialogRef.afterClosed().subscribe(result=>{
        if(result){
           this.buildingStore.delete(element);

        }
      })
  }

}
