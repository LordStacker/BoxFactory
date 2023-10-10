import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {RouterLink, RouterLinkActive, RouterModule, RouterOutlet, Routes} from "@angular/router";
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { IonicModule } from '@ionic/angular';
import { CardComponent } from './card/card.component';
import { NavbarComponent } from './navbar/navbar.component';
import { HomeViewComponent } from './home-view/home-view.component';
import { SearchComponent } from './search/search.component';
import { BoxInfoComponent } from './box-info/box-info.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { CreateBoxModalComponent } from './create-box-modal/create-box-modal.component';
import { UpdateBoxModalComponent } from './update-box-modal/update-box-modal.component';


const routes: Routes = [
  { path: '', component: HomeViewComponent },
  { path: 'box-info/:boxId', component: BoxInfoComponent }
];


@NgModule({
  declarations: [
    AppComponent,
    CardComponent,
    NavbarComponent,
    HomeViewComponent,
    SearchComponent,
    BoxInfoComponent,
    CreateBoxModalComponent,
    UpdateBoxModalComponent
  ],
  imports: [
    [RouterModule.forRoot(routes)],
    BrowserModule,
    HttpClientModule,
    IonicModule.forRoot(),
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  exports: [RouterModule]
})
export class AppModule { }
