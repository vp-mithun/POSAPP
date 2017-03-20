import { Users } from './Users';
import { BillInfo } from './BillInfo';
import { SalesInfo } from './SalesInfo';
import * as _ from 'lodash';
/**
 * Class for Preparing Bill, printing
 * GT = Grand Total
 */
export class BillInvoice {
    
    public PrepareBill(saleArry:SalesInfo[], includeGT:boolean){
        let newBill = new BillInfo();

        newBill.counter = saleArry[0].counter;
        newBill.billNo = saleArry[0].billNum;
        newBill.billdate = saleArry[0].dates.toDateString();
        newBill.custName = saleArry[0].customer;
        newBill.billQty = this.GetBillQtySum(saleArry);
        newBill.billSubTotal = this.GetBillSubTotalSum(saleArry);
        newBill.grandTotal = saleArry[0].totalamount;
        newBill.SalesItems = saleArry;
        this.AddUserName(newBill);
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
        let loggedInUser = JSON.parse(localStorage.getItem('loggedUserInfo')) as Users;
        bill.billBy = loggedInUser.userName;
    }    

    ConvertBillAmtInWords(){

    }

    PrepareBillInvoiceFromTemplate(){

    }
}