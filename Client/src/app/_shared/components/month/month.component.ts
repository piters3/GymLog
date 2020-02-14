import { Component, OnInit, Input } from '@angular/core';
import { Month } from 'src/app/user/daylogs/month';

@Component({
  selector: 'app-month',
  templateUrl: './month.component.html',
  styleUrls: ['./month.component.css']
})
export class MonthComponent implements OnInit {
  @Input() month : Month;

  constructor() { }

  ngOnInit() {
  }

}
