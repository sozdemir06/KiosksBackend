import { NgModule } from '@angular/core';
import { AnnounceContentTypesComponent } from './announce-content-types.component';
import { AnnounceContentTypeListComponent } from './announce-content-type-list/announce-content-type-list.component';
import { EditAnnounceContentTypeDialogComponent } from './edit-announce-content-type-dialog/edit-announce-content-type-dialog.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { Routes, RouterModule } from '@angular/router';


export const routes:Routes=[
  {
    path:"",
    component:AnnounceContentTypesComponent
  }
]
@NgModule({
  declarations: [
    AnnounceContentTypesComponent,
    AnnounceContentTypeListComponent,
    EditAnnounceContentTypeDialogComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
    
  ],

  exports:[
    AnnounceContentTypesComponent,
    AnnounceContentTypeListComponent,
    EditAnnounceContentTypeDialogComponent,
    RouterModule
  ]
})
export class AnnounceContentTypesModule {}
