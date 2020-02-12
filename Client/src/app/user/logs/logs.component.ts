import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/app/_models/user';
import { Urls } from 'src/app/urls';

@Component({
  selector: 'app-logs',
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.css']
})
export class LogsComponent implements OnInit {

  constructor(private http: HttpClient) { }

  ngOnInit() {
    return this.http.get<object[]>(Urls.userDaylogsUrl).subscribe(res => {
      console.log(res);
    });
  }

}
