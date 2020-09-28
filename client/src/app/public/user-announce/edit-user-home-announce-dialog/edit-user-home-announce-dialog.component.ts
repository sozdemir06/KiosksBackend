import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { combineLatest, Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { IHomeAnnounceOptinsSelect } from 'src/app/admin/home-announces/edit-home-announce-dialog/edit-home-announce-dialog.component';
import { BuildingAgeStore } from 'src/app/core/services/stores/building-age-store';
import { FlatOfHomeStore } from 'src/app/core/services/stores/flat-of-home-store';
import { HeatingTypeStore } from 'src/app/core/services/stores/heating-type-store';
import { NumberOfroomStore } from 'src/app/core/services/stores/number-of-rrom-store';
import { IUser } from 'src/app/shared/models/IUser';
import { IHomeAnnounceForPublic } from '../../models/IHomeAnnounceForPublic';
import { UserHomeAnnounceStore } from '../../store/user-home-announce-store';

@Component({
  selector: 'app-edit-user-home-announce-dialog',
  templateUrl: './edit-user-home-announce-dialog.component.html',
  styleUrls: ['./edit-user-home-announce-dialog.component.scss'],
})
export class EditUserHomeAnnounceDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  item: IHomeAnnounceForPublic;

  homeAnnounceForm: FormGroup;
  data$: Observable<IHomeAnnounceOptinsSelect>;
  user:IUser;


  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private dialogRef: MatDialogRef<EditUserHomeAnnounceDialogComponent>,
    private userHomeAnnounceStore: UserHomeAnnounceStore,
    private fb: FormBuilder,
    private numberOfRomeStore: NumberOfroomStore,
    private buildingAgeStore: BuildingAgeStore,
    private heatingTypeStore: HeatingTypeStore,
    private flatOfHomeStore: FlatOfHomeStore
  ) {
    this.title = data?.title;
    this.mode = data?.mode;
    this.item = data?.item;
    this.user=data?.user;

    const formControls = {
      header: ['', [Validators.required, Validators.maxLength(140)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],

      numberOfRoomId: ['', Validators.required],
      heatingTypeId: ['', Validators.required],
      flatOfHomeId: ['', Validators.required],
      buildingAgeId: ['', Validators.required],
      squareMeters:['',Validators.required],
      price: ['', Validators.required],
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

  onSubmit() {
    if (this.homeAnnounceForm.valid) {
      if (this.mode == 'create') {
        const model: IHomeAnnounceForPublic = {
          ...this.homeAnnounceForm.value,
          price: parseInt(this.homeAnnounceForm.get('price').value),
          userId:this.user?.userId
        };

        this.userHomeAnnounceStore.create(model,this.user?.userId);
        this.dialogRef.close();
      } else if (this.mode == 'update') {
        const model: IHomeAnnounceForPublic = {
          ...this.item,
          ...this.homeAnnounceForm.value,
        };
        this.userHomeAnnounceStore.update(model,this.user?.userId);
        this.dialogRef.close();
      }
    }
  }
}
