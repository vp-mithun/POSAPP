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
    grandTotal:number;
    SalesItems:SalesInfo[];
}