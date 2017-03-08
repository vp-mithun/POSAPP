import { Products } from './../../models/Products';
import { PosDataService } from './../../providers/pos-data-service';
import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
  selector: 'page-sales-home',
  templateUrl: 'sales-home.html'
})
export class SalesHomePage {
  qtybuttonlist:number[] = [1,2,3,4,5,6,7,8,9]
  productslist: Products[] = []; //Based on loggedIn user branchid & shopid
  fullproductslist: Products[] = [];
  productsGrid: Array<Array<Products>>;
  searchTypestr:string;
  chkSearchType:boolean;
  prodsearchQuery:string = '';
  qtyBtnSelected:number;
  discountper:number;
  grandTotal:number = 1234523.25

  //Sale Common Properties
  customerName:string = "Haribhakta";
  validateDate:any = moment().format('L')
  saleBook:string = "Cash Sales";
  generatedBillNo:string = "CS-001";
  todayDate:any = moment().format('L')
  saletime:any = moment().format('LT')

  constructor(public navCtrl: NavController, public navParams: NavParams, private _posService:PosDataService) {
    this.chkSearchType = true;
    this.searchTypestr = "Search by Barcode...";
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad SalesHomePage');
    this.loadProductsList();
  }

  onSearchTypeChange(){
    if(this.chkSearchType){
      //Barcode search
      this.searchTypestr = "Search by Barcode...";
    }
    else{
      //product name
      this.searchTypestr = "Search by Product Name...";
    }
  }

  loadProductsList(){
    this._posService.getProductList()
            .subscribe(prodlist => {
                this.productslist = prodlist;
                this.fullproductslist = prodlist;
                //this.CalculateLoadProducts();
            });
  }

  showFilterProducts(){
    if (this.prodsearchQuery.length >= 2) {
      let qryStr = this.prodsearchQuery.toLowerCase();

      let filterProds = [];
      if (this.chkSearchType) {
        //Barcode search
        filterProds = _.filter(this.productslist, t=> (<Products>t).barcode.toLowerCase().includes(qryStr));  
      } else {
        //Name search
        filterProds = _.filter(this.productslist, t=> (<Products>t).product_name.toLowerCase().includes(qryStr));
      }
      this.productslist = filterProds;
    } else {
      this.productslist = this.fullproductslist;      
    }
  }

  setQuantityColor(btnNo){
    //console.log(btnNo);
    this.qtyBtnSelected = btnNo;    
  }

  AddProductToSale(selProduct:Products){
    console.log(selProduct);
    
  }

  CalculateLoadProducts(){
     this.productsGrid = Array(Math.ceil(this.productslist.length/3));

     let rowNum = 0;
    
    for (let i = 0; i < this.productslist.length; i+=3) {
      
      if(this.productslist[i])

      this.productsGrid[rowNum] = Array(3);

      if (this.productslist[i]!==undefined) {
        this.productsGrid[rowNum][0] = this.productslist[i]
      }
      if (this.productslist[i+1]!==undefined) {
        this.productsGrid[rowNum][1] = this.productslist[i+1]
      }
      if (this.productslist[i+2]!==undefined) {
        this.productsGrid[rowNum][2] = this.productslist[i+2]
      }     
    
      rowNum++;
    }
      
  } 

}
