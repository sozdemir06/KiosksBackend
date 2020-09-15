import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { DegreeStore } from 'src/app/core/services/stores/degree-store';
import { DegreeParams } from 'src/app/shared/models/DegreeParams';
import { EditDegreeDialogComponent } from './edit-degree-dialog/edit-degree-dialog.component';

@Component({
  selector: 'app-degree',
  templateUrl: './degree.component.html',
  styleUrls: ['./degree.component.scss']
})
export class DegreeComponent implements OnInit {
  toolbarTitle: string = 'Ünvan Listesi';
  toolbarSearchPlaceholderText: string =
    'Ünvan adı ile arama...';
  allowedRolesForCreate:string[]=["Sudo","UserOptions.All"];
  

  unSubscribeSearchInputEvent: any;

  @ViewChild('searchInput') Input: ElementRef;
  vehicleBrandStore: any;

  constructor(
    public degreeStore: DegreeStore,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {}
  ngAfterViewInit() {
    this.unSubscribeSearchInputEvent = fromEvent<any>(
      this.Input.nativeElement,
      'keyup'
    )
      .pipe(
        map((event) => event.target.value),
        debounceTime(400),
        distinctUntilChanged()
      )
      .subscribe((result) => {
        const params = this.degreeStore.getParams();
        params.search = result;
        params.pageIndex = 1;
        this.degreeStore.setParams(params);
        this.degreeStore.getListWithParams();
      });
  }

  onPageChange(event: PageEvent) {
    const params = this.degreeStore.getParams();
    params.pageIndex = event.pageIndex + 1;
    params.pageSize = event.pageSize;
    this.degreeStore.setParams(params);
    this.degreeStore.getListWithParams();
  }

  onCreate() {
    this.dialog.open(EditDegreeDialogComponent, {
      width: '45rem',
      maxHeight: '100vh',
      data: {
        title: 'Yeni ünvan Ekle',
        mode: 'create',
        item: null,
      },
    });
  }

 

  onReset(){
    const params=new DegreeParams();
    this.degreeStore.setParams(params);
    this.degreeStore.getListWithParams();
  }

  ngOnDestroy() {
    this.unSubscribeSearchInputEvent.unsubscribe();
  }

}
