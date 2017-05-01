import { AppSettings } from './../models/AppSettings';
import { Injectable } from '@angular/core';

@Injectable()
export class Configservice {
private PosApiUrl:string = "";
  constructor() {    
  }

  getPosApiUrl():string{
    let settingsObj = JSON.parse(localStorage.getItem('AppSettings')) as AppSettings;
          if (settingsObj != null) {
              this.PosApiUrl = "http://" + settingsObj.PosApiUrl + "/posapi/";
              //this.PosApiUrl = "http://" + settingsObj.PosApiUrl + "/";
            }
    return this.PosApiUrl;
  }

}
