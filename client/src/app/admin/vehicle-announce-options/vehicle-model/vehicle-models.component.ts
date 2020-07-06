import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { VehicleModelStore } from 'src/app/core/services/stores/vehicle-model-store';
import { EditVehicleModelDialogComponent } from './edit-vehicle-model-dialog/edit-vehicle-model-dialog.component';
import { IVehicleModel } from 'src/app/shared/models/IVehicleModel';
import { VehicleModelParams } from 'src/app/shared/models/VehicleModelParams';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { fromEvent } from 'rxjs';
import { map, debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-vehicle-models',
  templateUrl: './vehicle-models.component.html',
  styleUrls: ['./vehicle-models.component.scss'],
})
export class VehicleModelsComponent implements OnInit {
  toolbarTitle: string = 'Araç Markaları';
  toolbarSearchPlaceholderText: string =
    'Marka adı ile arama yapabilirsiniz...';
  allowedRolesForCreate: string[] = ['Sudo', 'VehicleModels.Create'];

  unSubscribeSearchInputEvent: any;
  @ViewChild('searchInput') Input: ElementRef;

  constructor(
    public vehicleModelStore: VehicleModelStore,
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
        const params = this.vehicleModelStore.getVehicleModelParams();
        params.search = result;
        params.pageIndex = 1;
        this.vehicleModelStore.setVehicleModelParams(params);
        this.vehicleModelStore.getVehicleModelList();
      });
  }

  onPageChange(event: PageEvent) {
    const params = this.vehicleModelStore.getVehicleModelParams();
    params.pageIndex = event.pageIndex + 1;
    params.pageSize = event.pageSize;
    this.vehicleModelStore.setVehicleModelParams(params);
    this.vehicleModelStore.getVehicleModelList();
  }

  onCreate() {
    this.dialog.open(EditVehicleModelDialogComponent, {
      width: '45rem',
      maxHeight: '100vh',
      data: {
        title: 'Yeni araç markası ekle',
        mode: 'create',
        item: null,
      },
    });
  }

  filters(item: IVehicleModel) {
    const params = this.vehicleModelStore.getVehicleModelParams();
    params.vehicleCategoryId = item.id;
    this.vehicleModelStore.setVehicleModelParams(params);
    this.vehicleModelStore.getVehicleModelList();
  }

  onReset() {
    const params = new VehicleModelParams();
    this.vehicleModelStore.setVehicleModelParams(params);
    this.vehicleModelStore.getVehicleModelList();
  }

  ngOnDestroy() {
    this.unSubscribeSearchInputEvent.unsubscribe();
  }
}
