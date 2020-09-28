import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { combineLatest, Observable } from 'rxjs';
import { map, startWith, tap } from 'rxjs/operators';
import { IHomeAnnounceOptinsSelect } from 'src/app/admin/home-announces/edit-home-announce-dialog/edit-home-announce-dialog.component';
import { EditVehicleAnnounceDialogComponent } from 'src/app/admin/vehicle-announces/edit-vehicle-announce-dialog/edit-vehicle-announce-dialog.component';
import { VehicleBrandStore } from 'src/app/core/services/stores/vehicle-brand-store';
import { VehicleCategoryStore } from 'src/app/core/services/stores/vehicle-category-store';
import { VehicleEngineSizeStore } from 'src/app/core/services/stores/vehicle-engine-size-store';
import { VehicleFuelTypeStore } from 'src/app/core/services/stores/vehicle-fuel-type-store';
import { VehicleGearTypeStore } from 'src/app/core/services/stores/vehicle-gear-type-store';
import { VehicleModelStore } from 'src/app/core/services/stores/vehicle-model-store';
import { IUser } from 'src/app/shared/models/IUser';
import { IVehicleBrand } from 'src/app/shared/models/IVehicleBrand';
import { IVehicleEngineSize } from 'src/app/shared/models/IVehicleEngineSize';
import { IVehicleFuelType } from 'src/app/shared/models/IVehicleFuelType';
import { IVehicleGearType } from 'src/app/shared/models/IVehicleGearType';
import { IVehicleModel } from 'src/app/shared/models/IVehicleModel';
import { IVehicleAnnounceForPublic } from '../../models/IVehicleAnnounceForPublic';
import { UserVehicleAnnounceStore } from '../../store/user-vehicle-announce-store';
export interface IOptionsData {
  vehicleGearType: IVehicleGearType[];
  vehicleFuelType: IVehicleFuelType[];
  vehicleEngineSize: IVehicleEngineSize[];
}

@Component({
  selector: 'app-edit-user-vehicle-announce-dialog',
  templateUrl: './edit-user-vehicle-announce-dialog.component.html',
  styleUrls: ['./edit-user-vehicle-announce-dialog.component.scss'],
})
export class EditUserVehicleAnnounceDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  item: IVehicleAnnounceForPublic;

  vehicleAnnounceForm: FormGroup;

  data$: Observable<IOptionsData>;
  brands$: Observable<IVehicleBrand[]>;
  models$: Observable<IVehicleModel[]>;
  user: IUser;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditVehicleAnnounceDialogComponent>,
    private userVehicleAnnounceStore: UserVehicleAnnounceStore,
    private fb: FormBuilder,
    public vehicleCategoryStore: VehicleCategoryStore,
    private vehicleGearTypeStore: VehicleGearTypeStore,
    private vehicleFuelTypeStore: VehicleFuelTypeStore,
    private vehicleEngineSizeStore: VehicleEngineSizeStore,
    private vehicleBrandStore: VehicleBrandStore,
    private vehicleModelStore: VehicleModelStore
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data?.item;
    this.user = data?.user;

    const formControls = {
      header: ['', [Validators.required, Validators.maxLength(140)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      vehicleCategoryId: ['', Validators.required],
      vehicleBrandId: ['', Validators.required],
      vehicleModelId: ['', Validators.required],
      vehicleFuelTypeId: ['', Validators.required],
      vehicleGearTypeId: ['', Validators.required],
      vehicleEngineSizeId: ['', Validators.required],
      price: ['', Validators.required],
    };

    if (this.mode == 'create') {
      this.vehicleAnnounceForm = this.fb.group({
        ...formControls,
      });
    } else if (this.mode == 'update') {
      this.vehicleAnnounceForm = this.fb.group(formControls);
      this.vehicleAnnounceForm.patchValue({
        ...this.item,
        userId: this.item?.userId,
        vehicleBrandId: this.item?.vehicleBrandId,
        vehicleModelId: this.item?.vehicleModelId,
      });
    }
  }

  get f() {
    return this.vehicleAnnounceForm.controls;
  }

  ngOnInit(): void {
    const vehicleGearTypes$ = this.vehicleGearTypeStore.vehicleGearTypes$.pipe(
      startWith([])
    );
    const vehicleEngineSizes$ = this.vehicleEngineSizeStore.vehicleEngineSizes$.pipe(
      startWith([])
    );
    const vehicleFuelTypes$ = this.vehicleFuelTypeStore.vehicleFuelTypes$.pipe(
      startWith([])
    );

    this.data$ = combineLatest([
      vehicleGearTypes$,
      vehicleFuelTypes$,
      vehicleEngineSizes$,
    ]).pipe(
      map(([vehicleGearType, vehicleFuelType, vehicleEngineSize]) => {
        return {
          vehicleGearType,
          vehicleFuelType,
          vehicleEngineSize,
        };
      })
    );
    if (this.mode == 'update') {
      this.brands$ = this.vehicleBrandStore.getByCategory(
        this.item.vehicleCategoryId
      );
      this.models$ = this.vehicleModelStore.getByBrand(
        this.item?.vehicleBrandId
      );
    }
    this.vehicleAnnounceForm
      .get('vehicleCategoryId')
      .valueChanges.pipe(
        map((val) => val),
        tap((value) => {
          this.brands$ = this.vehicleBrandStore.getByCategory(+value);
        })
      )
      .subscribe();

    this.vehicleAnnounceForm
      .get('vehicleBrandId')
      .valueChanges.pipe(
        map((val) => val),
        tap((value) => {
          this.models$ = this.vehicleModelStore.getByBrand(+value);
        })
      )
      .subscribe();
  }

  onSubmit() {
    if (this.vehicleAnnounceForm.valid) {
      if (this.mode == 'create') {
        const model: IVehicleAnnounceForPublic = {
          ...this.vehicleAnnounceForm.value,
          price: parseInt(this.vehicleAnnounceForm.get('price').value),
          userId: this.user?.userId,
        };

        this.userVehicleAnnounceStore.create(model, this.user?.userId);
        this.dialogRef.close();
      } else if (this.mode == 'update') {
        const model: IVehicleAnnounceForPublic = {
          ...this.item,
          ...this.vehicleAnnounceForm.value,
        };
        this.userVehicleAnnounceStore.update(model, this.user?.userId);
        this.dialogRef.close();
      }
    }
  }
}
