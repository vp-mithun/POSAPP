import { Configservice } from './../../providers/configservice';
import { SqlDbService } from './../../providers/sql-db-service';
import { SQLite } from 'ionic-native';
import { SettingsPage } from './../settings/settings';
import { AuthService } from './../../providers/auth-service';
import { HomePage } from './../home/home';
import { Component } from '@angular/core';
import { NavController, ToastController, LoadingController, Platform } from 'ionic-angular';
import { FormBuilder,FormGroup,Validators } from '@angular/forms';

@Component({
  selector: 'page-login',
  templateUrl: 'login.html'
})
export class LoginPage {
  loginform:FormGroup;
  sqlstorage: SQLite;
  items: Array<Object>;
  //loginModel:any;
  loginModel: {username: string, password: string} = {
    username: '',
    password: ''
  };

  constructor(public navCtrl: NavController,
              public loadingCtrl: LoadingController,
              public toastCtrl: ToastController,
              private authenticationService: AuthService,
              private fb: FormBuilder,
              private platform: Platform,
              private sqlDB:SqlDbService,
              private confSrv:Configservice
              ) {
                this.loginform = this.fb.group({
                  username: [this.loginModel.username, [Validators.required]],
                  password: [this.loginModel.password, [Validators.required]]      
    });

    platform.ready()
		.then(() => {
      //this.sqlDB.init();
      // this.sqlDB.getSettings().then((data)=>{
      //   console.log(JSON.stringify(data));      
      // });
    });
	}

  ionViewDidLoad() {    
    console.log('Login Load');
    
    
  }

  onSubmit({value,valid}: {value: any,valid: boolean}) {

    let url = this.confSrv.getPosApiUrl();
    if(url == ""){
      alert("Configure Settings missing");
      return;
    }

    //this.navCtrl.setRoot(HomePage);
    let loading  = this.loadingCtrl.create({
        content: 'Please wait...'        
      });
      loading.present();

    

    if (value.username == "bapsadmin" &&  value.password == "admin123") {
      loading.dismiss();
      this.navCtrl.setRoot(SettingsPage);
    }
    else{
      this.authenticationService.login(value.username, value.password).subscribe((resp) => {      
        loading.dismiss();
        //console.log(resp);
        if (resp === true) {
                      // login successful
                      this.navCtrl.setRoot(HomePage);
                  } else {
                      // login failed
                      // Unable to log in
                      loading.dismiss();
                      let toast = this.toastCtrl.create({
                        message: "Login Failed",
                        duration: 3000,
                        position: 'middle'        
                      });
                      toast.present();
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
}
