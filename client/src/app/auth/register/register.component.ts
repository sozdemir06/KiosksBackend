import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { combineLatest, Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { CampusStore } from 'src/app/core/services/stores/campus-store';
import { DegreeStore } from 'src/app/core/services/stores/degree-store';
import { DepartmentStore } from 'src/app/core/services/stores/department-store';
import { UserStore } from 'src/app/core/services/stores/user-store';
import { MustMatch } from 'src/app/shared/helpers/password-match';
import { ICampus } from 'src/app/shared/models/ICampus';
import { IDegree } from 'src/app/shared/models/IDegree';
import { IDepartment } from 'src/app/shared/models/IDepartment';
import { IUser, IUserList } from 'src/app/shared/models/IUser';
export interface ISelectOptionData {
  departments: IDepartment[];
  campuses: ICampus[];
  degrees: IDegree[];
}
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  data$: Observable<ISelectOptionData>;
  constructor(
    private fb: FormBuilder,
    private departmentStore: DepartmentStore,
    private campuseStore: CampusStore,
    private degreeStore: DegreeStore,
    private userStore: UserStore
  ) {}

  ngOnInit(): void {
    this.checkRegisterForm();
    const departments$ = this.departmentStore.departments$.pipe(startWith([]));
    const campuses$ = this.campuseStore.campus$.pipe(startWith([]));
    const degrees$ = this.degreeStore.getDegreeList().pipe(startWith([]));

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

  checkRegisterForm() {
    this.registerForm = this.fb.group(
      {
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
          [
            Validators.required,
            Validators.minLength(4),
            Validators.maxLength(8),
          ],
        ],
        passwordConfirm: [
          '',
          [
            Validators.required,
            Validators.minLength(4),
            Validators.maxLength(8),
          ],
        ],
      },
      {
        validator: MustMatch('password', 'passwordConfirm'),
      }
    );
  }
  get f() {
    return this.registerForm.controls;
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const model: IUserList = {
        ...this.registerForm.value,
        isActive: false,
      };
      this.userStore.create(model);
      this.registerForm.reset();
    }
  }
}
