import { Component } from '@angular/core';
import { CardFilterService } from '../services/card-filter.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {
  constructor(private cardFilterService: CardFilterService) {}

  onInputChange(event: any) {
    const searchTerm = event.target.value;
    this.cardFilterService.setSearchQuery(searchTerm);
  }
}
