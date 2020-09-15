import {
  Component,
  OnInit,
  ViewChild,
  ElementRef,
  AfterViewInit,
  OnDestroy,
} from '@angular/core';
import { VehicleBrandStore } from 'src/app/core/services/stores/vehicle-brand-store';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { EditVehicleBrandsDialogComponent } from './edit-vehicle-brands-dialog/edit-vehicle-brands-dialog.component';
import { fromEvent } from 'rxjs';
import { map, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { VehicleCategoryStore } from 'src/app/core/services/stores/vehicle-category-store';
import { IVehicleCategory } from 'src/app/shared/models/IVehicleCategory';
import { VehicleBrandParams } from 'src/app/shared/models/VehicleBrandParams';

@Component({
  selector: 'app-vehicle-brands',
  templateUrl: './vehicle-brands.component.html',
  styleUrls: ['./vehicle-brands.component.scss'],
})
export class VehicleBrandsComponent
  implements OnInit, AfterViewInit, OnDestroy {
  toolbarTitle: string = 'Araç Markaları';
  toolbarSearchPlaceholderText: string =
    'Marka adı ile arama yapabilirsiniz...';
  allowedRolesForCreate:string[]=["Sudo","VehicleAnnounceOptions.All"];
  

  unSubscribeSearchInputEvent: any;

  @ViewChild('searchInput') Input: ElementRef;

  constructor(
    public vehicleBrandStore: VehicleBrandStore,
    public vehicleCategoryStore: VehicleCategoryStore,
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
        const params = this.vehicleBrandStore.getVehicleBrandParams();
        params.search = result;
        params.pageIndex = 1;
        this.vehicleBrandStore.setVehicleBrandParams(params);
        this.vehicleBrandStore.getVehicleBrandList();
      });
  }

  onPageChange(event: PageEvent) {
    const params = this.vehicleBrandStore.getVehicleBrandParams();
    params.pageIndex = event.pageIndex + 1;
    params.pageSize = event.pageSize;
    this.vehicleBrandStore.setVehicleBrandParams(params);
    this.vehicleBrandStore.getVehicleBrandList();
  }

  onCreate() {
    this.dialog.open(EditVehicleBrandsDialogComponent, {
      width: '45rem',
      maxHeight: '100vh',
      data: {
        title: 'Yeni araç markası ekle',
        mode: 'create',
        item: null,
      },
    });
  }

  filters(item: IVehicleCategory) {
    const params = this.vehicleBrandStore.getVehicleBrandParams();
    params.vehicleCategoryId = item.id;
    this.vehicleBrandStore.setVehicleBrandParams(params);
    this.vehicleBrandStore.getVehicleBrandList();
  }

  onReset(){
    const params=new VehicleBrandParams();
    this.vehicleBrandStore.setVehicleBrandParams(params);
    this.vehicleBrandStore.getVehicleBrandList();
  }

  ngOnDestroy() {
    this.unSubscribeSearchInputEvent.unsubscribe();
  }
}
