import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FoodMenuParams } from 'src/app/shared/models/FoodMenuParams';
import { EditFoodMenuDialogComponent } from './edit-food-menu-dialog/edit-food-menu-dialog.component';
import { PageEvent } from '@angular/material/paginator';
import { fromEvent, Subscription, Observable } from 'rxjs';
import { map, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { FoodMenuStore } from 'src/app/core/services/stores/food-menu-store';
import { SubScreenStore } from 'src/app/core/services/stores/subscreen-store';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-food-menu',
  templateUrl: './food-menu.component.html',
  styleUrls: ['./food-menu.component.scss']
})
export class FoodMenuComponent implements OnInit {

  @ViewChild('searchInput') searchInput: ElementRef;
  subscription:Subscription=Subscription.EMPTY;

  roleForCreate:string[]=['Sudo','FoodMenu.Create,FoodMenu.All']
  subscreens$:Observable<ISubScreen[]>;



  constructor(
    public foodMenuStore:FoodMenuStore,
    public subScreenStore:SubScreenStore,
    private dialog:MatDialog,

  ) { }

  ngOnInit(): void {
    
  }

  ngAfterViewInit() {
    this.subscription = fromEvent<any>(
      this.searchInput.nativeElement,
      'keyup'
    )
      .pipe(
        map((event) => event.target.value),
        debounceTime(400),
        distinctUntilChanged()
      )
      .subscribe((result) => {
        const params = this.foodMenuStore.getParams();
        params.search = result;
        this.foodMenuStore.setParams(params);
        this.foodMenuStore.getListByParams();
      });
  }

  onPageChange(event:PageEvent){
     const params=this.foodMenuStore.getParams();
     params.pageSize = event.pageSize;
     params.pageIndex = event.pageIndex + 1;
     this.foodMenuStore.setParams(params);
     this.foodMenuStore.getListByParams();

  }

  onWaitingForConfirm(){
    const params = this.foodMenuStore.getParams();
    params.isNew=true;
    this.foodMenuStore.setParams(params);
    this.foodMenuStore.getListByParams();
  }

  onCreateNew(){
    this.dialog.open(EditFoodMenuDialogComponent,{
      width:"60vw",
      maxHeight:"100vh",
      autoFocus:false,
      data:{
        title:"Yeni Menü  ekle",
        mode:"create",
        item:null
      }
    })
  }

  onReset(){
    const params = new FoodMenuParams();
    this.foodMenuStore.setParams(params);
    this.foodMenuStore.getListByParams();
  }

  filterBySubScreen(id:number){
    const params = this.foodMenuStore.getParams();
    params.subScreenId =id;
    this.foodMenuStore.setParams(params);
    this.foodMenuStore.getListByParams();
  }

  ngOnDestroy(){
    this.subscription.unsubscribe();
  }


}
