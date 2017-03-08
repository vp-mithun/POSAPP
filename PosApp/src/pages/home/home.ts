import { ProductsPage } from './../products/products';
import { SalesHomePage } from './../sales-home/sales-home';
import { MySalePage } from './../my-sale/my-sale';
import { Users } from './../../models/Users';
import { PosDataService } from './../../providers/pos-data-service';
import { Component } from '@angular/core';

import { NavController } from 'ionic-angular';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {
  mysalestab = MySalePage;
  saleshometab = SalesHomePage;
  productstab = ProductsPage;
  loggedInUserName:string;
  loggedInUser:Users;
  //loggedInUser: Users[] = [];

  constructor(public navCtrl: NavController, private _posService:PosDataService) {
    
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad LoginPage');    
        this.loadUserInfo();
  }

  loadUserInfo()
  {
    this._posService.getUserDetails()
            .subscribe(users => {
                this.loggedInUser = users;
                this.loggedInUserName = users.employeeName;
                localStorage.setItem('loggedUserInfo', JSON.stringify(this.loggedInUser));
            });
  }

}
