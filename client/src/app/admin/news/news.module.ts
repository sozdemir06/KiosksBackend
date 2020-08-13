import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewsComponent } from './news.component';
import { NewsListComponent } from './news-list/news-list.component';
import { EditNewsDialogComponent } from './edit-news-dialog/edit-news-dialog.component';
import { NewsPhotoListComponent } from './news-photo-list/news-photo-list.component';
import { NewsSubscreenListComponent } from './news-subscreen-list/news-subscreen-list.component';
import { Routes, RouterModule } from '@angular/router';
import { NewsDetailComponent } from './news-detail/news-detail.component';
import { SharedModule } from 'src/app/shared/shared.module';


export const routes:Routes=[
  {
    path:"",
    component:NewsComponent
  },
  {
    path:"detail/:id",
    component:NewsDetailComponent
  }
]
@NgModule({
  declarations: [
    NewsComponent,
    NewsListComponent,
    EditNewsDialogComponent,
    NewsPhotoListComponent,
    NewsSubscreenListComponent,
    NewsDetailComponent
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  
  ],

  exports:[
    NewsComponent,
    NewsListComponent,
    EditNewsDialogComponent,
    NewsPhotoListComponent,
    NewsSubscreenListComponent,
    NewsDetailComponent,
    RouterModule
  ]
})
export class NewsModule {}
