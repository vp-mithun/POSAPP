import { BillInfo } from './../models/BillInfo';
import { Injectable } from '@angular/core';
import { Http, ResponseContentType, RequestOptions, Response } from '@angular/http';
import 'rxjs/add/operator/map';
var Mustache = require('Mustache'); 

@Injectable()
export class InvoiceGenerator {

  constructor(public http: Http) {
    console.log('Hello InvoiceGenerator Provider');
  }

  public buildInvoiceFromTemplate(billItem:BillInfo){


    this.http.get('/template/billInvoiceTemplate.html').map(function(response){
       //do something
       return response.text()
       
    }).subscribe(data => {
      //console.log(cc + "----");
      
      // mu.compileAndRender(data.toString(), JSON.stringify(billItem))
      //                     .on('data', function (dataitem) {
      //                                           console.log(dataitem.toString());
      //                                                         });

      var html = Mustache.to_html(data, billItem);
      
    });
  }

//   private extractContent(res: Response) {
//     //let blob: Blob = res.blob();   
//     console.log(res);
    
// }

}
