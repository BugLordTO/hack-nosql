import { GlobalVarible } from './../models';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { NavController } from '@ionic/angular';
import { BuyingPage } from '../buying/buying.page';


@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss']
})
export class HomePage {
  data: any = [];
  constructor(public navCtrl: NavController, private http: HttpClient) {
  }

  buying() {
    this.navCtrl.navigateForward("buying");
  }
  selling() {
    this.navCtrl.navigateForward("selling");
  }

  ionViewDidEnter() {

    let url = GlobalVarible.host + "/GetCoinPrice";
    console.log("Url: " + url);

    this.http.get(url).subscribe((response) => {
      this.data = response;
      console.log("Data: " + JSON.stringify(this.data));
    });
  }


}
