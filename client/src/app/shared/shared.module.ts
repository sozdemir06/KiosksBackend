import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, } from '@angular/forms';
import { LoadingComponent } from './loading/loading.component';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { NotFoundComponent } from './not-found/not-found.component';
import { AdminToolbarComponent } from './admin-toolbar/admin-toolbar.component';
import { MaterialModule } from '../material/material.module';




@NgModule({
  declarations: [LoadingComponent, NotFoundComponent, AdminToolbarComponent,],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatProgressSpinnerModule,
    MaterialModule
  
  ],

  exports:[
    ReactiveFormsModule,
    CommonModule,
    LoadingComponent,
    MatProgressSpinnerModule,
    NotFoundComponent,
    AdminToolbarComponent,
    MaterialModule
   
    
  ]
})
export class SharedModule { }
