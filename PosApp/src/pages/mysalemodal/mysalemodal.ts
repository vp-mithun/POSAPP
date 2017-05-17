import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ViewController } from 'ionic-angular';


@Component({
  selector: 'page-mysalemodal',
  templateUrl: 'mysalemodal.html',
})
export class Mysalemodal {
  billdetails:any;
  firstbill:any;

  constructor(public navCtrl: NavController, public navParams: NavParams, public viewCtrl: ViewController) {
    this.billdetails = navParams.get('selectedbill');
    this.firstbill = this.billdetails.billItems[0];
    console.log(this.billdetails);
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad Mysalemodal');
  }

  dismiss() {
    this.viewCtrl.dismiss();
}

}
