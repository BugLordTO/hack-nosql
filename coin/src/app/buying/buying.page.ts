import { Component, OnInit } from '@angular/core';
import { NavParams } from '@ionic/angular';

@Component({
  selector: 'app-buying',
  templateUrl: './buying.page.html',
  styleUrls: ['./buying.page.scss'],
})
export class BuyingPage implements OnInit {

  constructor(private navParam: NavParams) {
    console.log(this.navParam.get("id"));
   }

  ngOnInit() {
  }

}
