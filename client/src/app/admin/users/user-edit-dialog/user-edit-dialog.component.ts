import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { IUserList } from 'src/app/shared/models/IUser';
import { MustMatch } from 'src/app/shared/helpers/password-match';
import { IDepartment } from 'src/app/shared/models/IDepartment';
import { ICampus } from 'src/app/shared/models/ICampus';
import { IDegree } from 'src/app/shared/models/IDegree';
import { Observable, combineLatest } from 'rxjs';
import { startWith, map} from 'rxjs/operators';
import { DepartmentStore } from 'src/app/core/services/stores/department-store';
import { CampusStore } from 'src/app/core/services/stores/campus-store';
import { DegreeStore } from 'src/app/core/services/stores/degree-store';
import { UserStore } from 'src/app/core/services/stores/user-store';


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
  mode: "create" | "update";
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
    };

    if (this.mode == 'create') {
      this.loginForm = this.fb.group(
        {
          ...formControls,
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
    } else if (this.mode == 'update') {
       this.loginForm=this.fb.group(formControls);
       this.loginForm.patchValue({
         ...this.user,
         campusId:this.user.campus?.id,
         departmentId:this.user.department?.id,
         degreeId:this.user.degree?.id 
        });
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
      }),
    );
  }

  onSubmit() {
    if (this.mode == 'create') {
      if (this.loginForm.valid) {
        this.userStore.create(this.loginForm.value);
        this.dialogRef.close();
      }
    }else if(this.mode=='update')
    {
        this.userStore.update(this.user.id,this.loginForm.value);
        this.dialogRef.close();
        
    }


    
  }
}
