import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Urls } from 'src/app/urls';

@Injectable({
  providedIn: 'root'
})
export class DaylogsService {

  constructor(private http: HttpClient) { }

  getDaylogsDates(date: Date) {
    const params = new HttpParams().set('date', date.toDateString());
    return this.http.get<Date[]>(Urls.userDaylogsDatesUrl, { params });
  }
}
