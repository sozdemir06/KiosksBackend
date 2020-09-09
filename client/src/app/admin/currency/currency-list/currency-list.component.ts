import { Component, OnInit, Input } from '@angular/core';
import { ICurrency } from 'src/app/shared/models/ICurrency';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { CurrencyStore } from 'src/app/core/services/stores/currency-store';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-currency-list',
  templateUrl: './currency-list.component.html',
  styleUrls: ['./currency-list.component.scss']
})
export class CurrencyListComponent implements OnInit {
  displayedColumns: string[] = ["Id",'Name',"Selected","Actions"];
  @Input() dataSource:ICurrency[];
  allowedRoleCityForUpdate:string[]=['Sudo','Currencies.Update'];
  allowedRoleCityForDelete:string[]=['Sudo','Currencies.Delete'];

  constructor(
    private dialog:MatDialog,
    private currencyStore:CurrencyStore
  ) { }

  ngOnInit(): void {
  }


  onUpdate(currency:ICurrency){
    let message:string;
    if(currency.selected){
      message=currency.name+" için kur bilgisini kaldırmak istiyormusunuz.?"
    }else if(!currency.selected){
      message=currency.name+" için kur bilgisini görünmesini istiyormusnuz.?"
    }
    const dialogRef=this.dialog.open(ConfirmDialogComponent,{
      width:"45rem",
      data:{
        message:message
      }
    });
    dialogRef.afterClosed().subscribe(result=>{
      if(result){
        const model:ICurrency={
          ...currency,
          selected:currency.selected?false:true
        }
        this.currencyStore.update(model);
      }
    })
  }

}
