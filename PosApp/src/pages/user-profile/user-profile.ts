import { LoginPage } from './../login/login';
import { AuthService } from './../../providers/auth-service';
import { Component } from '@angular/core';
import { NavController, NavParams, Events } from 'ionic-angular';

@Component({
  selector: 'page-user-profile',
  templateUrl: 'user-profile.html',
})
export class UserProfilePage {

  constructor(public navCtrl: NavController,
  public events: Events,
  public navParams: NavParams,
   public authenticationService:AuthService) {
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad UserProfilePage');
  }

  doLogout(){
    this.events.publish('user:logout');
    this.authenticationService.logout();
    this.navCtrl.setRoot(LoginPage);
  }

}
