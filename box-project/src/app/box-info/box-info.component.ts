import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import { ActivatedRoute } from '@angular/router';
import { BoxInfo } from './box-info.model';




@Component({
  selector: 'app-box-info',
  templateUrl: './box-info.component.html',
  styleUrls: ['./box-info.component.css']
})
export class BoxInfoComponent implements OnInit {
  card: BoxInfo;

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute
  ) {}


  ngOnInit(): void {
    const boxId = this.route.snapshot.params['boxId'];
    this.http.get<BoxInfo>(`http://localhost:5000/box/${boxId}`).subscribe(
      data => {
        this.card = data;
      },
      error => {
        console.error('Error:', error);
      }
    );

  }
}
