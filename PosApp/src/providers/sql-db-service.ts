import { Injectable } from '@angular/core';
import { SQLite } from 'ionic-native';

@Injectable()
export class SqlDbService {

  private storage: SQLite;
    private isOpen: boolean;

  constructor() {
    if(!this.isOpen) {
            this.storage = new SQLite();
            this.storage.openDatabase({name: "data.db", location: "default"}).then(() => {
                this.storage.executeSql("CREATE TABLE IF NOT EXISTS posettings (id INTEGER PRIMARY KEY AUTOINCREMENT, settingname TEXT, settingvalue TEXT)", []);
                this.isOpen = true;
            });
        }
    }

    public createSettings(sName: string, sValue: string) {
        return new Promise((resolve, reject) => {
            this.storage.executeSql("INSERT INTO posettings (settingname, settingvalue) VALUES (?, ?)", [sName, sValue]).then((data) => {
                resolve(data);
            }, (error) => {
                reject(error);
            });
        });
    }

    public getSettings() {
        return new Promise((resolve, reject) => {
            this.storage.executeSql("SELECT * FROM possettings", []).then((data) => {
                let posSettingslist = [];
                if(data.rows.length > 0) {
                    for(let i = 0; i < data.rows.length; i++) {
                        posSettingslist.push({
                            id: data.rows.item(i).id,
                            sname: data.rows.item(i).settingname,
                            svalue: data.rows.item(i).settingvalue
                        });
                    }
                }
                resolve(posSettingslist);
            }, (error) => {
                reject(error);
            });
        });
    }
}
