import { NgModule } from '@angular/core';
import { BuildingAgeListComponent } from './building-age-list/building-age-list.component';
import { EditBuildingAgeDialogComponent } from './edit-building-age-dialog/edit-building-age-dialog.component';
import { BuildingAgeComponent } from './building-age.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';

export const routes:Routes=[
  {
    path:"",
    component:BuildingAgeComponent
  }
]


@NgModule({
  declarations: [
    BuildingAgeComponent,
    BuildingAgeListComponent,
    EditBuildingAgeDialogComponent,
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],

  exports:[
    RouterModule,
    BuildingAgeComponent,
    BuildingAgeListComponent,
    EditBuildingAgeDialogComponent,
  ]
})
export class BuildingAgeModule {}
