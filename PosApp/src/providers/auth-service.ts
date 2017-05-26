import { Configservice } from './configservice';
import { AppSettings } from './../models/AppSettings';
import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Storage } from '@ionic/storage';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Observable } from 'rxjs';

@Injectable()
export class AuthService {
  public token: string;
  public loggedUserId: number;
  public PosApiUrl:string;

  constructor(public http: Http, private confgSrv:Configservice) {

    //this.PosApiUrl = "http://192.168.194.2/PosApi/";
    //this.PosApiUrl = "http://localhost:5000/";
  }

  login(username: string, password: string): Observable<boolean> {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    console.log('Inside loggin');

        let url = this.confgSrv.getPosApiUrl() + "api/auth/token";
        return this.http.post(url,
        JSON.stringify({ username: username, password: password }),options)
            .map((response: Response) => {
                // login successful if there's a jwt token in the response
                console.log(response);

                let token = response.json() && response.json().token;
                let loggeduserid = response.json() && response.json().loggeduserid;
                if (token) {
                    // set token & UserId property
                    this.token = token;
                    this.loggedUserId = loggeduserid;

                    // store username and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify({ username: username, token: token }));

                    // return true to indicate successful login
                    return true;
                } else {
                    // return false to indicate failed login
                    return false;
                }
            })
        .catch(this.handleError);
    }

    logout(): void {
        // clear token remove user from local storage to log user out
        this.token = null;
        localStorage.removeItem('currentUser');
    }

    handleError(error: any) {
        return Observable.throw('Server error ' + error);
    }


  public CheckConnection(Appurl:string) : Observable<boolean>{
      let headers = new Headers({ 'Content-Type': 'application/json' });
      let options = new RequestOptions({ headers: headers });

    //let url = "http://" + Appurl +"/api/values";
    let url = "http://" + Appurl +"/posapi/api/values";

      return this.http.post(url,options)
            .map((response: Response) => {
                if(response.status == 200)
                {
                    return true;
                }
                return false;

            }).catch(this.handleError);
  }
}
