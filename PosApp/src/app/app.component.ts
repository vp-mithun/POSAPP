import { SalesHomePage } from './../pages/sales-home/sales-home';
import { MySalePage } from './../pages/my-sale/my-sale';
import { ProductsPage } from './../pages/products/products';
import { UserProfilePage } from './../pages/user-profile/user-profile';
import { LoginPage } from './../pages/login/login';
import { Component, ViewChild } from '@angular/core';
import { Nav, Platform, Events, MenuController } from 'ionic-angular';
import { StatusBar, Splashscreen } from 'ionic-native';
import { HomePage } from '../pages/home/home';


@Component({
  templateUrl: 'app.html'
})
export class MyApp {
  @ViewChild(Nav) nav: Nav;
  rootPage = LoginPage;

  menupages: Array<{title: string, component: any}>;

  constructor(platform: Platform, public events: Events, public menu: MenuController) {
    platform.ready().then(() => {
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      StatusBar.styleDefault();
      Splashscreen.hide();
    });

    this.menupages = [
                      { title: 'Home', component: HomePage },
                      { title: 'My Sales', component: MySalePage },
                      { title: 'Sales', component: SalesHomePage },
                      { title: 'Products', component: ProductsPage },
                      { title: 'Profile', component:UserProfilePage }
                    ];

      this.listenToLoginEvents();
      this.enableMenu(false);
  }

  openPage(page) {
     // Reset the content nav to have just this page
     // we wouldn't want the back button to show in this scenario
     this.nav.setRoot(page.component);

   }

   listenToLoginEvents() {
    this.events.subscribe('user:login', () => {
      this.enableMenu(true);
    });

    this.events.subscribe('user:logout', () => {
      this.enableMenu(false);
    });
  }

  enableMenu(loggedIn: boolean) {
    this.menu.enable(loggedIn, 'loggedInMenu');
  }
}
