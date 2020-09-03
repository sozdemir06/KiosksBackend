import { Component, OnInit, Inject } from '@angular/core';
import { IVehicleAnnounceList } from 'src/app/shared/models/IVehicleAnnounceList';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { VehilceAnnounceStore } from 'src/app/core/services/stores/vehicle-announce-store';
import { HelperService } from 'src/app/core/services/helper-service';
import { UserStore } from 'src/app/core/services/stores/user-store';
import { IUserList } from 'src/app/shared/models/IUser';
import { VehicleCategoryStore } from 'src/app/core/services/stores/vehicle-category-store';
import { VehicleGearTypeStore } from 'src/app/core/services/stores/vehicle-gear-type-store';
import { VehicleFuelTypeStore } from 'src/app/core/services/stores/vehicle-fuel-type-store';
import { VehicleEngineSizeStore } from 'src/app/core/services/stores/vehicle-engine-size-store';
import { IVehicleGearType } from 'src/app/shared/models/IVehicleGearType';
import { IVehicleFuelType } from 'src/app/shared/models/IVehicleFuelType';
import { IVehicleEngineSize } from 'src/app/shared/models/IVehicleEngineSize';
import { Observable, combineLatest } from 'rxjs';
import { startWith, map,tap} from 'rxjs/operators';
import { IVehicleBrand } from 'src/app/shared/models/IVehicleBrand';
import { IVehicleModel } from 'src/app/shared/models/IVehicleModel';
import { VehicleBrandStore } from 'src/app/core/services/stores/vehicle-brand-store';
import { VehicleModelStore } from 'src/app/core/services/stores/vehicle-model-store';

export interface IOptionsData {
  vehicleGearType: IVehicleGearType[];
  vehicleFuelType: IVehicleFuelType[];
  vehicleEngineSize: IVehicleEngineSize[];
}

@Component({
  selector: 'app-edit-vehicle-announce-dialog',
  templateUrl: './edit-vehicle-announce-dialog.component.html',
  styleUrls: ['./edit-vehicle-announce-dialog.component.scss'],
})
export class EditVehicleAnnounceDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  item: IVehicleAnnounceList;

  vehicleAnnounceForm: FormGroup;

  data$: Observable<IOptionsData>;
  brands$: Observable<IVehicleBrand[]>;
  models$: Observable<IVehicleModel[]>;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditVehicleAnnounceDialogComponent>,
    private vehicleAnnounceStore: VehilceAnnounceStore,
    private fb: FormBuilder,
    private helperService: HelperService,
    public userStore: UserStore,
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

    const formControls = {
      header: ['', [Validators.required, Validators.maxLength(140)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      publishStartDate: [
        this.helperService.dateToLocaleFormat(new Date()),
        Validators.required,
      ],
      publishFinishDate: [
        this.helperService.dateToLocaleFormat(new Date()),
        Validators.required,
      ],
      vehicleCategoryId: ['', Validators.required],
      vehicleBrandId: ['', Validators.required],
      vehicleModelId: ['', Validators.required],
      vehicleFuelTypeId: ['', Validators.required],
      vehicleGearTypeId: ['', Validators.required],
      vehicleEngineSizeId: ['', Validators.required],
      slideIntervalTime:[8,Validators.required],
      price: ['', Validators.required],
      userId: ['', Validators.required],
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

  onUserSelectionChange(user: IUserList) {
    this.vehicleAnnounceForm.patchValue({
      userId: user.id,
    });
  }
  onChangeStartDate(event) {
    this.vehicleAnnounceForm.patchValue({
      publishStartDate: this.helperService.dateToLocaleFormat(event.value),
    });
  }

  onChangeFinishDate(event) {
    this.vehicleAnnounceForm.patchValue({
      publishFinishDate: this.helperService.dateToLocaleFormat(event.value),
    });
  }

  onSubmit() {
    if (this.vehicleAnnounceForm.valid) {
      const startDate = this.vehicleAnnounceForm.get('publishStartDate').value;
      const finishDate = this.vehicleAnnounceForm.get('publishFinishDate')
        .value;

      const checkDate = this.helperService.checkPublishDate(
        startDate,
        finishDate
      );
      if (checkDate) {
        if (this.mode == 'create') {
          const model: IVehicleAnnounceList = {
            ...this.vehicleAnnounceForm.value,
            price: parseInt(this.vehicleAnnounceForm.get('price').value),
            isNew: true,
            isPublish: false,
            reject: false,
          };

          this.vehicleAnnounceStore.create(model);
          this.dialogRef.close();
        }else if(this.mode=="update"){
          const model:IVehicleAnnounceList={
            ...this.item,
            ...this.vehicleAnnounceForm.value,
          };
          this.vehicleAnnounceStore.update(model);
          this.dialogRef.close();
        }
      }
    }
  }
}
