import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {RouterLink, RouterLinkActive, RouterModule, RouterOutlet, Routes} from "@angular/router";
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { IonicModule } from '@ionic/angular';
import { CardComponent } from './card/card.component';
import { NavbarComponent } from './navbar/navbar.component';
import { HomeViewComponent } from './home-view/home-view.component';
import { LoginViewComponent } from './login-view/login-view.component';
import { SearchComponent } from './search/search.component';


const routes: Routes = [
  { path: '', component: HomeViewComponent },
];


@NgModule({
  declarations: [
    AppComponent,
    CardComponent,
    NavbarComponent,
    HomeViewComponent,
    LoginViewComponent,
    SearchComponent
  ],
  imports: [
    [RouterModule.forRoot(routes)],
    BrowserModule,
    HttpClientModule,
    IonicModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent],
  exports: [RouterModule]
})
export class AppModule { }
