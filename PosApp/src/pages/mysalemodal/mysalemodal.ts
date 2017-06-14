import { SalesHomePage } from './../sales-home/sales-home';
import { Configservice } from './../../providers/configservice';
import { UserPermissions } from './../../models/UserPermissions';
import { SalesInfo, SaleDtoArray } from './../../models/SalesInfo';
import { Component } from '@angular/core';
import { NavController, NavParams, ViewController, ToastController } from 'ionic-angular';
import * as _ from 'lodash';

@Component({
  selector: 'page-mysalemodal',
  templateUrl: 'mysalemodal.html',
})
export class Mysalemodal {
  billdetails:SaleDtoArray;
  firstbill:SalesInfo
  canReturnSale:boolean;

  constructor(public navCtrl: NavController,
              public navParams: NavParams,
              public toastCtrl: ToastController,
              public viewCtrl: ViewController,
              public _config:Configservice) {
    this.billdetails = navParams.get('selectedbill') as SaleDtoArray;
    this.firstbill = this.billdetails.saleInfos[0];
    //this.setControlsVisibility();
    this.canReturnSale = _config.checkForPermissions("salesreturns");
  }

  //Sets Controls visibility based on User Permissions
  setControlsVisibility(){
    let userpermission = JSON.parse(localStorage.getItem("loggedUserPermission")) as UserPermissions;
    if(userpermission !== null){
      if (_.includes(userpermission[0].permissionsList, "salesreturns")) {
        this.canReturnSale = true;
      }
    }
  }

  ionViewDidLoad() {

    console.log('ionViewDidLoad Mysalemodal');

  }

  dismiss() {
    this.viewCtrl.dismiss();
}

returnSale(){
  if (this.billdetails.saleInfos.length == 0) {
    let toast = this.toastCtrl.create({
          message: "No items to return",
          duration: 2000,
          position: 'middle'
        });
        toast.present();
  }
  else{
    this.navCtrl.push(SalesHomePage, {"EditSale":this.billdetails});
  }
}
}
