import { Mysalemodal } from './../mysalemodal/mysalemodal';
import { SalesInfo } from './../../models/SalesInfo';
import { PosDataService } from './../../providers/pos-data-service';
import { Component } from '@angular/core';
import { NavController, NavParams, LoadingController, Events, ModalController } from 'ionic-angular';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
  selector: 'page-my-sale',
  templateUrl: 'my-sale.html'
})
export class MySalePage {
loading:any;
mysaleList:SalesInfo[];
mysortedList = [];
myFiltersortedList = [];
searchByDate:string = new Date().toISOString();
billNoStr:string = '';

private allSalesDivision:any;

  constructor(public navCtrl: NavController,
              public navParams: NavParams, 
              public modalCtrl: ModalController,
              private _posService:PosDataService,              
              public events: Events ) {

                this.events.subscribe('user:loggedin', ( time) => {  
                                          this.LoadMySalesForDay();
                    });
              }

  ionViewDidLoad() {
    console.log('ionViewDidLoad MySalePage');    
  }  

  LoadMySalesForDay(){    
    //var searchdate = moment(this.searchByDate, 'DD/MM/YYYY');    
    this._posService.getMySalesForDay(this.searchByDate)
            .subscribe(salelist => {                
                this.mysaleList = salelist;

                this.allSalesDivision =
                _.chain(salelist)
                .groupBy('billnum')
                .toPairs()
                .map(item => _.zipObject(['billNo', 'billItems'], item))
                .value();

                this.mysortedList = this.allSalesDivision;
                this.myFiltersortedList = this.allSalesDivision;
                console.log(this.mysortedList);
            });
  }

  ShowBillDetails(billItem){
    //console.log(billItem);
    let modal = this.modalCtrl.create(Mysalemodal, {selectedbill:billItem});
    modal.present();

  }

  SearchByBillNo(){
    if(this.billNoStr.length >= 2){
      let qryStr = this.billNoStr.toLowerCase();

      let filterProds = [];
      filterProds = _.filter(this.mysortedList, t=> (<any>t).billNo.toLowerCase().includes(qryStr));
      this.myFiltersortedList = filterProds;
    }
    else{
      this.myFiltersortedList = this.mysortedList;
    }
  }
}
