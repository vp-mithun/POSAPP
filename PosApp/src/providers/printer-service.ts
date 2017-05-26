import { Injectable } from '@angular/core';
declare let DatecsPrinter:any;

@Injectable()
export class PrinterService {

  constructor() {
    console.log('Hello PrinterService Provider');
  } 

  public listBluetoothDevices(){
    return new Promise((resolve, reject) => {
        DatecsPrinter.listBluetoothDevices(
          function (success) {
            resolve(success);
          },
          function (error) {
            reject(error);
          });          
    });
  }

  public connectToBlueToothDevice(deviceAddress:string)
  {
    return new Promise((resolve, reject) => {
        DatecsPrinter.connect(deviceAddress,
          function (success) {
            console.log('Device Conn '+ deviceAddress);            
            resolve(success);
          },
          function (error) {
            reject(error);
          });
    });
  }

  public feedPaper(lines:number)
  {
    return new Promise((resolve, reject) => {
        DatecsPrinter.feedPaper(lines,
          function (success) {            
            resolve(success);
          },
          function (error) {
            reject(error);
          });
    });
  }

  public printText (billToPrint:string, charset:string)
  {
     if (charset == null) {
        charset = 'ISO-8859-1';
      }
    return new Promise((resolve, reject) => {
        DatecsPrinter.printText(billToPrint,charset,
          function (success) {            
            DatecsPrinter.feedPaper(5,
          function (success) {            
            resolve(success);
          },
          function (error) {
            reject(error);
          });
            //resolve(success);
          },
          function (error) {
            reject(error);
          });
    });
  }

  public printImage (image2Print:any, cwidth:number, cheight:number)
  {
    return new Promise((resolve, reject) => {
        DatecsPrinter.printImage(image2Print,cwidth,cheight,
          1,        
          function (success) {            
            DatecsPrinter.feedPaper(5,
          function (success) {            
            resolve(success);
          },
          function (error) {
            reject(error);
          });
            //resolve(success);
          },
          function (error) {
            reject(error);
          });
    });
  }

}

