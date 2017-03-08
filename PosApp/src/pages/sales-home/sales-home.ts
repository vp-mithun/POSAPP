import { SalesInfo } from './../../models/SalesInfo';
import { FormGroup, FormBuilder, Validators, AbstractControl, ValidatorFn, FormArray } from '@angular/forms';
import { Products } from './../../models/Products';
import { PosDataService } from './../../providers/pos-data-service';
import { Component } from '@angular/core';
import { NavController, NavParams, AlertController } from 'ionic-angular';
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
  grandTotal:number;
  salesubTotal:number;

  //Sale Common Properties
  customerName:string = "Haribhakta";
  validateDate:any = moment().format('L');
  saleBook:string = "Cash Sales";
  generatedBillNo:string = "CS-001";
  todayDate:any = moment().format('L');
  saletime:any = moment().format('LT');

  //Sales Form
  salesList:SalesInfo[] = [];
  isSalesItemsExists:boolean = false;
  salesForm: FormGroup;
  get salesItems(): FormArray{
        return <FormArray>this.salesForm.get('salesItems');
    }

  constructor(public navCtrl: NavController,
              public alertCtrl: AlertController, 
              private fb: FormBuilder,
              private _posService:PosDataService) {

    this.chkSearchType = true;
    this.searchTypestr = "Search by Barcode...";

    //Initiate Form
    this.SetupSalesForm();
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
        filterProds = _.filter(this.productslist, t=> (<Products>t).productName.toLowerCase().includes(qryStr));
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
    if (this.qtyBtnSelected == undefined) {
      this.showAlert("Please select Quantity");
      return;
    }

    if(this.IsDuplicateProductAdded(selProduct)){
      this.showAlert("Product already added...");
      return;
    }
    else{
      this.PrepareSaleInfoList(selProduct);
    }
    
    this.isSalesItemsExists = true;
    this.salesItems.push(this.buildSalesItems(selProduct));    
    this.UpdateSubGrandTotal();
  }

  PrepareSaleInfoList(selProduct:Products){
    let singleSale = new SalesInfo();
      singleSale.productCode = selProduct.barcode;
      singleSale.branchId = selProduct.branchId;
      //To Do - Add More

      this.salesList.push(singleSale);
  }


  IsDuplicateProductAdded(toAddProd:Products): boolean{
    let prodExists:boolean = false;   

    let foundPrd = _.find(this.salesList, { 'productCode': toAddProd.barcode });
    if (foundPrd !== undefined) {
      prodExists = true;
    }
    return prodExists
  }

  UpdateSubGrandTotal()
  {
    let subtotal = _.sumBy(this.salesForm.value.salesItems, 'itemPrice');
    this.salesubTotal = subtotal;
    this.grandTotal = subtotal;
    this.ApplyDiscount();
  }

  ApplyDiscount(){
    if(this.discountper !== undefined){
    let disGrandTotal = (this.discountper/100) * this.salesubTotal;
    this.grandTotal = Math.round(this.salesubTotal - disGrandTotal);
    }
  }

  SetupSalesForm()
  {    
    this.salesForm = this.fb.group({
      salesItems: this.fb.array([])
    });
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

  buildSalesItems(selProduct:Products): FormGroup {
    if (selProduct.barcode !== undefined && selProduct.productName !== undefined) {
      return this.fb.group({
                itemName: selProduct.productName,
                itemQty: this.qtyBtnSelected,
                itemPrice: this.qtyBtnSelected * selProduct.sellingPrice
        });
      }
  }    
    

  saveSales(){

  }

  showAlert(msg) {
    let alert = this.alertCtrl.create({
      title: 'Sales Warning',
      subTitle: msg,
      buttons: ['OK']
    });
    alert.present();
  }
}