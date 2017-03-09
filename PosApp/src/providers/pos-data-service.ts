import { Salebook } from './../models/Salebook';
import { Products } from './../models/Products';
import { Users } from './../models/Users';
import { AuthService } from './auth-service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class PosDataService {
PosApiUrl:string;

  constructor(public http: Http, private authServ:AuthService) {
      this.PosApiUrl = "http://192.168.194.2/PosApi/";
  }

  //Gets Users data based on ID
  getUserDetails(): Observable<Users> {
        // add authorization header with jwt token
        let headers = new Headers({ 'Authorization': 'Bearer ' + this.authServ.token });
        let options = new RequestOptions({ headers: headers });
 
        // get users from api
        return this.http.get(this.PosApiUrl + 'api/user/' + this.authServ.loggedUserId, options)
            .map((response: Response) => response.json() as Users);
    }

    //Gets list of PRODCUTS based on Loggedin User ShopId & BranchId
    getProductList(): Observable<Products[]> {
        // add authorization header with jwt token
        let headers = new Headers({ 'Authorization': 'Bearer ' + this.authServ.token });
        let options = new RequestOptions({ headers: headers });
        let userinfo = JSON.parse(localStorage.getItem("loggedUserInfo"));

        let querystr = this.PosApiUrl + "api/products?branchid="+userinfo.branchId+"&shopid="+userinfo.shopId; 
        
        return this.http.get(querystr, options)
            .map((response: Response) => response.json() as Products[]);
    }

    //Gets list of SaleBook based on Loggedin User ShopId & BranchId
    getSaleBookList(): Observable<Salebook[]> {
        // add authorization header with jwt token
        let headers = new Headers({ 'Authorization': 'Bearer ' + this.authServ.token });
        let options = new RequestOptions({ headers: headers });
        let userinfo = JSON.parse(localStorage.getItem("loggedUserInfo"));

        let querystr = this.PosApiUrl + "api/salebook?branchid="+userinfo.branchId+"&shopid="+userinfo.shopId; 
        
        return this.http.get(querystr, options)
            .map((response: Response) => response.json() as Salebook[]);
    }
}
