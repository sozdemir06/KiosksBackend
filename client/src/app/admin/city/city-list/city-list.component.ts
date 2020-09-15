import { Component, OnInit, Input } from '@angular/core';
import { ICity } from 'src/app/shared/models/ICity';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { CityStore } from 'src/app/core/services/stores/city-store';

@Component({
  selector: 'app-city-list',
  templateUrl: './city-list.component.html',
  styleUrls: ['./city-list.component.scss']
})
export class CityListComponent implements OnInit {
  displayedColumns: string[] = ["Id",'Name',"Selected","Actions"];
  @Input() dataSource:ICity[];
  allowedRoles:string[]=['Sudo','AddCityForWheatherForeCast'];


  constructor(
    private dialog:MatDialog,
    private cityStore:CityStore
  ) { }

  ngOnInit(): void {
  }


  onUpdate(city:ICity){
    let message:string;
    if(city.selected){
      message=city.name+" için hava durumu bilgisini kaldırmak istiyormusunuz.?"
    }else if(!city.selected){
      message=city.name+" için hava durumunun görünmesini istiyormusnuz.?"
    }
    const dialogRef=this.dialog.open(ConfirmDialogComponent,{
      width:"45rem",
      data:{
        message:message
      }
    });
    dialogRef.afterClosed().subscribe(result=>{
      if(result){
        const model:ICity={
          ...city,
          selected:city.selected?false:true
        }
        this.cityStore.update(model);
      }
    })
  }



}
