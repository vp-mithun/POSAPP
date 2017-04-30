import { BillInfo } from './../models/BillInfo';
import { Injectable } from '@angular/core';
import { Http, ResponseContentType, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

@Injectable()
export class InvoiceGenerator {

  constructor(public http: Http) {
    console.log('Hello InvoiceGenerator Provider');
  }

  public buildInvoiceFromTemplate():Observable<string>{
    return this.http.get('/template/billInvoiceTemplate.html').map((response: Response) => response.text() as string);
  }
}
