import { LoginPage } from './../login/login';
import { AuthService } from './../../providers/auth-service';
import { Users } from './../../models/Users';
import { PosDataService } from './../../providers/pos-data-service';
import { Component } from '@angular/core';

import { NavController, Events, MenuController } from 'ionic-angular';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {
  // mysalestab = MySalePage;
  // saleshometab = SalesHomePage;
  // productstab = ProductsPage;
  loggedInUserName:string;
  loggedInUser:Users;



  constructor(public navCtrl: NavController,public events: Events, private _posService:PosDataService,
   private authenticationService: AuthService, public menu:MenuController) {

  }

  ionViewDidLoad() {
      this.loadUserInfo();
  }


  loadUserInfo()
  {
    this._posService.getUserDetails()
            .subscribe(users => {
                this.loggedInUser = users;
                this.loggedInUserName = users.employeeName;
                localStorage.setItem('loggedUserInfo', JSON.stringify(this.loggedInUser));
                this.loadStoreDetails();
                this.loadUserPermissions(users.id);
            });
  }

  //Store Details of LOgged in user
  loadStoreDetails(){
    this._posService.getStoreDetails()
            .subscribe(storedetails => {
                localStorage.setItem('loggedUserStoreInfo', JSON.stringify(storedetails));
                this.events.publish('user:loggedin',Date.now());
            });
  }

  loadUserPermissions(userId:number)
  {
    this._posService.getUserPermissions(userId)
            .subscribe(usrspm => {
                localStorage.setItem('loggedUserPermission', JSON.stringify(usrspm));
            });
  }



  doLogout(){
    this.authenticationService.logout();
    this.navCtrl.setRoot(LoginPage);
  }
}
