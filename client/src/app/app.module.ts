import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { CoreModule } from './core/core.module';
import { ErrorMessagesService } from './core/services/error-messages.service';
import { LoadingService } from './core/services/loading-service';




@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CoreModule
   
  ],
  providers: [
    ErrorMessagesService,
    LoadingService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
