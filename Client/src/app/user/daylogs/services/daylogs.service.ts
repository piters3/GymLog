import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Urls } from 'src/app/urls';
import { DaylogDto } from 'src/app/_models/daylogDto';
import * as moment from 'moment';

@Injectable({
  providedIn: 'root'
})
export class DaylogsService {

  constructor(private http: HttpClient) { }

  getDaylogsDates(date: Date) {
    return this.http.get<Date[]>(Urls.userDaylogsDatesUrl + moment(date).format('YYYY-MM-DD'));
  }

  getDaylog(date: Date) {

    return this.http.get<DaylogDto>(Urls.userDaylogUrl + moment(date).format('YYYY-MM-DD'));
  }
}
