import { Products } from './../models/Products';
import { Users } from './../models/Users';
import { AuthService } from './auth-service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class PosDataService {

  constructor(public http: Http, private authServ:AuthService) {
    console.log('Hello PosDataService Provider');
  }

  //Gets Users data based on ID
  getUserDetails(): Observable<Users> {
        // add authorization header with jwt token
        let headers = new Headers({ 'Authorization': 'Bearer ' + this.authServ.token });
        let options = new RequestOptions({ headers: headers });
 
        // get users from api
        return this.http.get('http://localhost:5000/api/user/' + this.authServ.loggedUserId, options)
            .map((response: Response) => response.json() as Users);
    }

    //Gets list of PRODCUTS based on Loggedin User ShopId & BranchId
    getProductList(): Observable<Products[]> {
        // add authorization header with jwt token
        let headers = new Headers({ 'Authorization': 'Bearer ' + this.authServ.token });
        let options = new RequestOptions({ headers: headers });
        let userinfo = JSON.parse(localStorage.getItem("loggedUserInfo"));

        let querystr = "http://localhost:5000/api/products?branchid="+userinfo.branchId+"&shopid="+userinfo.shopId; 
        
        return this.http.get(querystr, options)
            .map((response: Response) => response.json() as Products[]);
    }
}
