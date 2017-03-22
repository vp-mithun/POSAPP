import { SalesInfo } from './SalesInfo';

/**
 * name
 */
export class BillInfo {    
    storeName:string;
    storeLoc:string;
    counter:number;
    billNo:string;
    billdate:string;
    custName:string;
    billInWords:string;
    billBy:string;
    billQty:number;
    billSubTotal:number;
    isGrandTotal:boolean;
    grandTotal:number;
    isDiscountApplies:boolean;
    isTaxable:boolean = false;
    amtDiscounted:number;
    discPert:number;
    taxapplied:number;
    SalesItems:SalesInfo[];
}