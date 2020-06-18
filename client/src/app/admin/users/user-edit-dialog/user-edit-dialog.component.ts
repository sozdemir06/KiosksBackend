import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { IUserList } from 'src/app/shared/models/IUser';
import { ErrorMessagesService } from 'src/app/core/services/error-messages.service';
import { MustMatch } from 'src/app/shared/helpers/password-match';
import { DepartmentStore } from 'src/app/core/services/department-store';
import { CampusStore } from 'src/app/core/services/campus-store';
import { DegreeStore } from 'src/app/core/services/degree-store';
import { IDepartment } from 'src/app/shared/models/IDepartment';
import { ICampus } from 'src/app/shared/models/ICampus';
import { IDegree } from 'src/app/shared/models/IDegree';
import { Observable, combineLatest } from 'rxjs';
import { startWith, map, tap } from 'rxjs/operators';
import { UserStore } from 'src/app/core/services/user-store';

export interface ISelectOptionData {
  departments: IDepartment[];
  campuses: ICampus[];
  degrees: IDegree[];
}
@Component({
  selector: 'app-user-edit-dialog',
  templateUrl: './user-edit-dialog.component.html',
  styleUrls: ['./user-edit-dialog.component.scss'],
})
export class UserEditDialogComponent implements OnInit {
  title: string;
  mode: 'create' | 'update';
  user: IUserList;

  loginForm: FormGroup;

  data$: Observable<ISelectOptionData>;

  constructor(
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) private data: any,
    private dialogRef: MatDialogRef<UserEditDialogComponent>,
    private departmentStore: DepartmentStore,
    private campuseStore: CampusStore,
    private degreeStore: DegreeStore,
    private userStore: UserStore,
  ) {
    this.title = data.title;
    this.mode = data.mode;
    this.user = data.user;

    const formControls = {
      firstName: ['', [Validators.required, Validators.maxLength(50)]],
      lastName: ['', [Validators.required, Validators.maxLength(50)]],
      email: ['', [Validators.required, Validators.email]],
      interPhone: ['', [Validators.maxLength(11)]],
      gsmPhone: ['', [Validators.maxLength(11)]],
      campusId: ['', Validators.required],
      departmentId: ['', Validators.required],
      degreeId: ['', Validators.required],
      password: [
        '',
        [Validators.required, Validators.minLength(4), Validators.maxLength(8)],
      ],
      passwordConfirm: [
        '',
        [Validators.required, Validators.minLength(4), Validators.maxLength(8)],
      ],
    };

    if (this.mode == 'create') {
      this.loginForm = this.fb.group(
        {
          ...formControls,
        },
        {
          validator: MustMatch('password', 'passwordConfirm'),
        }
      );
    }
  }

  get f() {
    return this.loginForm.controls;
  }

  ngOnInit(): void {
    const departments$ = this.departmentStore.departments$.pipe(startWith([]));
    const campuses$ = this.campuseStore.campus$.pipe(startWith([]));
    const degrees$ = this.degreeStore.degrees$.pipe(startWith([]));

    this.data$ = combineLatest([departments$, campuses$, degrees$]).pipe(
      map(([departments, campuses, degrees]) => {
        return {
          departments,
          campuses,
          degrees,
        };
      })
    );
  }

  onSubmit() {
    if (this.mode === 'create') {
      if (this.loginForm.valid) {
        this.userStore.create(this.loginForm.value);
      }
      
    }
  }
}
