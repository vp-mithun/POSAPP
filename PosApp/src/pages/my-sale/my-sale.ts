import { Mysalemodal } from './../mysalemodal/mysalemodal';
import { SalesInfo, SaleDtoArray } from './../../models/SalesInfo';
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
//mysaleList:SaleDtoArray[];
mysortedList:SaleDtoArray[] = [];
myFiltersortedList:SaleDtoArray[] = [];
searchByDate:string = new Date().toISOString();
billNoStr:string = '';

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

  returnSale(){
    // if (this.billdetails.saleInfos.length == 0) {
    //   let toast = this.toastCtrl.create({
    //         message: "No items to return",
    //         duration: 2000,
    //         position: 'middle'
    //       });
    //       toast.present();
    // }
    // else{
    //   this.navCtrl.parent.select(2);
    // }
    this.navCtrl.parent.select(1);
}

  LoadMySalesForDay(){
    //var searchdate = moment(this.searchByDate, 'DD/MM/YYYY');
    this._posService.getMySalesForDay(this.searchByDate)
            .subscribe(salelist => {
                //this.mysaleList = salelist;


                if (salelist.length > 0) {
                  this.myFiltersortedList = salelist;
                  this.mysortedList = salelist;
                } else {
                    this.myFiltersortedList = [];
                    this.mysortedList = [];
                }
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
      filterProds = _.filter(this.mysortedList, t=> (<SaleDtoArray>t).billNum.toLowerCase().includes(qryStr));
      this.myFiltersortedList = filterProds;
    }
    else{
      this.myFiltersortedList = this.mysortedList;
    }
  }
}
