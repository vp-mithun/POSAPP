import { SQLite } from 'ionic-native';
import { PrinterService } from './../../providers/printer-service';
import { AppSettings } from './../../models/AppSettings';
import { AuthService } from './../../providers/auth-service';
import { LoginPage } from './../login/login';
import { Component } from '@angular/core';
import { NavController, NavParams, LoadingController, ToastController } from 'ionic-angular';
declare let BTPrinter: any;
declare let DatecsPrinter:any;

@Component({
  selector: 'page-settings',
  templateUrl: 'settings.html'
})
export class SettingsPage {
  APIurl:string = "";   
  printerList:any;
  selectedPrinterName:string;
  sqlstorage: SQLite;

  settings:AppSettings = new AppSettings();

  constructor(public navCtrl: NavController, 
              public navParams: NavParams, 
              private authenticationService: AuthService,
              public loadingCtrl:LoadingController,
              public toastCtrl:ToastController,
              public printSer:PrinterService) {

                //  this.printerList = new Array<string>();
                //  this.printerList.push('a');
                //  this.printerList.push('b');
              }

  ionViewDidLoad() {
    console.log('ionViewDidLoad SettingsPage');
    let settingsObj = JSON.parse(localStorage.getItem('AppSettings')) as AppSettings;
    if (settingsObj != null) {
      this.APIurl = settingsObj.PosApiUrl;      
    }

    //this.LoadPrinterList();  
  }

  doLogout(){
    this.authenticationService.logout();
    this.navCtrl.setRoot(LoginPage);
  }

  updateUrl(){
    if (this.APIurl !== "") {
      let loading  = this.loadingCtrl.create({
        content: 'Connecting...'        
      });
      loading.present();

      //localStorage.setItem('AppSettings', JSON.stringify(this.loggedInUser));      
      this.authenticationService.CheckConnection(this.APIurl).subscribe((resp)=>{
        if(resp === true){
           this.settings.PosApiUrl = this.APIurl;
           localStorage.setItem('AppSettings', JSON.stringify(this.settings));
//           this.AddApiUrl(this.APIurl);
           loading.dismiss();
           alert('Connected');
        }  
      }, (err) => {      
        // Unable to log in
        loading.dismiss();
        let toast = this.toastCtrl.create({
          message: err,
          duration: 2000,
          position: 'middle'        
        });
        toast.present();
      });
    }
  }

  public AddApiUrl(APIurl:string) {
      
    }

  LoadPrinterList(){
    this.printSer.listBluetoothDevices().then(result => {
      console.log(JSON.stringify(result));
      this.printerList = result;
    }).catch(err => {

    });
  }

  setPrinterName(){
    if(this.selectedPrinterName != null){
      let settingsObj = JSON.parse(localStorage.getItem('AppSettings')) as AppSettings;
      if (settingsObj != null) {
        settingsObj.printerName = this.selectedPrinterName;
        this.settings = settingsObj;
        localStorage.setItem('AppSettings', JSON.stringify(this.settings));
      } 
      else{
        this.settings.printerName = this.selectedPrinterName;;
        localStorage.setItem('AppSettings', JSON.stringify(this.settings));
      }
    }
  }

}
