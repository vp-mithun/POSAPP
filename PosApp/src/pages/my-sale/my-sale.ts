import { SalesInfo } from './../../models/SalesInfo';
import { PosDataService } from './../../providers/pos-data-service';
import { Component } from '@angular/core';
import { NavController, NavParams, LoadingController } from 'ionic-angular';
import * as _ from 'lodash';

@Component({
  selector: 'page-my-sale',
  templateUrl: 'my-sale.html'
})
export class MySalePage {
loading:any;
mysaleList:SalesInfo[];
mysortedList = [];

private allSalesDivision:any;

  constructor(public navCtrl: NavController, public navParams: NavParams, 
              private _posService:PosDataService,              
              private loadingCtrl:LoadingController, ) {}

  ionViewDidLoad() {
    console.log('ionViewDidLoad MySalePage');
    this.LoadMySalesForDay();
  }

  LoadMySalesForDay(){
    // this.loading  = this.loadingCtrl.create({
    //     content: 'Processing Order..'        
    //   });
    //   this.loading.present();
    this._posService.getMySalesForDay()
            .subscribe(salelist => {
                //console.log(salelist);
                this.mysaleList = salelist;

                this.allSalesDivision =
                _.chain(salelist)
                .groupBy('billnum')
                .toPairs()
                .map(item => _.zipObject(['billNo', 'billItems'], item))
                .value();

                this.mysortedList = this.allSalesDivision;
                console.log(this.mysortedList);
                
                //this.loading.dismiss();
            });
  }

}
