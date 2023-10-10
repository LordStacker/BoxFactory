import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CardFilterService } from '../services/card-filter.service';
import {ModalController, NavController} from "@ionic/angular";
import {CreateBoxModalComponent} from "../create-box-modal/create-box-modal.component";
import {UpdateBoxModalComponent} from "../update-box-modal/update-box-modal.component";

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent implements OnInit {
  cards: any[] = [];
  filteredCards: any[] = [];
  chunkedCards: any[][] = [];


  constructor(
    private http: HttpClient,
    private cardFilterService: CardFilterService,
    private navCtrl: NavController,
    private modalController: ModalController
  ) {}

  ngOnInit() {
    this.loadCards();
    this.http.get<any>('http://localhost:5000/boxes').subscribe(data => {
      this.cards = data;
      this.chunkedCards = this.chunk(this.cards, 3);
    });

    this.cardFilterService.getSearchQuery().subscribe(searchTerm => {
      this.filterCards(searchTerm);
    });
  }

  loadCards() {
    this.http.get<any>('http://localhost:5000/boxes').subscribe(data => {
      this.cards = data;
      this.filteredCards = [...this.cards]; // Initialize filteredCards with all cards
      this.chunkedCards = this.chunk(this.filteredCards, 3);
    });
  }

  filterCards(searchTerm: string) {
    if (searchTerm.trim() === '') {
      this.filteredCards = this.cards;
    } else {
      this.filteredCards = this.cards.filter(card =>
        card.boxName.toLowerCase().includes(searchTerm.toLowerCase())
      );
    }
    this.chunkedCards = this.chunk(this.filteredCards, 3);
  }

  chunk(arr: any[], size: number): any[][] {
    const chunkedArray: any[][] = [];
    for (let i = 0; i < arr.length; i += size) {
      chunkedArray.push(arr.slice(i, i + size));
    }
    return chunkedArray;
  }
  openBoxInfo(boxId: number) {
    this.navCtrl.navigateForward(`/box-info/${boxId}`);
  }
  deleteBox(boxId: number) {
    this.http.delete<any>(`http://localhost:5000/box/${boxId}`).subscribe(data => {
      console.log(data);
      const index = this.cards.findIndex(card => card.boxId === boxId);
      if (index !== -1) {
        this.cards.splice(index, 1);
        this.chunkedCards = this.chunk(this.cards, 3);
      }
    });
  }

  async openUpdateModal(boxId: number) {
    const modal = await this.modalController.create({
      component: UpdateBoxModalComponent,
      componentProps: {
        boxId: boxId,
      },
    });

    modal.onDidDismiss().then(data => {
      console.log('Modal dismissed with data:', data);
      if (data && data.data && data.data.updatedBox) {
        const updatedCard = data.data.updatedBox;
        const index = this.cards.findIndex(card => card.boxId === updatedCard.boxId);
        if (index !== -1) {
          this.cards[index] = updatedCard;
          this.filteredCards = [...this.cards];
          this.chunkedCards = this.chunk(this.filteredCards, 3);
        }
      }
    });

    await modal.present();
  }


}
