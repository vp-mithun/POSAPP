import { Users } from './../../models/Users';
import { Salebook } from './../../models/Salebook';
import { SalesInfo } from './../../models/SalesInfo';
import { FormGroup, FormBuilder, Validators, AbstractControl, ValidatorFn, FormArray } from '@angular/forms';
import { Products } from './../../models/Products';
import { PosDataService } from './../../providers/pos-data-service';
import { Component } from '@angular/core';
import { NavController, AlertController } from 'ionic-angular';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
  selector: 'page-sales-home',
  templateUrl: 'sales-home.html'
})
export class SalesHomePage {
  //isAddedEdit:boolean = true;
  qtybuttonlist:number[] = [1,2,3,4,5,6,7,8,9]
  productslist: Products[] = []; //Based on loggedIn user branchid & shopid
  fullproductslist: Products[] = [];
  salesBookList:Salebook[] = [];  
  productsGrid: Array<Array<Products>>;
  searchTypestr:string;
  chkSearchType:boolean;
  prodsearchQuery:string = '';
  qtyBtnSelected:number;
  discountper:number;
  grandTotal:number;
  salesubTotal:number;
  loggedInUser: Users;
  txtnarration:string;

  //Sale Common Properties
  customerName:string = "Haribhakt";
  validateDate:any = moment().format('DD[-]MM[-]YYYY');// moment().format('L');
  saleBookopt:string = "Cash Sales";
  saleBookoptAbbr:string = "CS-";
  payByopt:string = "cash";
  generatedBillNo:string;
  todayDate:any = moment().format('DD[-]MM[-]YYYY')
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

    this.loggedInUser = JSON.parse(localStorage.getItem('loggedUserInfo')) as Users;

    //Initiate Form
    this.SetupSalesForm();
  }

  ionViewDidEnter() {
    this.LoadEssentials();
  }

  LoadEssentials(){
    this.loadProductsList();
    this.loadSalesbookList();
    this.generateBillNumber();
  }

  padZero(n, width, z) {
  z = z || '0';
  n = n + '';
  return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}

  generateBillNumber():string{
    let billNumb: string = '';

    this._posService.countOfSalesForDay()
            .subscribe(salecount => {
                billNumb = this.saleBookoptAbbr + this.loggedInUser.id + "-" + this.padZero((salecount+1), 3,null);
                this.generatedBillNo = billNumb;                
            });

    return billNumb;
  }

  StartSales():void{
    //this.isAddedEdit = false;
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
            });
  }

  loadSalesbookList(){
    this._posService.getSaleBookList()
            .subscribe(saleblist => {
                this.salesBookList = saleblist;
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
      //singleSale.id = selProduct.id;
      singleSale.productCode = selProduct.barcode;
      singleSale.branchId = selProduct.branchId;
      singleSale.productName = selProduct.productName;
      singleSale.quantity = this.qtyBtnSelected.toString();
      singleSale.price = selProduct.sellingPrice;
      singleSale.discount = 0;
      singleSale.discountper = 0;
      singleSale.discountamt = 0;
      singleSale.saleManger = this.loggedInUser.employeeName; //Logged in User Name
      singleSale.amount = (this.qtyBtnSelected * selProduct.sellingPrice).toString();
      singleSale.billNum = moment().format('DD[-]MM[-]YYYY[-]') + this.generatedBillNo;
      singleSale.billnum = this.generatedBillNo;
      singleSale.numcount = ""; //Generate it TODO
      singleSale.customer = this.customerName;
      singleSale.totalamount = 0; // This gets updated while saving
      singleSale.dates = this.todayDate;
      singleSale.validitydate = this.validateDate;
      singleSale.userId = this.loggedInUser.id;
      singleSale.branchId = this.loggedInUser.branchId;
      singleSale.shopId = this.loggedInUser.shopId;
      singleSale.productId = selProduct.id;
      singleSale.salebook = "33"
      singleSale.counter = parseInt(selProduct.counterNo);
      singleSale.cashtype = "50";
      singleSale.narration = '';
      singleSale.ptype = parseInt(selProduct.ptype)

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
    this.setTotalAmountOnSales(this.grandTotal);
  }

  setTotalAmountOnSales(grandTotal:number){    
    _.forEach(this.salesList, function(item:SalesInfo) {
        item.totalamount = grandTotal;
      });
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
                itemPrice: this.qtyBtnSelected * selProduct.sellingPrice,
                itemSellingPrice: selProduct.sellingPrice,
                itembarcode:selProduct.barcode
        });
      }
  }    
    

  //Only Save to DB
  SaveSales(){
    //this.isAddedEdit = true;    

    if (this.salesList.length > 0) {
      let narration = this.txtnarration;
    _.forEach(this.salesList, function(item:SalesInfo) {
        item.narration = narration;
      });

      this._posService.saveSalesListDB(this.salesList)
            .subscribe(prodlist => {
                console.log('return save');
            });
    }
    else{
      this.showAlert("Add items to Sales");
    }
  }

  //Save to DB & Print BILL
  PrintSales(){
    if (this.salesList.length > 0) {
      //ToDo - Printing
    }
    else{
      this.showAlert("Add items to Sales");
    }   
  }

  CloseSales(){
    this.showConfirm();
  }

  ReduceQty(index){
    
    let itemqty = this.salesForm.value.salesItems[index].itemQty;
    if (itemqty == 1) {
      this.showAlert("Minimum Qty is 1");
      return;
    }    
    let itemsArray = this.salesForm.get(['salesItems']) as FormArray;
    let item = itemsArray.at(index);
    let newPrice = ((itemqty - 1) * this.salesForm.value.salesItems[index].itemSellingPrice);
    let newQty =  itemqty - 1;

    item.patchValue({      
        itemPrice : newPrice,
        itemQty: newQty      
    });
    this.setQtyPriceOnSingleSales(newQty,newPrice);
    this.UpdateSubGrandTotal();
  }

  setQtyPriceOnSingleSales(newQty:number,newPrice:number){    
    _.forEach(this.salesList, function(item:SalesInfo) {
        item.quantity = newQty.toString();
        item.amount = newPrice.toString();
      });
  }

  AddQty(index){
    let itemqty = this.salesForm.value.salesItems[index].itemQty;
    // if (itemqty == 1) {
    //   this.showAlert("Minimum Qty is 1");
    //   return;
    // }    
    let itemsArray = this.salesForm.get(['salesItems']) as FormArray;
    let item = itemsArray.at(index);
    let newPrice = ((itemqty + 1) * this.salesForm.value.salesItems[index].itemSellingPrice);
    let newQty =  itemqty + 1;

    item.patchValue({      
        itemPrice : newPrice,
        itemQty: newQty      
    });
    this.setQtyPriceOnSingleSales(newQty,newPrice);
    this.UpdateSubGrandTotal();
  }

  RemoveItem(index){
    console.log('Remove ' + index);
    let itemsArray = this.salesForm.get(['salesItems']) as FormArray;
    let item = itemsArray.at(index);    
    itemsArray.removeAt(index);
    this.RemoveFromSalesList(item.value.itembarcode);
    this.UpdateSubGrandTotal();
  }

  RemoveFromSalesList(barcode:any){
    _.remove(this.salesList, function(item:SalesInfo) {
        return  item.productCode == barcode;
      });
  }

  showAlert(msg) {
    let alert = this.alertCtrl.create({
      title: 'Sales Warning',
      subTitle: msg,
      buttons: ['OK']
    });
    alert.present();
  }

  showConfirm() {
    let confirm = this.alertCtrl.create({
      title: 'Sales Warning',
      message: 'Do you want to cancel it?',
      buttons: [
        {
          text: 'No',
          handler: () => {
            console.log('Disagree clicked');
          }
        },
        {
          text: 'Yes',
          handler: () => {
            //this.isAddedEdit = true;
            //TODO -- Other Form elements
            // 1. Load Products, 2. any variable reset
          }
        }
      ]
    });
    confirm.present();
  }
}