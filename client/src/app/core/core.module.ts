import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ErrorInterceptorProvider } from './interceptors/error-interceptor';
import { AuthInterceptors } from './interceptors/auth-token-interceptor';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { NotifyComponent } from './notify/notify.component';
import { AuthGuard } from './guards/auth-guard';




@NgModule({
  declarations: [
    NotifyComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,
    MatSnackBarModule
  ],
  exports:[
    NotifyComponent
  ],
  providers:[
    ErrorInterceptorProvider,
    AuthInterceptors,
    AuthGuard
    
  ],
})
export class CoreModule { }
