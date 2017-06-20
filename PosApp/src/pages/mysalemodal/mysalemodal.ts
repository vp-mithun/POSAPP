import { PosDataService } from './../../providers/pos-data-service';
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
  itemsToReturn:SalesInfo[] = [];

  constructor(public navCtrl: NavController,
              public navParams: NavParams,
              public toastCtrl: ToastController,
              public viewCtrl: ViewController,
              private _posService:PosDataService,
              public _config:Configservice) {
    this.billdetails = navParams.get('selectedbill') as SaleDtoArray;
    this.firstbill = this.billdetails.saleInfos[0];
    //this.setControlsVisibility();
    this.canReturnSale = _config.checkForPermissions("salesreturns");
    this.canReturnSale = true; //TODO Remove
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

selectToReturn(item:SalesInfo){
  if(!(_.includes(this.itemsToReturn, item))){
    this.itemsToReturn.push(item);
  }
  else{
    _.remove(this.itemsToReturn, function(n) {
      return n.id === item.id;
    });
  }
}

returnSale(){
  if (!this.billdetails.canReturn) {
    let toast = this.toastCtrl.create({
          message: "No items to return",
          duration: 2000,
          position: 'middle'
        });
        toast.present();
  }
  else{
    if (this.itemsToReturn.length > 0) {
      this._posService.returnSalesDB(this.itemsToReturn)
            .subscribe(savedbillNo => {
                console.log('return sale save ' + savedbillNo);
                //this.loading.dismiss();
                //this.createNewSaleForm();
                //this.PrepareBillToPrint(savedbillNo);
            });
    }
  }
}
}
