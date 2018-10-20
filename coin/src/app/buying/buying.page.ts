import { Component, OnInit } from '@angular/core';
import { NavParams, NavController } from '@ionic/angular';

@Component({
  selector: 'app-buying',
  templateUrl: './buying.page.html',
  styleUrls: ['./buying.page.scss'],
})
export class BuyingPage implements OnInit {

  constructor(public navCtrl: NavController) {
   // console.log(this.navParam.get("id"));
   }

  ngOnInit() {
  }
  gohome(){
    this.navCtrl.navigateBack("");
  }

}
