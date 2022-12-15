import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { getArabicPaginatorIntl } from './shared/arabic-paginator-intl';
import { AppConsts } from 'src/AppConsts';
import { API_BASE_URL } from 'src/shared/service-proxies/service-proxies';
import { ConfirmDialogComponent } from './shared/confirm-dialog/confirm-dialog.component';
import { InterceptService } from 'src/app/_helpers/intercept.service';
export function getBaseUrl(): string {
  return AppConsts.baseUrl;
}
@NgModule({
  declarations: [AppComponent,ConfirmDialogComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    CoreModule,
    SharedModule,
    MatPaginatorModule,
  ],
  providers: [
    { provide: MatPaginatorIntl, useValue: getArabicPaginatorIntl() },
    { provide: API_BASE_URL, useFactory: getBaseUrl },
    InterceptService,
    {
      provide: HTTP_INTERCEPTORS,
        useClass: InterceptService,
      multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
    