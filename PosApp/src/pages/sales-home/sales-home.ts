import { AppSettings } from './../../models/AppSettings';
import { PrinterService } from './../../providers/printer-service';
import { InvoiceGenerator } from './../../providers/invoice-generator';
import { BillInfo } from './../../models/BillInfo';
import { BillInvoice } from './../../models/BillInvoice';
import { Users } from './../../models/Users';
import { Salebook } from './../../models/Salebook';
import { SalesInfo, SaleDtoArray } from './../../models/SalesInfo';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { Products } from './../../models/Products';
import { PosDataService } from './../../providers/pos-data-service';
import { Component, Input, ViewChild } from '@angular/core';
import { NavController, AlertController, LoadingController, ActionSheetController, NavParams } from 'ionic-angular';
import * as _ from 'lodash';
import * as moment from 'moment';
import {Printer, BarcodeScanner, BarcodeScannerOptions} from 'ionic-native';
var Handlebars = require('Handlebars');
import * as html2canvas from "html2canvas"
declare let DatecsPrinter:any;

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
  options :BarcodeScannerOptions;

  //Sale Common Properties
  customerName:string = "Haribhakt";
  dateValue = moment();
  validityDate:string = this.dateValue.toISOString();
  validateDate:any = moment().format('MM[/]DD[/]YYYY');
  saleBookopt:string = "Cash Sales";
  saleBookoptAbbr:string = "CS";
  payByopt:string = "cash";
  generatedBillNo:string;
  todayDate:any = moment().format('MM[/]DD[/]YYYY')
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
              private loadingCtrl:LoadingController,
              private actShtCtr: ActionSheetController,
              public _navParams:NavParams) {

    this.chkSearchType = true;
    this.searchTypestr = "Search Products..";

    this.loggedInUser = JSON.parse(localStorage.getItem('loggedUserInfo')) as Users;

    console.log(_navParams.get("EditSale"));

    let editSaleitem = _navParams.get("EditSale") as SaleDtoArray;

    //Initiate Form
    this.SetupSalesForm(editSaleitem);
    this.ConnectToPrinter();
  }


  BuildHTMLInvoice(){

let htmlbasedtemplate = `<div>
<div style="max-width: 380px; margin: 5px auto; font-family: Arial, Helvetica, sans-serif; font-size: 20px; text-align: center;">
<div style="min-width: 250px; margin: 0 auto;">
<div style="overflow: hidden; margin-bottom: 15px;">&nbsp;</div>
<h3 style="margin: 0; padding-top: 5px; font-size: 30px; float: left; text-align: center; width: 100%;">{{storeName}}, {{storeLoc}}</h3>
</div><br>
<h5><strong> Counter: {{counter}}</strong></h5>
<span style="float: left; margin-bottom: 3px; text-align: left; width: 50%;"><strong>Bill # </strong> {{billNo}}</span>
 <span style="float: right; margin-bottom: 3px; text-align: right; width: 50%;"><strong>Date:</strong> {{billdate}}</span> <span style="float: left; margin-bottom: 3px; text-align: left; width: 50%;"><strong>Name: </strong>{{custName}}</span>
 <span style="float: right; margin-bottom: 3px; text-align: right; width: 50%;"><strong>Time:</strong> {{billTime}}</span>
<br>
  <br>

<table style="margin: 10px 0; width: 100%; font-size: 16px;" border="0" cellspacing="0">
<thead>
<tr><th style="border-bottom: 1px solid #000;">Desc.</th><th style="border-bottom: 1px solid #000; text-align: center;">Qty.</th><th style="border-bottom: 1px solid #000; text-align: center;">Rate</th><th style="border-bottom: 1px solid #000;">Amount</th></tr>
</thead>
<tbody>
{{#SalesItems}}
<tr>
<td style="text-align: left; vertical-align: top; padding: 5px;">{{shortproductName}}</td>
<td style="text-align: center; vertical-align: top; padding: 5px;">{{quantity}}</td>
<td style="text-align: right; vertical-align: top; padding: 5px;">{{price}}</td>
<td style="text-align: right; vertical-align: top; padding: 5px; padding-right: 18px;">{{amount}}</td>
</tr>
{{/SalesItems}}
<tr>
<td colspan="4"><hr /></td>
</tr>
{{#if isDiscountApplies}}
<tr><th style="text-align: right; vertical-align: top; padding: 5px; border-bottom: 1px solid #000;" colspan="3">Discount:</th><th style="text-align: right; vertical-align: top; padding: 5px; border-bottom: 1px solid #000;"><span style="font-weight: normal;">{{discPert}}</span></th></tr>
{{/if}}
{{#if isTaxable}}
  <tr>
            <th colspan='3' style='text-align:right; vertical-align:top; padding:5px;border-bottom: 1px solid #000;'>Tax:</th>
            <th style='text-align:right; vertical-align:top; padding:5px;border-bottom: 1px solid #000;'><span style='font-weight:normal'>{{taxapplied}}</span></th>
        </tr>
{{/if}}
  <tr>
            <th colspan='1' style='text-align:right; vertical-align:top; padding:5px;border-bottom: 1px solid #000;'>Sub Total:</th>
            <th colspan='1' style='text-align:center;border-bottom: 1px solid #000;'>{{billQty}}</th>
            <th colspan='2' style='text-align:right; vertical-align:top; padding:5px;border-bottom: 1px solid #000;'>{{billSubTotal}}</th>
        </tr>
{{#if isGrandTotal}}
  <tr>
            <th colspan='2' style='text-align:right; vertical-align:top; padding:5px;border-bottom: 1px solid #000;'>Grand Total:</th>
            <th colspan='2' style='text-align:right; vertical-align:top; padding:5px;border-bottom: 1px solid #000;'>{{grandTotal}}</th>
        </tr>
{{/if}}
		</tfoot>
  </table>
  <div style=' text-align:left;font-size: 22px;' >{{billInWords}}</div>
  <br>
    <div style='padding-bottom:10px; margin:0;font-style:italic;font-size: 18px;'>Jai Swaminarayan</div>
</div>

</div> </div>
</tbody>
</table>
</div>
</div>`

  //this.billHtmlTemplate = Handlebars.compile(template);
  this.billHtmlTemplate = Handlebars.compile(htmlbasedtemplate);

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
          this.PrepareSaleInfoList(selProduct, false);
        }

        this.isSalesItemsExists = true;
        this.salesItems.push(this.buildSalesItems(selProduct, false));
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
      this.PrepareSaleInfoList(selProduct, false);
    }

    this.isSalesItemsExists = true;
    this.salesItems.push(this.buildSalesItems(selProduct, false));
    this.UpdateSubGrandTotal();
  }

  ShortProductName(prodname:string){
    return (prodname.substring(0,20));
  }

  PrepareSaleInfoList(selProduct:Products, frmBarcode:boolean){
    if(selProduct !== null){
    let singleSale = new SalesInfo();
      //singleSale.id = selProduct.id;
      singleSale.productCode = selProduct.barcode;
      singleSale.branchId = selProduct.branchId;
      singleSale.productName = selProduct.productName;
      singleSale.shortproductName = this.ShortProductName(selProduct.productName);
      singleSale.quantity = frmBarcode? "1" : this.quantityItems.toString();
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
      //singleSale.dates = new Date(moment().format('DD[/]MM[/]YYYY'));
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

  SetupSalesForm(editSaleitem:SaleDtoArray)
  {
    this.salesForm = this.fb.group({
      salesItems: this.fb.array([])
    });

    // if (editSaleitem !== undefined) {
    //     this.salesForm = this.fb.group({
    //     salesItems: this.fb.array(editSaleitem.saleInfos)
    //   });

    //   editSaleitem.saleInfos.forEach(element => {
    //     this.salesItems = this.buildSalesItems(element);
    //   });

    console.log("Setupsalesform")

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

  buildSalesItems(selProduct:Products, frmBarcode:boolean): FormGroup {
    let qtyItem:number = (frmBarcode) ? 1: this.quantityItems;
    if (selProduct.barcode !== undefined && selProduct.productName !== undefined) {
      return this.fb.group({
                itemName: selProduct.productName,
                itemQty: qtyItem,
                //itemQty: this.quantityItems,
                //itemPrice: this.qtyBtnSelected * selProduct.sellingPrice,
                //itemPrice: this.quantityItems * selProduct.sellingPrice,
                itemPrice: qtyItem * selProduct.sellingPrice,
                itemSellingPrice: selProduct.sellingPrice,
                itembarcode:selProduct.barcode
        });
      }
  }

  //Only Save to DB
  SaveSales(){

    this.loading  = this.loadingCtrl.create({
        content: 'Processing Order..'
      });

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
          this.PrepareSaleInfoList(this.singleFilterProduct, false);
        }

        this.isSalesItemsExists = true;
        this.salesItems.push(this.buildSalesItems(this.singleFilterProduct, false));
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
        // alert('grpLength' + grpbyCounter.length)
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
      document.getElementById("generatedBill").innerHTML = "";
      document.getElementById("generatedBill").innerHTML = task;
      // //console.log(task);
      // //alert('Abt to print' + task.length);
      // this.printSer.printText("jai Swaminarayan", null).then(function(){
      // }).catch(function(err){
      //   alert("Printer Erro! " + err);
      // });

      html2canvas(document.getElementById("generatedBill"), {background: '#fff', width:380}).then((canvas) => {
        //document.body.appendChild(canvas);
        //alert(canvas);
        var imageData = canvas.toDataURL('image/png').replace(/^data:image\/(png|jpg|jpeg);base64,/, "");
           //window.open(imageData);
          this.printSer.printImage(imageData, canvas.width, canvas.height).then(function(){
              }).catch(function(err){
                        alert("Printer Erro! " + err);
      });


    });

      });
  });
  return result;
}

GenerateBillHTML(newBill:BillInfo){
        this.BuildHTMLInvoice();
        var result = this.billHtmlTemplate(newBill);
        //alert('HTML prepared');
        //console.log(result);
        //document.getElementById("generatedBill").innerHTML = result;
        return result;
}

  //Save to DB & Print BILL -- Not used now
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

  //NOt used now
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
    this.SetupSalesForm(undefined);

    this.chkSearchType = true;
    this.searchTypestr = "Search Products..";
    this.productSearchQuery = '';
    this.quantityItems = undefined;
    this.preloadSaleValues();
    this.setQuantityFocus();
    this.isSalesItemsExists = false;
    document.getElementById("generatedBill").innerHTML = "";
    this.LoadEssentials();

  }

  scanBarcode(){
    this.options = {showTorchButton: true };
    BarcodeScanner.scan(this.options).then((barcode) => {
      console.log(barcode);
      this.CheckProductFromBarcode(barcode.text);
    }, (err)=>{
      alert(`Error Scanning: ${err}`);
    });
  }

  CheckProductFromBarcode(barcodestr:string){
    let foundPrd = _.find(this.fullproductslist, { 'barcode': barcodestr }) as Products;
    if (foundPrd !== undefined) {
      //alert('exists ' + foundPrd.productName);
      this.AddProductToSalesBarcode(foundPrd);
    }
    else{
      alert("Does not exists in our database");
    }
  }

  AddProductToSalesBarcode(foundPrd:Products){
    if(this.IsDuplicateProductAdded(foundPrd)){
            this.showAlert("Product already added...");
            return;
          }
        else{
          this.PrepareSaleInfoList(foundPrd, true);
        }

        this.isSalesItemsExists = true;
        this.salesItems.push(this.buildSalesItems(foundPrd, true));
        this.UpdateSubGrandTotal();
  }

  preloadSaleValues(){
    console.log('preloadSaleValues');
    this.validateDate = moment().format('DD[/]MM[/]YYYY');// moment().format('L');
    this.saleBookopt = "Cash Sales";
    this.saleBookoptAbbr = "CS";
    this.payByopt = "cash";
    this.generatedBillNo;
    this.discountper = undefined;
    this.todayDate = moment().format('MM[/]DD[/]YYYY')
    this.saletime = moment().format('LT');
  }

  SalesItemsMoreOptions(index:any){
    console.log(index +  ' INdex');

    let actionSheet = this.actShtCtr.create({
      title: 'Options',
      cssClass: 'action-sheets-basic-page',
      buttons: [
        {
          text: 'Add Qty.',
          icon: 'add',
          handler: () => {
            console.log('Add clicked');
            this.AddQty(index);
          }
        },
        {
          text: 'Reduce Qty.',
          icon: 'remove',
          handler: () => {
            console.log('reduce clicked');
            this.ReduceQty(index);
          }
        },
        {
          text: 'Delete',
          role: 'destructive',
          icon:  'trash',
          handler: () => {
            console.log('Delete clicked');
            this.RemoveItem(index);
          }
        },
        {
          text: 'Cancel',
          role: 'cancel', // will always sort to be on the bottom
          icon: 'close',
          handler: () => {
            console.log('Cancel clicked');
          }
        }
      ]
    });
actionSheet.present();
  }
}
