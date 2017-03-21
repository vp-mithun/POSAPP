import { StoreInfo } from './StoreInfo';
import { Users } from './Users';
import { BillInfo } from './BillInfo';
import { SalesInfo } from './SalesInfo';
import * as _ from 'lodash';
import { Http, Headers, RequestOptions, Response } from '@angular/http';

/**
 * Class for Preparing Bill, printing
 * GT = Grand Total
 */
export class BillInvoice {        
    
    public PrepareBill(saleArry:SalesInfo[], includeGT:boolean):BillInfo{
        let newBill = new BillInfo();
        
        var converter = require('number-to-words');
        //console.log("Rs. " + converter.toWords(289) + " only" );

        newBill.counter = saleArry[0].counter;
        newBill.billNo = saleArry[0].billNum;
        newBill.billdate = saleArry[0].dates.toString();
        newBill.custName = saleArry[0].customer;
        newBill.billQty = this.GetBillQtySum(saleArry);
        newBill.billSubTotal = this.GetBillSubTotalSum(saleArry);
        newBill.grandTotal = saleArry[0].totalamount;
        newBill.SalesItems = saleArry;
        newBill.billInWords = "Rs. " + converter.toWords(newBill.billSubTotal) + " only"
        this.AddUserName(newBill);
        this.AddStoreInfo(newBill);
        newBill.SalesItems = saleArry;

        return newBill;
    }

    GetBillQtySum(salrArry:SalesInfo[]):number{
        let qty:number = 0;
        qty = _.sumBy(salrArry, 'quantity');
        return qty;
    }

    GetBillSubTotalSum(salrArry:SalesInfo[]):number{
        let subtotal:number = 0;
        subtotal = _.sumBy(salrArry, 'amount');
        return subtotal;
    }

    AddUserName(bill:BillInfo){
        let loggedInUser = JSON.parse(localStorage.getItem('loggedUserInfo')) as Users;
        bill.billBy = loggedInUser.userName;
    }

    AddStoreInfo(bill:BillInfo){
        let loggedInStore = JSON.parse(localStorage.getItem('loggedUserStoreInfo')) as StoreInfo;
        bill.storeName = loggedInStore.shopName;
        bill.storeLoc = loggedInStore.location;
    }

    PrepareBillInvoiceFromTemplate(billItem:BillInfo){

        var mu = require('mu2'); 
        var fs = require('file-system');

        fs.readFile(__dirname + '/template/billInvoiceTemplate.html', function (err, data) {
              if (err) throw err;
                mu.compileAndRender(data.toString(), JSON.stringify(billItem))
                          .on('data', function (dataitem) {
                                                console.log(dataitem.toString());
                                                              });                
            });

        // mu.root = __dirname + '/src/models/'
        // mu.compileAndRender('billInvoiceTemplate.html', JSON.stringify(billItem))
        //   .on('data', function (data) {
        //           console.log(data.toString());
        //           });
    }
}