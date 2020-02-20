import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-empty',
  templateUrl: './empty.component.html',
  styleUrls: ['./empty.component.css']
})
export class EmptyComponent implements OnInit {
  @Input() message: string;
  @Input() addType: string;
  @Input() link: string;

  constructor() { }

  ngOnInit() {
  }

}
