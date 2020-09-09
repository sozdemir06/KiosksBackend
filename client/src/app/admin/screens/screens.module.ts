import { NgModule } from '@angular/core';
import { ScreensComponent } from './screens.component';
import { ScreensListComponent } from './screens-list/screens-list.component';
import { EditScreensDialogComponent } from './edit-screens-dialog/edit-screens-dialog.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { RouterModule, Routes } from '@angular/router';
import { EditScreenHeaderComponent } from './edit-screen-header/edit-screen-header.component';
import { EditScreenFooterComponent } from './edit-screen-footer/edit-screen-footer.component';
import { EditScreenHeaderPhotoComponent } from './edit-screen-header-photo/edit-screen-header-photo.component';

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
    EditScreenHeaderComponent,
    EditScreenFooterComponent,
    EditScreenHeaderPhotoComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  
  ],
  exports:[
    RouterModule,
    ScreensComponent,
    ScreensListComponent,
    EditScreensDialogComponent,
    EditScreenHeaderComponent,
    EditScreenFooterComponent,
    EditScreenHeaderPhotoComponent,
  ]
})
export class ScreensModule {}
