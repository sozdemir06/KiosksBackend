import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { LoadingComponent } from './loading/loading.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NotFoundComponent } from './not-found/not-found.component';
import { AdminToolbarComponent } from './admin-toolbar/admin-toolbar.component';
import { MaterialModule } from '../material/material.module';
import { PagerComponent } from './pager/pager.component';
import { ErrorMessagesComponent } from './error-messages/error-messages.component';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import { HasRoleDirective } from './directives/has-role-directive';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
import { UserAutocompleteComponent } from './user-autocomplete/user-autocomplete.component';
import { TimeAgoPipe } from './pipes/time-ago-pipe';
import { AnnounceStatusComponent } from './announce-status/announce-status.component';
import { ShortenPipe } from './pipes/shorten-pipe';
import { AnnounceDetailMenuComponent } from './announce-detail-menu/announce-detail-menu.component';
import { UserCardComponent } from './user-card/user-card.component';
import { ImageSliderComponent } from './image-slider/image-slider.component';
import { NgxGalleryModule} from "ngx-gallery-9";
import { UploadComponent } from './upload/upload.component';
import { SubscreensListForAnnounceComponent } from './subscreens-list-for-announce/subscreens-list-for-announce.component';
import { QuillModule } from "ngx-quill";
import { CustomSliderComponent } from './custom-slider/custom-slider.component';

@NgModule({
  declarations: [
    LoadingComponent,
    NotFoundComponent,
    AdminToolbarComponent,
    PagerComponent,
    ErrorMessagesComponent,
    ConfirmDialogComponent,
    HasRoleDirective,
    UserAutocompleteComponent,
    TimeAgoPipe,
    ShortenPipe,
    AnnounceStatusComponent,
    AnnounceDetailMenuComponent,
    UserCardComponent,
    ImageSliderComponent,
    UploadComponent,
    SubscreensListForAnnounceComponent,
    CustomSliderComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatProgressSpinnerModule,
    MaterialModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    NgxGalleryModule,
    QuillModule.forRoot()
  ],

  exports: [
    ReactiveFormsModule,
    CommonModule,
    LoadingComponent,
    MatProgressSpinnerModule,
    NotFoundComponent,
    AdminToolbarComponent,
    MaterialModule,
    PagerComponent,
    ErrorMessagesComponent,
    ConfirmDialogComponent,
    HasRoleDirective,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    UserAutocompleteComponent,
    TimeAgoPipe,
    ShortenPipe,
    AnnounceStatusComponent,
    AnnounceDetailMenuComponent,
    UserCardComponent,
    ImageSliderComponent,
    UploadComponent,
    SubscreensListForAnnounceComponent,
    QuillModule,
    CustomSliderComponent
  ],
})
export class SharedModule {}
