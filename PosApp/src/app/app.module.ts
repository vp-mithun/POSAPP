import { ProductsPage } from './../pages/products/products';
import { UserProfilePage } from './../pages/user-profile/user-profile';
import { Mysalemodal } from './../pages/mysalemodal/mysalemodal';
import { Configservice } from './../providers/configservice';
import { PrinterService } from './../providers/printer-service';
import { SqlDbService } from './../providers/sql-db-service';
import { SettingsPage } from './../pages/settings/settings';
import { InvoiceGenerator } from './../providers/invoice-generator';
import { SalesHomePage } from './../pages/sales-home/sales-home';
import { MySalePage } from './../pages/my-sale/my-sale';
import { PosDataService } from './../providers/pos-data-service';
import { AuthService } from './../providers/auth-service';
import { LoginPage } from './../pages/login/login';
import { NgModule, ErrorHandler } from '@angular/core';
import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';
import { MyApp } from './app.component';
import { HomePage } from '../pages/home/home';
import { HttpModule } from "@angular/http";
import { BrowserModule } from "@angular/platform-browser";

@NgModule({
  declarations: [
    MyApp,
    HomePage,
    LoginPage,
    MySalePage,
    SalesHomePage,
    SettingsPage,
    Mysalemodal,
    UserProfilePage,
    ProductsPage
  ],
  imports: [
    BrowserModule,
    HttpModule,
    IonicModule.forRoot(MyApp)
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    HomePage,
    LoginPage,
    MySalePage,
    SalesHomePage,
    SettingsPage,
    Mysalemodal,
    UserProfilePage,
    ProductsPage
  ],
  providers: [{provide: ErrorHandler, useClass: IonicErrorHandler}, AuthService, PosDataService, InvoiceGenerator, SqlDbService, PrinterService, Configservice]
})
export class AppModule {}
