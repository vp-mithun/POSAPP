import { UserPermissions } from './../models/UserPermissions';
import { AppSettings } from './../models/AppSettings';
import { Injectable } from '@angular/core';
import * as _ from 'lodash';

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

  //Looks for User Permissions, returns TRUE if exists
  checkForPermissions(perName:string):boolean{
    let isPermissionExists:boolean = false;

    let userpermission = JSON.parse(localStorage.getItem("loggedUserPermission")) as UserPermissions;
    if(userpermission !== null){
      if (_.includes(userpermission[0].permissionsList, perName)) {
        isPermissionExists = true;
      }
    }
    return isPermissionExists;
  }
}
