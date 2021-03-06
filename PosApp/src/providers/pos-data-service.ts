import { UserPermissions } from './../models/UserPermissions';
import { Configservice } from './configservice';
import { AppSettings } from './../models/AppSettings';
import { StoreInfo } from './../models/StoreInfo';
import { SalesInfo, SaleDtoArray } from './../models/SalesInfo';
import { Salebook } from './../models/Salebook';
import { Products } from './../models/Products';
import { Users } from './../models/Users';
import { AuthService } from './auth-service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import 'rxjs/add/operator/map';
import * as moment from 'moment';


@Injectable()
export class PosDataService {
PosApiUrl:string;

  constructor(public http: Http, private authServ:AuthService, private confgSrv:Configservice) {
      //this.PosApiUrl = "http://192.168.194.2/PosApi/";
      //this.PosApiUrl = "http://localhost:5000/";
  }

  //Gets Users data based on ID
  getUserDetails(): Observable<Users> {
        // add authorization header with jwt token
        let headers = new Headers({ 'Authorization': 'Bearer ' + this.authServ.token });
        let options = new RequestOptions({ headers: headers });

        // get users from api
        let url = this.confgSrv.getPosApiUrl();
        return this.http.get(url + 'api/user/' + this.authServ.loggedUserId, options)
            .map((response: Response) => response.json() as Users);
    }

    //Gets list of PRODCUTS based on Loggedin User ShopId & BranchId
    getProductList(): Observable<Products[]> {
        // add authorization header with jwt token
        let headers = new Headers({ 'Authorization': 'Bearer ' + this.authServ.token });
        let options = new RequestOptions({ headers: headers });
        let userinfo = JSON.parse(localStorage.getItem("loggedUserInfo"));

        //let querystr = this.PosApiUrl + "api/products?branchid="+userinfo.branchId+"&shopid="+userinfo.shopId;
        let querystr = this.confgSrv.getPosApiUrl() + "api/products?branchid="+userinfo.branchId+"&shopid="+userinfo.shopId;
        return this.http.get(querystr, options)
            .map((response: Response) => response.json() as Products[]);
    }

    //Gets list of SaleBook based on Loggedin User ShopId & BranchId
    getSaleBookList(): Observable<Salebook[]> {
        // add authorization header with jwt token
        let headers = new Headers({ 'Authorization': 'Bearer ' + this.authServ.token });
        let options = new RequestOptions({ headers: headers });
        let userinfo = JSON.parse(localStorage.getItem("loggedUserInfo"));

        //        let querystr = this.PosApiUrl + "api/salebook?branchid="+userinfo.branchId+"&shopid="+userinfo.shopId;
        let querystr = this.confgSrv.getPosApiUrl() + "api/salebook?branchid="+userinfo.branchId+"&shopid="+userinfo.shopId;

        return this.http.get(querystr, options)
            .map((response: Response) => response.json() as Salebook[]);
    }

    saveSalesListDB(tosaveSalesList:SalesInfo[]):Observable<string>{
        let billNum:string = "";
        //let headers = new Headers([{ 'Authorization': 'Bearer ' + this.authServ.token,
        //'Content-Type': 'application/json', "Accept" : "application/json" }]);

        let headers:Headers = new Headers();
        headers.append('Authorization', 'Bearer ' + this.authServ.token);
        headers.append('Content-Type', 'application/json;charset=utf-8');
        headers.append("Accept",  "application/json");
        let body = JSON.stringify(tosaveSalesList);

        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.confgSrv.getPosApiUrl() + 'api/sales',
        body,options)
            .map((response: Response) => {
                console.log(response);
                if (response.status == 200) {
                    billNum = response.text();
                }
            return billNum
        });
}

returnSalesDB(toreturnSalesList:SalesInfo[]):Observable<string>{
        let billNum:string = "";
        //let headers = new Headers([{ 'Authorization': 'Bearer ' + this.authServ.token,
        //'Content-Type': 'application/json', "Accept" : "application/json" }]);

        let headers:Headers = new Headers();
        headers.append('Authorization', 'Bearer ' + this.authServ.token);
        headers.append('Content-Type', 'application/json;charset=utf-8');
        headers.append("Accept",  "application/json");
        let body = JSON.stringify(toreturnSalesList);

        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.confgSrv.getPosApiUrl() + 'api/sales/ReturnSale',
        body,options)
            .map((response: Response) => {
                console.log(response);
                if (response.status == 200) {
                    billNum = response.text();
                }
            return billNum
        });
}

    countOfSalesForDay():Observable<number>{
        let headers = new Headers({ 'Authorization': 'Bearer ' + this.authServ.token });
        let options = new RequestOptions({ headers: headers });
        let userinfo = JSON.parse(localStorage.getItem("loggedUserInfo"));
        let saleDate = moment().format('L');

        let querystr = this.confgSrv.getPosApiUrl() + "api/sales/GetMaxSaleCount?branchid="+userinfo.branchId+"&shopid="+
                        userinfo.shopId+"&userId="+userinfo.id+"&sdate="+saleDate;

        return this.http.get(querystr, options)
            .map((response: Response) => response.json() as number);
    }

    //Gets Store data based on shopID & branchId
  getStoreDetails(): Observable<StoreInfo> {
        // add authorization header with jwt token
        let headers = new Headers({ 'Authorization': 'Bearer ' + this.authServ.token });
        let options = new RequestOptions({ headers: headers });

        let userinfo = JSON.parse(localStorage.getItem("loggedUserInfo"));

        //let querystr = this.PosApiUrl + "api/shopdetails?branchid="+userinfo.branchId+"&shopid="+userinfo.shopId;
        let querystr = this.confgSrv.getPosApiUrl() + "api/shopdetails?branchid="+userinfo.branchId+"&shopid="+userinfo.shopId;
        return this.http.get(querystr, options)
            .map((response: Response) => response.json() as StoreInfo);
    }

    //Gets list of Sales based on Loggedin User ShopId & BranchId and Date
    getMySalesForDay(searchdate:any): Observable<SaleDtoArray[]> {
        // add authorization header with jwt token
        let headers = new Headers({ 'Authorization': 'Bearer ' + this.authServ.token });
        let options = new RequestOptions({ headers: headers });
        let userinfo = JSON.parse(localStorage.getItem("loggedUserInfo"));
        //let saleDate = moment().format('L');
        let saleDate = moment(searchdate).format('MM[/]DD[/]YYYY');

        let querystr = this.confgSrv.getPosApiUrl() + "api/sales/GetMySalesForDay?branchid="+userinfo.branchId+"&shopid="+
                        userinfo.shopId+"&userId="+userinfo.id+"&sdate="+saleDate;
        return this.http.get(querystr, options)
            .map((response: Response) => response.json() as SaleDtoArray[]);
    }

    //Gets User Permissions based on Logged in User
    getUserPermissions(userId:number):Observable<UserPermissions>{
      // add authorization header with jwt token
        let headers = new Headers({ 'Authorization': 'Bearer ' + this.authServ.token });
        let options = new RequestOptions({ headers: headers });

        // get users from api
        let url = this.confgSrv.getPosApiUrl();
        return this.http.get(url + 'api/permission/' + this.authServ.loggedUserId, options)
            .map((response: Response) => response.json() as UserPermissions);
    }
}
