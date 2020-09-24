import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MustMatch } from 'src/app/shared/helpers/password-match';
import { IUserList } from 'src/app/shared/models/IUser';
import { PublicUserStore } from '../../store/public-user-store';

@Component({
  selector: 'app-user-profile-change-password',
  templateUrl: './user-profile-change-password.component.html',
  styleUrls: ['./user-profile-change-password.component.scss'],
})
export class UserProfileChangePasswordComponent implements OnInit {
  changePasswordForm: FormGroup;
 @Input() user:IUserList;

  constructor(
    private fb: FormBuilder,
    private publicStore:PublicUserStore
    ) {}

  ngOnInit(): void {
    this.checkForm();
  }

  get f() {
    return this.changePasswordForm.controls;
  }

  checkForm() {
    this.changePasswordForm = this.fb.group({
      oldPassword: [
        '',
        [Validators.required, Validators.minLength(4), Validators.maxLength(8)],
      ],
      newPassword: [
        '',
        [Validators.required, Validators.minLength(4), Validators.maxLength(8)],
      ],

      confirmNewPassword: [
        '',
        [Validators.required, Validators.minLength(4), Validators.maxLength(8)],
      ],
    },
    
    {
      validator: MustMatch('newPassword', 'confirmNewPassword'),
    }
    );
  }

  onSubmit(){
    if(this.changePasswordForm.valid){
      this.publicStore.changePassword(this.changePasswordForm.value,this.user.id);
      this.changePasswordForm.reset();
    }
  }
}
