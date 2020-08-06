import { Component, OnInit, ElementRef, ViewChild, AfterViewInit, OnDestroy } from '@angular/core';
import { VehilceAnnounceStore } from 'src/app/core/services/stores/vehicle-announce-store';
import { PageEvent } from '@angular/material/paginator';
import { SubScreenStore } from 'src/app/core/services/stores/subscreen-store';
import { MatDialog } from '@angular/material/dialog';
import { EditVehicleAnnounceDialogComponent } from './edit-vehicle-announce-dialog/edit-vehicle-announce-dialog.component';
import { fromEvent } from 'rxjs';
import { VehicleAnnounceParams } from 'src/app/shared/models/VehicleAnnounceParams';
import { map, debounceTime, distinctUntilChanged } from 'rxjs/operators';



@Component({
  selector: 'app-vehicle-announce',
  templateUrl: './vehicle-announce.component.html',
  styleUrls: ['./vehicle-announce.component.scss']
})
export class VehicleAnnounceComponent implements OnInit,AfterViewInit,OnDestroy {
  @ViewChild('searchInput') searchInput: ElementRef;
  unSubsCribeFromSearchInput: any;
  roleForCreate:string[]=['Sudo','VehicleAnnounces.Create,VehicleAnnounces.All']




  constructor(
    public vehicleStore:VehilceAnnounceStore,
    public subscreenstore:SubScreenStore,
    private dialog:MatDialog,

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
        const params = this.vehicleStore.getParams();
        params.search = result;
        this.vehicleStore.setParams(params);
        this.vehicleStore.getListByParams();
      });
  }

  onPageChange(event:PageEvent){
     const params=this.vehicleStore.getParams();
     params.pageSize = event.pageSize;
     params.pageIndex = event.pageIndex + 1;
     this.vehicleStore.setParams(params);
     this.vehicleStore.getListByParams();

  }

  onWaitingForConfirm(){
    const params = this.vehicleStore.getParams();
    params.isNew=true;
    this.vehicleStore.setParams(params);
    this.vehicleStore.getListByParams();
  }

  onCreateNew(){
    this.dialog.open(EditVehicleAnnounceDialogComponent,{
      width:"55rem",
      maxHeight:"100vh",
      autoFocus:false,
      data:{
        title:"Yeni araç ilanı ekle",
        mode:"create",
        item:null
      }
    })
  }

  onReset(){
    const params = new VehicleAnnounceParams();
    this.vehicleStore.setParams(params);
    this.vehicleStore.getListByParams();
  }

  filterBySubScreen(id:number){
    const params = this.vehicleStore.getParams();
    params.subScreenId =id;
    this.vehicleStore.setParams(params);
    this.vehicleStore.getListByParams();
  }

  ngOnDestroy(){
    this.unSubsCribeFromSearchInput?.unsubscribe();
  }



}
