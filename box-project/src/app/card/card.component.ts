import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CardFilterService } from '../services/card-filter.service';

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
    private cardFilterService: CardFilterService
  ) {}

  ngOnInit() {
    this.http.get<any>('https://swapi.dev/api/people').subscribe(data => {
      this.cards = data.results;
      this.chunkedCards = this.chunk(this.cards, 3);
    });

    this.cardFilterService.getSearchQuery().subscribe(searchTerm => {
      this.filterCards(searchTerm);
    });
  }

  filterCards(searchTerm: string) {
    if (searchTerm.trim() === '') {
      this.filteredCards = this.cards;
    } else {
      this.filteredCards = this.cards.filter(card =>
        card.name.toLowerCase().includes(searchTerm.toLowerCase())
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
}
