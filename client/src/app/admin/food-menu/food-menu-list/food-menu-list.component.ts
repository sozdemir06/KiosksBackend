import { Component, OnInit, Input } from '@angular/core';
import { IFoodMenu } from 'src/app/shared/models/IFoodMenu';
import { FoodMenuStore } from 'src/app/core/services/stores/food-menu-store';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { IVehicleAnnounceList } from 'src/app/shared/models/IVehicleAnnounceList';
import { EditFoodMenuDialogComponent } from '../edit-food-menu-dialog/edit-food-menu-dialog.component';

@Component({
  selector: 'app-food-menu-list',
  templateUrl: './food-menu-list.component.html',
  styleUrls: ['./food-menu-list.component.scss']
})
export class FoodMenuListComponent implements OnInit {
  @Input() dataSource:IFoodMenu[];
  displayedColumns:string[]=['Image','Created','PublishDates','PublishStatus','Actions'];
  
  roleForUpdate:string[]=["Sudo","FoodMenu.Update","FoodMenu.All"]
  roleForPublish:string[]=["Sudo","FoodMenu.Publish","FoodMenu.All"]
    constructor(
      private dialog:MatDialog,
      private foodMenuStore:FoodMenuStore
    ) { }
  
    ngOnInit(): void {
    }
  
    onUpdate(element:IFoodMenu){
      this.dialog.open(EditFoodMenuDialogComponent,{
        width:"55rem",
        maxHeight:"100vh",
        autoFocus:false,
        data:{
          title:"Araç ilanını güncelle",
          mode:"update",
          item:element
        }
      })    
    }
  
    onPublish(announce:IFoodMenu){
      const dialogRef = this.dialog.open(ConfirmDialogComponent, {
        width: '45rem',
        data: {
          message: 'Menuyü yayınlamak  istiyormusunuz.?',
        },
      });
      dialogRef.afterClosed().subscribe((result) => {
        if (result) {
          const model: IFoodMenu = {
            ...announce,
            isNew: false,
            reject: false,
            isPublish: true,
          };
          this.foodMenuStore.publish(model);
        }
      });
    }
    unPublish(announce:IFoodMenu){
      const dialogRef = this.dialog.open(ConfirmDialogComponent, {
        width: '45rem',
        data: {
          message: 'Menüyü yayından kaldırmak istiyormusunuz.?',
        },
      });
      dialogRef.afterClosed().subscribe((result) => {
        if (result) {
          const model: IFoodMenu = {
            ...announce,
            isNew: false,
            reject: false,
            isPublish: false
          };
          this.foodMenuStore.publish(model);
        }
      });
    }
  
    onReject(announce:IFoodMenu){
       const dialogRef = this.dialog.open(ConfirmDialogComponent, {
        width: '45rem',
        data: {
          message: 'Yemek Menusu Reddetmek istiyormusunuz.?',
        },
      });
      dialogRef.afterClosed().subscribe((result) => {
        if (result) {
          const model: IFoodMenu = {
            ...announce,
            isNew: false,
            reject: true,
            isPublish: false
          };
          this.foodMenuStore.publish(model);
        }
      });
    }
  
    onDelete(element:IVehicleAnnounceList){
      console.log("deleted");
    }
  

}
