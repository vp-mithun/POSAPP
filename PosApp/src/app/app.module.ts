import { ProductsPage } from './../pages/products/products';
import { SalesHomePage } from './../pages/sales-home/sales-home';
import { MySalePage } from './../pages/my-sale/my-sale';
import { PosDataService } from './../providers/pos-data-service';
import { AuthService } from './../providers/auth-service';
import { LoginPage } from './../pages/login/login';
import { NgModule, ErrorHandler } from '@angular/core';
import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';
import { MyApp } from './app.component';
import { HomePage } from '../pages/home/home';

@NgModule({
  declarations: [
    MyApp,
    HomePage, 
    LoginPage,
    MySalePage,
    SalesHomePage,
    ProductsPage
  ],
  imports: [
    IonicModule.forRoot(MyApp)
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    HomePage,
    LoginPage,
    MySalePage,
    SalesHomePage,
    ProductsPage
  ],
  providers: [{provide: ErrorHandler, useClass: IonicErrorHandler}, AuthService, PosDataService]
})
export class AppModule {}
