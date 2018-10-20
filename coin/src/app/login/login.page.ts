import { Component, OnInit } from '@angular/core';
import { HomePage } from '../home/home.page';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

  constructor(public navCtrl: NavController) { }

  ngOnInit() {
  }
  gohome(){
    this.navCtrl.navigateForward("home");
  }

}
