import { StoreInfo } from './StoreInfo';
import { Users } from './Users';
import { BillInfo } from './BillInfo';
import { SalesInfo } from './SalesInfo';
import * as _ from 'lodash';
import * as moment from 'moment';

/**
 * Class for Preparing Bill, printing
 * GT = Grand Total
 */
export class BillInvoice {        
    
    public PrepareBill(saleArry:SalesInfo[], includeGT:boolean):BillInfo{
        let newBill = new BillInfo();
        
        var converter = require('number-to-words');       

        newBill.counter = saleArry[0].counter;
        newBill.billNo = saleArry[0].billnum;
        newBill.billdate = saleArry[0].dates.toString();
        newBill.billTime = moment().format('LT');
        newBill.custName = saleArry[0].customer;
        newBill.billQty = this.GetBillQtySum(saleArry);
        newBill.billSubTotal = this.GetBillSubTotalSum(saleArry);
        newBill.grandTotal = saleArry[0].totalamount;
        newBill.SalesItems = saleArry;
        newBill.billInWords = "Rs." + converter.toWords(newBill.grandTotal) + " only"
        this.AddUserName(newBill);
        this.AddStoreInfo(newBill);
        //Check if Discount applied
        if(saleArry[0].discount != undefined && saleArry[0].discount > 0){
            newBill.isDiscountApplies = true;
            //newBill.amtDiscounted = saleArry[0].discountamt;
            newBill.discPert = saleArry[0].discount;
        }        
        newBill.SalesItems = saleArry;
        newBill.isGrandTotal = includeGT;

        return newBill;
    }

    GetBillQtySum(salrArry:SalesInfo[]):number{
        let qty:number = 0;
        qty = _.sumBy(salrArry, function(o) { return parseFloat(o.quantity); });
        //qty = _.sumBy(salrArry, 'quantity');
        return qty;
    }

    GetBillSubTotalSum(salrArry:SalesInfo[]):number{
        let subtotal:number = 0;
        subtotal = _.sumBy(salrArry, function(o) { return parseFloat(o.amount); });
        //subtotal = _.sumBy(salrArry, 'amount');
        return subtotal;
    }

    AddUserName(bill:BillInfo){
        let loggedInUser = JSON.parse(localStorage.getItem('loggedUserInfo')) as Users;
        bill.billBy = loggedInUser.employeeName;
    }

    AddStoreInfo(bill:BillInfo){
        let loggedInStore = JSON.parse(localStorage.getItem('loggedUserStoreInfo')) as StoreInfo;
        bill.storeName = loggedInStore.shopName;
        bill.storeLoc = loggedInStore.location;
    }    
}