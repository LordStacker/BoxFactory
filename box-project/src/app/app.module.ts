import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {RouterLink, RouterLinkActive, RouterModule, RouterOutlet, Routes} from "@angular/router";

import { AppComponent } from './app.component';
import { IonicModule } from '@ionic/angular';
import { CardComponent } from './card/card.component';
import { NavbarComponent } from './navbar/navbar.component';


const routes: Routes = [
  { path: 'home', component: CardComponent },
];


@NgModule({
  declarations: [
    AppComponent,
    CardComponent,
    NavbarComponent
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
