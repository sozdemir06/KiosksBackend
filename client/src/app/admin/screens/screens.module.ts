import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScreensComponent } from './screens.component';
import { ScreensListComponent } from './screens-list/screens-list.component';
import { EditScreensDialogComponent } from './edit-screens-dialog/edit-screens-dialog.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { RouterModule, Routes } from '@angular/router';

export const routes:Routes=[
  {
    path:"",
    component:ScreensComponent
  }
]

@NgModule({
  declarations: [
    ScreensComponent,
    ScreensListComponent,
    EditScreensDialogComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  
  ],
  exports:[
    RouterModule,
    ScreensComponent,
    ScreensListComponent,
    EditScreensDialogComponent
  ]
})
export class ScreensModule {}
