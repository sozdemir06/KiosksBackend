import { NgModule } from '@angular/core';
import { UserProfileComponent } from './user-profile.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { RouterModule, Routes } from '@angular/router';
import { UserProfileChangePasswordComponent } from './user-profile-change-password/user-profile-change-password.component';
import { UserProfileEditPhotoComponent } from './user-profile-edit-photo/user-profile-edit-photo.component';

export const routes: Routes = [
  {
    path: '',
    component: UserProfileComponent,
  },
];

@NgModule({
  declarations: [
    UserProfileComponent,
    UserProfileChangePasswordComponent,
    UserProfileEditPhotoComponent,
  ],
  
  imports: [SharedModule, RouterModule.forChild(routes)],

  exports: [
    RouterModule,
    UserProfileComponent,
    UserProfileChangePasswordComponent,
    UserProfileEditPhotoComponent,
  ],
})
export class UserProfileModule {}
