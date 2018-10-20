import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: '', loadChildren: './tabs/tabs.module#TabsPageModule' },
  { path: 'login', loadChildren: './login/login.module#LoginPageModule' },
  { path: 'buying', loadChildren: './buying/buying.module#BuyingPageModule' },
  { path: 'buying/:id', loadChildren: './buying/buying.module#BuyingPageModule' },
  { path: 'selling', loadChildren: './selling/selling.module#SellingPageModule' },
  { path: 'update', loadChildren: './update/update.module#UpdatePageModule' }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
