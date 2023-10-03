import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {RouterLink, RouterLinkActive, RouterModule, RouterOutlet, Routes} from "@angular/router";

import { AppComponent } from './app.component';
import { IonicModule } from '@ionic/angular';
import { CardComponent } from './card/card.component';
import { NavbarComponent } from './navbar/navbar.component';
import { HomeViewComponent } from './home-view/home-view.component';
import { SearchViewComponent } from './search-view/search-view.component';
import { LoginViewComponent } from './login-view/login-view.component';
import { BoxViewComponent } from './box-view/box-view.component';


const routes: Routes = [
  { path: 'home', component: CardComponent },
];


@NgModule({
  declarations: [
    AppComponent,
    CardComponent,
    NavbarComponent,
    HomeViewComponent,
    SearchViewComponent,
    LoginViewComponent,
    BoxViewComponent
  ],
  imports: [
    [RouterModule.forRoot(routes)],
    BrowserModule,
    IonicModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent],
  exports: [RouterModule]
})
export class AppModule { }
