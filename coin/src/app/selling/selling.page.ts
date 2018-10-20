import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-selling',
  templateUrl: './selling.page.html',
  styleUrls: ['./selling.page.scss'],
})
export class SellingPage implements OnInit {

  constructor(public navCtrl: NavController) { }

  ngOnInit() {
  }
  
  home(){
    this.navCtrl.navigateBack("home");
  }

}
