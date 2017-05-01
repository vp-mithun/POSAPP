import { AppSettings } from './../../models/AppSettings';
import { PrinterService } from './../../providers/printer-service';
import { InvoiceGenerator } from './../../providers/invoice-generator';
import { BillInfo } from './../../models/BillInfo';
import { BillInvoice } from './../../models/BillInvoice';
import { Observable } from 'rxjs';
import { Users } from './../../models/Users';
import { Salebook } from './../../models/Salebook';
import { SalesInfo } from './../../models/SalesInfo';
import { FormGroup, FormBuilder, Validators, AbstractControl, ValidatorFn, FormArray } from '@angular/forms';
import { Products } from './../../models/Products';
import { PosDataService } from './../../providers/pos-data-service';
import { Component, Input, ViewChild } from '@angular/core';
import { NavController, AlertController, LoadingController } from 'ionic-angular';
import * as _ from 'lodash';
import * as moment from 'moment';
import {Printer, PrintOptions} from 'ionic-native';
var Handlebars = require('Handlebars');


@Component({
  selector: 'page-sales-home',
  templateUrl: 'sales-home.html'
})
export class SalesHomePage {
  //isAddedEdit:boolean = true;
  @ViewChild('qtyinpt') qtyinput;

  qtybuttonlist:number[] = [1,2,3,4,5,6,7,8,9]
  productslist: Products[] = []; //Based on loggedIn user branchid & shopid
  fullproductslist: Products[] = [];
  singleFilterProduct:Products;
  salesBookList:Salebook[] = [];  
  productsGrid: Array<Array<Products>>;
  searchTypestr:string;
  chkSearchType:boolean;
  prodsearchQuery:string = '';
  productSearchQuery:string = '';
  quantityItems:number;
  qtyBtnSelected:number;
  discountper:number;
  grandTotal:number;
  salesubTotal:number;
  loggedInUser: Users;
  txtnarration:string;
  billHtmlTemplate:any;

  //Sale Common Properties
  customerName:string = "Haribhakt";
  validateDate:any = moment().format('L');
  saleBookopt:string = "Cash Sales";
  saleBookoptAbbr:string = "CS";
  payByopt:string = "cash";
  generatedBillNo:string;
  todayDate:any = moment().format('L');//moment().format('DD[-]MM[-]YYYY')
  saletime:any = moment().format('LT');
  loading:any;

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
              private invbill:InvoiceGenerator,
              private _posService:PosDataService,
              public printSer:PrinterService,
              private loadingCtrl:LoadingController) {

    this.chkSearchType = true;
    this.searchTypestr = "Search Products..";

    this.loggedInUser = JSON.parse(localStorage.getItem('loggedUserInfo')) as Users;

    this.invbill.buildInvoiceFromTemplate()
                .subscribe(htmltemp => {
                  this.billHtmlTemplate = Handlebars.compile(htmltemp);
                });

    this.loading  = this.loadingCtrl.create({
        content: 'Processing Order..'        
      });     

    //Initiate Form
    this.SetupSalesForm();
    this.ConnectToPrinter();
  }

  ConnectToPrinter(){
    let settingsObj = JSON.parse(localStorage.getItem('AppSettings')) as AppSettings;
          if (settingsObj != null) {
              //this.PosApiUrl = settingsObj.printerName
              if(settingsObj.printerName !== null){
              this.printSer.connectToBlueToothDevice(settingsObj.printerName).then(result => {
                      console.log('Connection ' + JSON.stringify(result));                      
                            
                              }).catch(err => {
                                alert('Printer Connectin ' + err);
                              });
            }
            else{
              alert("No Printer, Configured !!");
            }
          }

  }

  ionViewDidEnter() {
    console.log("Sales Home enter");    
    this.setQuantityFocus();
    this.LoadEssentials();
  }  

  setQuantityFocus(){
    setTimeout(() => {
      this.qtyinput.setFocus();
    },150);
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
                billNumb = this.saleBookoptAbbr  + "-" + this.padZero((salecount+1), 4,null);
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
      this.searchTypestr = "Search Products..";
    }
    // else{
    //   //product name
    //   this.searchTypestr = "Search by Product Name...";
    // }
  }

  loadProductsList(){
    this._posService.getProductList()
            .subscribe(prodlist => {
                this.productslist = _.take(prodlist, 100);
                this.fullproductslist = prodlist;
            });
  }

  loadSalesbookList(){
    this._posService.getSaleBookList()
            .subscribe(saleblist => {
                this.salesBookList = saleblist;
            });
  }

  //Search by Barcode & add to SalesList
  FilterProducts(){
    if (this.productSearchQuery.length >= 2) {
      let qryStr = this.productSearchQuery.toLowerCase();

      let filterProds = [];
      filterProds = _.filter(this.productslist, t=> (<Products>t).barcode.toLowerCase().includes(qryStr));
      this.productslist = filterProds;
      if (filterProds.length == 1) {
        //Add to SalesList
        let selProduct = filterProds[0];

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
    } else {
      this.productslist = this.fullproductslist;      
    }
  }

  showFilterProducts(){
    if (this.productSearchQuery.length >= 2) {
      this.singleFilterProduct = null;
      this.productslist = this.fullproductslist;
      let qryStr = this.productSearchQuery.toLowerCase();

      let filterProds = [];
      if (this.chkSearchType) {
        //Barcode search
        filterProds = _.filter(this.productslist, t=> (<Products>t).barcode.toLowerCase().includes(qryStr) || (<Products>t).productName.toLowerCase().includes(qryStr));  
      } else {
        //Name search
        filterProds = _.filter(this.productslist, t=> (<Products>t).productName.toLowerCase().includes(qryStr));
      }
      this.productslist = filterProds;

      if (filterProds.length == 1) {
        //Add to SalesList
        this.singleFilterProduct = filterProds[0];        
      }
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

  ShortProductName(prodname:string){
    return (prodname.substring(0,20));
  }

  PrepareSaleInfoList(selProduct:Products){
    if(selProduct !== null){
    let singleSale = new SalesInfo();
      //singleSale.id = selProduct.id;
      singleSale.productCode = selProduct.barcode;
      singleSale.branchId = selProduct.branchId;
      singleSale.productName = selProduct.productName;
      singleSale.shortproductName = this.ShortProductName(selProduct.productName);
      singleSale.quantity = this.quantityItems.toString();
      //singleSale.quantity = this.qtyBtnSelected.toString();
      singleSale.price = selProduct.sellingPrice;
      singleSale.discount = 0;
      singleSale.discountper = 0;
      singleSale.discountamt = 0;
      singleSale.saleManger = this.loggedInUser.employeeName; //Logged in User Name
      //singleSale.amount = (this.qtyBtnSelected * selProduct.sellingPrice).toString();
      singleSale.amount = (this.quantityItems * selProduct.sellingPrice).toString();
      singleSale.billNum = moment().format('DD[-]MM[-]YYYY[-]') + this.generatedBillNo;
      singleSale.billnum = this.generatedBillNo;
      singleSale.numcount = ""; //Generate it TODO - Not Needed
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
      singleSale.ptype = parseInt(selProduct.ptype);
      this.salesList.push(singleSale);
    }
  }


  IsDuplicateProductAdded(toAddProd:Products): boolean{
    if(toAddProd == null){
      return;
    }
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
      this.setDiscountOnSingleSales();
    }
    this.setTotalAmountOnSales(this.grandTotal);
  }

  setDiscountOnSingleSales(){    
    let disc = this.discountper;
    _.forEach(this.salesList, function(item:SalesInfo) {
        item.discount = disc;
      });
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
                //itemQty: this.qtyBtnSelected,
                itemQty: this.quantityItems,
                //itemPrice: this.qtyBtnSelected * selProduct.sellingPrice,
                itemPrice: this.quantityItems * selProduct.sellingPrice,
                itemSellingPrice: selProduct.sellingPrice,
                itembarcode:selProduct.barcode                
        });
      }
  } 

  //Only Save to DB
  SaveSales(){    
    if (this.salesList.length > 0) {
      this.loading.present();
      let narration = this.txtnarration;
    _.forEach(this.salesList, function(item:SalesInfo) {
        item.narration = narration;
      });

      this._posService.saveSalesListDB(this.salesList)
            .subscribe(savedbillNo => {
                console.log('return save ' + savedbillNo);
                this.loading.dismiss();
                //this.createNewSaleForm();
                this.PrepareBillToPrint(savedbillNo);
            });
    }
    else{
      this.showAlert("Add items to Sales");
    }
  }  

  isReadyToPrint(event):boolean{
    let isready:boolean = true;
    //This is ensure that 
    if(this.productSearchQuery == "0" &&
     this.productSearchQuery.length == 1 && this.salesList.length == 0){
       isready = false;
    }
    return isready;
  }


  //Works only with ENTER keystroke
  AddPrintSaleItems(event) {
    if ((this.quantityItems == undefined || this.quantityItems <= 0) && event.keyCode == 13) {
      this.showAlert("Invalid quantity");
      return;
    }
    
    if(event.keyCode == 13 && !this.isReadyToPrint(event)){
      this.showAlert("No items exists, add one");
      return;
    }

    // Goes for saving & printing - IMP
    if(event.keyCode == 13 && this.productSearchQuery == "0" &&
     this.productSearchQuery.length == 1 && this.salesList.length > 0){       
       this.SaveSales();
       //this.PrepareBillToPrint();
    }   

    if(event.keyCode == 13 && this.productSearchQuery != "0" &&
     this.productSearchQuery.length != 1 && this.singleFilterProduct !== null){
      if(this.IsDuplicateProductAdded(this.singleFilterProduct)){
            this.showAlert("Product already added...");
            return;
          }
        else{
          this.PrepareSaleInfoList(this.singleFilterProduct);
        }
    
        this.isSalesItemsExists = true;
        this.salesItems.push(this.buildSalesItems(this.singleFilterProduct));    
        this.UpdateSubGrandTotal();

        this.productSearchQuery = '';
        this.productslist = this.fullproductslist;      
    }
}

//Gets called after Save to DB
PrepareBillToPrint(newBillNo:string){
  let grpbyCounter = _.toArray(_.groupBy(this.salesList, 'counter'));
       let printSlips = []; 

       //grpbyCounter.forEach(elem => {
         for (var index = 0; index < grpbyCounter.length; index++) {
           var element = grpbyCounter[index];
          let billInv = new BillInvoice();
          let newBill:BillInfo;
            if (grpbyCounter.length == (index + 1)) {
              newBill = billInv.PrepareBill(element, true); //Grand total included in bill
              newBill.billNo = newBillNo;
            }
            else{
              newBill = billInv.PrepareBill(element, false);
              newBill.billNo = newBillNo;
            }
            printSlips.push(this.GenerateBillHTML(newBill));                      
       };

       this.RunPrintSlips(printSlips).then(res => {
         this.createNewSaleForm();
       });
}

RunPrintSlips(tasks) {  
  var result = Promise.resolve();
  tasks.forEach(task => {
    result = result.then(() =>{
      //console.log(task);      
      this.printSer.printText(task, null).then(function(){
      }).catch(function(err){
        alert("Printer Erro! " + err);
      });
      });
  });
  return result;
}

GenerateBillHTML(newBill:BillInfo){
        var helpers = function() {
                  var nameIndex = 1;
                  Handlebars.registerHelper('item_index', function() {
                    return nameIndex++;
                  });
                }();
        var result = this.billHtmlTemplate(newBill);
        console.log(result);
        return result;
}

  //Save to DB & Print BILL
  PrintSales(billHtml): Promise<boolean>{
    var options = { name: 'awesome' };
    return new Promise<boolean>((resolve, reject)=>{
      Printer.print(billHtml, options).then(function(){
        resolve(true);
        //alert("Printer Done!");
      }).catch(function(){
        reject(false);
        //alert("Printer Erro!");
      });
    });
  }

  isPrintAvailable():boolean{
    Printer.isAvailable().then(function(){
      return true;
    }).catch(function(){
      return false;
    });
    return false;
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
    let newPrice = ((parseInt(itemqty) - 1) * this.salesForm.value.salesItems[index].itemSellingPrice);
    let barcode = this.salesForm.value.salesItems[index].itembarcode;
    let newQty =  parseInt(itemqty) - 1;

    item.patchValue({      
        itemPrice : newPrice,
        itemQty: newQty      
    });
    this.setQtyPriceOnSingleSales(newQty,newPrice, barcode);
    this.UpdateSubGrandTotal();
  }

  setQtyPriceOnSingleSales(newQty:number,newPrice:number, itembarcode:string){    
    _.forEach(this.salesList, function(item:SalesInfo) {
      if(item.productCode == itembarcode){
        item.quantity = newQty.toString();
        item.amount = newPrice.toString();
      }
    });
  }

  AddQty(index){
    let itemqty = this.salesForm.value.salesItems[index].itemQty;
    let barcode = this.salesForm.value.salesItems[index].itembarcode;
    // if (itemqty == 1) {
    //   this.showAlert("Minimum Qty is 1");
    //   return;
    // }    itembarcode
    let itemsArray = this.salesForm.get(['salesItems']) as FormArray;
    let item = itemsArray.at(index);
    let newPrice = ((parseInt(itemqty) + 1) * this.salesForm.value.salesItems[index].itemSellingPrice);
    let newQty = parseInt(itemqty) + 1;

    item.patchValue({      
        itemPrice : newPrice,
        itemQty: newQty      
    });
    this.setQtyPriceOnSingleSales(newQty,newPrice, barcode);
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
            this.createNewSaleForm();
          }
        }
      ]
    });
    confirm.present();
  }

  showunSavedConfirm() {
    let confirm = this.alertCtrl.create({
      title: 'Sales Warning',
      message: 'Unsaved Sales present, Continue?',
      buttons: [
        {
          text: 'No',
          handler: () => {
            console.log('Disagree clicked');
            return;
          }
        },
        {
          text: 'Yes',
          handler: () => {
            //Unsubscribe Observable            
          }
        }
      ]
    });
    confirm.present();
  }

  //Creates new SALE form, loads necessary with data  
  createNewSaleForm(){
    this.salesList = [];
    this.qtyBtnSelected = undefined;
    //Initiate Form
    this.SetupSalesForm();

    this.chkSearchType = true;
    this.searchTypestr = "Search Products..";
    this.productSearchQuery = '';
    this.quantityItems = undefined;
    this.preloadSaleValues();
    this.setQuantityFocus();
    this.isSalesItemsExists = false;
    this.LoadEssentials();

  }

  preloadSaleValues(){
    this.validateDate = moment().format('DD[-]MM[-]YYYY');// moment().format('L');
    this.saleBookopt = "Cash Sales";
    this.saleBookoptAbbr = "CS-";
    this.payByopt = "cash";
    this.generatedBillNo;
    this.todayDate = moment().format('DD[-]MM[-]YYYY')
    this.saletime = moment().format('LT');
  }
}