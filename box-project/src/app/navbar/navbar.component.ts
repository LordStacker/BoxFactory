import { Component } from '@angular/core';
import { ModalController } from '@ionic/angular';
import {CreateBoxModalComponent} from "../create-box-modal/create-box-modal.component";


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  constructor(private modalController: ModalController) {}

  async openCreateBoxModal() {
    const modal = await this.modalController.create({
      component: CreateBoxModalComponent, // Use the modal component you created
    });

    await modal.present();
  }
}
