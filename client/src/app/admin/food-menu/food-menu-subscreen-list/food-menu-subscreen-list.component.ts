import { Component, OnInit, Input } from '@angular/core';
import { IFoodMenuSubScreen } from 'src/app/shared/models/IFoodMenuSubScreen';
import { MatDialog } from '@angular/material/dialog';
import { FoodMenuStore } from 'src/app/core/services/stores/food-menu-store';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-food-menu-subscreen-list',
  templateUrl: './food-menu-subscreen-list.component.html',
  styleUrls: ['./food-menu-subscreen-list.component.scss']
})
export class FoodMenuSubscreenListComponent implements OnInit {

  @Input() subscreens:IFoodMenuSubScreen[];
  displayedColumns:string[]=["Name","Actions"];
  roleForRemove:string[]=['Sudo','FoodMenuSubScreens.Delete','FoodMenu.All']
  constructor(
    private dialog:MatDialog,
    private foodMenuStore:FoodMenuStore
  ) { }

  ngOnInit(): void {
  }

  onDelete(element:IFoodMenuSubScreen){
    const dialogRef=this.dialog.open(ConfirmDialogComponent,{
      width:"45rem",
      data:{
        message:`Menüyü ${element.subScreenName} adlı ekrandan kaldırmak istoyormusunuz?`
      }
    });
    dialogRef.afterClosed().subscribe(result=>{
      if(result){
        this.foodMenuStore.removeSubScreen(element);
      }
    })
  }


}
