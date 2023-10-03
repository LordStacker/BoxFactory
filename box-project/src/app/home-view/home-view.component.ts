import { Component } from '@angular/core';
import { CardFilterService } from '../services/card-filter.service';


@Component({
  selector: 'app-home-view',
  templateUrl: './home-view.component.html',
  styleUrls: ['./home-view.component.css']
})
export class HomeViewComponent {
  constructor(private cardFilterService: CardFilterService) {
  }

  ngOnInit() {
  }

  onSearchChange(searchTerm: string) {
    console.log(searchTerm)
    this.cardFilterService.setSearchQuery(searchTerm);
  }
}
