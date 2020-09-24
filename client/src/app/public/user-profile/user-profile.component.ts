import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import { IUserList } from 'src/app/shared/models/IUser';
import { IUserPhoto } from 'src/app/shared/models/IUserPhoto';
import { IUserCamPusAndDepartmentAndDegree } from '../models/IUserCamPusAndDepartmentAndDegree';
import { PublicUserStore } from '../store/public-user-store';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss'],
})
export class UserProfileComponent implements OnInit, OnDestroy {
  userForm: FormGroup;
  data$: Observable<IUserCamPusAndDepartmentAndDegree>;
  user: IUserList;
  subscription: Subscription = Subscription.EMPTY;
  roleForUpload:string[]=['Sudo','Public'];
  constructor(
    private fb: FormBuilder,
    public publicUserStore: PublicUserStore
  ) {}

  ngOnInit(): void {
    this.checkUserForm();
    this.subscription = this.publicUserStore.user$.subscribe((result) => {
      this.user = result;
      this.userForm.patchValue({
        ...result,
        campusId: result?.campus?.id,
        departmentId: result?.department?.id,
        degreeId: result?.degree?.id,
      });
    });

    this.data$ = this.publicUserStore.getUserCamPusAndDepartmentAndDegree();
    this.userForm.get('firstName').disable();
    this.userForm.get('lastName').disable();
    this.userForm.get('email').disable();
    this.userForm.get('degreeId').disable();
  }

  uploadResult(event:IUserPhoto){
      this.publicUserStore.uploadNewPhoto(event);
  }

  get f() {
    return this.userForm.controls;
  }

  checkUserForm() {
    this.userForm = this.fb.group({
      firstName: [],
      lastName: [],
      email: [],
      interPhone: ['', [Validators.maxLength(11)]],
      gsmPhone: ['', [Validators.maxLength(11)]],
      campusId: ['', Validators.required],
      departmentId: ['', Validators.required],
      degreeId: [''],
    });
  }

  onSubmit() {
    if (this.userForm.valid) {
      const model: IUserList = {
        ...this.userForm.value,
        degreeId: this.user?.degree.id,
        firstName:this.user?.firstName,
        lastName:this.user?.lastName,
        email:this.user?.email
      };
      this.publicUserStore.updateUser(model, this.user?.id);
    }
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
