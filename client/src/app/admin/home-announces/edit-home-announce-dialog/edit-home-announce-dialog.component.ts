import { Component, OnInit, Inject } from '@angular/core';
import { IHomeAnnounce } from 'src/app/shared/models/IHomeAnnounce';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { HomeAnnounceStore } from 'src/app/core/services/stores/home-announce-store';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { INumberOfRoom } from 'src/app/shared/models/INumberOFRoom';
import { IBuildingAge } from 'src/app/shared/models/IBuildingAge';
import { IHeatingType } from 'src/app/shared/models/IHeatingType';
import { IFlatOfHome } from 'src/app/shared/models/IFlatOfHome';
import { Observable, combineLatest } from 'rxjs';
import { NumberOfroomStore } from 'src/app/core/services/stores/number-of-rrom-store';
import { BuildingAgeStore } from 'src/app/core/services/stores/building-age-store';
import { HeatingTypeStore } from 'src/app/core/services/stores/heating-type-store';
import { FlatOfHomeStore } from 'src/app/core/services/stores/flat-of-home-store';
import { startWith, map } from 'rxjs/operators';
import { HelperService } from 'src/app/core/services/helper-service';
import { UserStore } from 'src/app/core/services/stores/user-store';
import { IUserList } from 'src/app/shared/models/IUser';

export interface IHomeAnnounceOptinsSelect {
  numberOfRooms: INumberOfRoom[];
  buildingAges: IBuildingAge[];
  heatingTypes: IHeatingType[];
  flatOfHomes: IFlatOfHome[];
}

@Component({
  selector: 'app-edit-home-announce-dialog',
  templateUrl: './edit-home-announce-dialog.component.html',
  styleUrls: ['./edit-home-announce-dialog.component.scss'],
})
export class EditHomeAnnounceDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  item: IHomeAnnounce;

  homeAnnounceForm: FormGroup;
  data$: Observable<IHomeAnnounceOptinsSelect>;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditHomeAnnounceDialogComponent>,
    private homeAnnounceStore: HomeAnnounceStore,
    private fb: FormBuilder,
    private numberOfRomeStore: NumberOfroomStore,
    private buildingAgeStore: BuildingAgeStore,
    private heatingTypeStore: HeatingTypeStore,
    private flatOfHomeStore: FlatOfHomeStore,
    private helperService: HelperService,
    public userStore: UserStore
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
      numberOfRoomId: ['', Validators.required],
      heatingTypeId: ['', Validators.required],
      flatOfHomeId: ['', Validators.required],
      buildingAgeId: ['', Validators.required],
      price: ['', Validators.required],
      squareMeters: ['', Validators.required],
      userId: ['', Validators.required],
    };

    if (this.mode == 'create') {
      this.homeAnnounceForm = this.fb.group({ ...formControls });
    } else if (this.mode == 'update') {
      this.homeAnnounceForm = this.fb.group(formControls);
      this.homeAnnounceForm.patchValue({
        ...this.item,
        userId: this.item?.user?.id,
      });
    }
  }

  get f() {
    return this.homeAnnounceForm.controls;
  }

  onUserSelectionChange(user: IUserList) {
    this.homeAnnounceForm.patchValue({
      userId: user.id,
    });
  }

  ngOnInit(): void {
    const numberOfRooms$ = this.numberOfRomeStore.numberOfRooms$.pipe(
      startWith([])
    );
    const buildingAges$ = this.buildingAgeStore.buildingsAge$.pipe(
      startWith([])
    );
    const flatOfHomes$ = this.flatOfHomeStore.flatsofhome$.pipe(startWith([]));
    const heatingTypes$ = this.heatingTypeStore.heatingTypes$.pipe(
      startWith([])
    );

    this.data$ = combineLatest([
      numberOfRooms$,
      buildingAges$,
      flatOfHomes$,
      heatingTypes$,
    ]).pipe(
      map(([numberOfRooms, buildingAges, flatOfHomes, heatingTypes]) => {
        return {
          numberOfRooms,
          buildingAges,
          flatOfHomes,
          heatingTypes,
        };
      })
    );
  }

  onChangeStartDate(event) {
    this.homeAnnounceForm.patchValue({
      publishStartDate: this.helperService.dateToLocaleFormat(event.value),
    });
  }

  onChangeFinishDate(event) {
    this.homeAnnounceForm.patchValue({
      publishFinishDate: this.helperService.dateToLocaleFormat(event.value),
    });
  }

  onSubmit() {
    if (this.homeAnnounceForm.valid) {
      const startDate: Date = this.homeAnnounceForm.get('publishStartDate')
        .value;
      const finishDate: Date = this.homeAnnounceForm.get('publishFinishDate')
        .value;
      const checkDate = this.helperService.checkPublishDate(
        startDate,
        finishDate
      );
      if (checkDate) {
        if (this.mode == 'create') {
          const model: IHomeAnnounce = {
            ...this.homeAnnounceForm.value,
            price: parseInt(this.homeAnnounceForm.get('price').value),
            squareMeters: parseInt(this.homeAnnounceForm.get('squareMeters').value),
            isNew: true,
            isPublish: false,
            reject: false,
          };

          this.homeAnnounceStore.create(model);
          this.dialogRef.close();
          console.log(model);
        }
      }
    }
  }
}
