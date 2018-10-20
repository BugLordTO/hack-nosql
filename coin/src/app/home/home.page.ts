import { Component } from '@angular/core';
import { NavController } from '@ionic/angular';
import { BuyingPage } from '../buying/buying.page';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss']
})
export class HomePage {
  constructor(public navCtrl: NavController) {
  }

  buying() {
    this.navCtrl.navigateForward("buying");
  }
  selling() {
    this.navCtrl.navigateForward("selling");
  }


}
