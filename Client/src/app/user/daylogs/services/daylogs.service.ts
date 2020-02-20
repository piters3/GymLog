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
    return this.http.get<Date[]>(Urls.daylogsDatesUrl + moment(date).format('YYYY-MM-DD'));
  }

  getDaylog(date: Date) {

    return this.http.get<DaylogDto>(Urls.daylogsUrl + moment(date).format('YYYY-MM-DD'));
  }

  add(daylog: DaylogDto) {
    return this.http.post<DaylogDto>(Urls.daylogsUrl, daylog);
  }
}
