import { AppSettings } from './../../models/AppSettings';
import { AuthService } from './../../providers/auth-service';
import { LoginPage } from './../login/login';
import { Component } from '@angular/core';
import { NavController, NavParams, LoadingController, ToastController } from 'ionic-angular';
declare let BTPrinter: any;

@Component({
  selector: 'page-settings',
  templateUrl: 'settings.html'
})
export class SettingsPage {
  APIurl:string = "";   
  printerList:Array<string>;
  selectedPrinterName:string;

  settings:AppSettings = new AppSettings();

  constructor(public navCtrl: NavController, 
              public navParams: NavParams, 
              private authenticationService: AuthService,
              public loadingCtrl:LoadingController,
              public toastCtrl:ToastController) {

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
        content: 'Please wait...'        
      });
      loading.present();

      //localStorage.setItem('AppSettings', JSON.stringify(this.loggedInUser));      
      this.authenticationService.CheckConnection(this.APIurl).subscribe((resp)=>{
        if(resp === true){
           this.settings.PosApiUrl = this.APIurl;
           localStorage.setItem('AppSettings', JSON.stringify(this.settings));
           loading.dismiss();
           alert("Sucessfully Connected");
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

  LoadPrinterList(){
    BTPrinter.list((data) => {
			console.log('Printers list:', data);
      console.log(data + '--' + data[0]);
      //alert(data[0]);      
      this.printerList = data;

		}, (err) => {      
			alert(`Error: ${err}`);
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
