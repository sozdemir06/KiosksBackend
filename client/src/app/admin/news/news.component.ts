import { Component, OnInit, ViewChild, ElementRef, OnDestroy } from '@angular/core';
import { NewsStore } from 'src/app/core/services/stores/news-store';
import { PageEvent } from '@angular/material/paginator';
import { fromEvent, Observable, Subscription } from 'rxjs';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { map, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { EditNewsDialogComponent } from './edit-news-dialog/edit-news-dialog.component';
import { NewsParams } from 'src/app/shared/models/NewsParams';
import { MatDialog } from '@angular/material/dialog';
import { SubScreenStore } from 'src/app/core/services/stores/subscreen-store';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.scss']
})
export class NewsComponent implements OnInit,OnDestroy {
  unSubsCribeFromSearchInput:Subscription=Subscription.EMPTY;
  @ViewChild('searchInput') searchInput: ElementRef;
  roleForCreate:string[]=['Sudo','News.Create,News.All']
  
  constructor(
    public newsStore:NewsStore,
    private dialog:MatDialog,
    public subScreenStore:SubScreenStore
  ) { }

  ngOnInit(): void {
  }



  ngAfterViewInit() {
    this.unSubsCribeFromSearchInput = fromEvent<any>(
      this.searchInput.nativeElement,
      'keyup'
    )
      .pipe(
        map((event) => event.target.value),
        debounceTime(400),
        distinctUntilChanged()
      )
      .subscribe((result) => {
        const params = this.newsStore.getParams();
        params.search = result;
        this.newsStore.setParams(params);
        this.newsStore.getListByParams();
      });
  }

  onPageChange(event:PageEvent){
     const params=this.newsStore.getParams();
     params.pageSize = event.pageSize;
     params.pageIndex = event.pageIndex + 1;
     this.newsStore.setParams(params);
     this.newsStore.getListByParams();

  }

  onWaitingForConfirm(){
    const params = this.newsStore.getParams();
    params.isNew=true;
    this.newsStore.setParams(params);
    this.newsStore.getListByParams();
  }

  onCreateNew(){
    this.dialog.open(EditNewsDialogComponent,{
      width:"60vw",
      maxHeight:"100vh",
      autoFocus:false,
      data:{
        title:"Yeni Haber  ekle",
        mode:"create",
        item:null
      }
    })
  }

  onReset(){
    const params = new NewsParams();
    this.newsStore.setParams(params);
    this.newsStore.getListByParams();
  }

  filterBySubScreen(id:number){
    const params = this.newsStore.getParams();
    params.subScreenId =id;
    this.newsStore.setParams(params);
    this.newsStore.getListByParams();
  }

  ngOnDestroy(){
    this.unSubsCribeFromSearchInput.unsubscribe();
  }

}
