import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { DaylogsService } from './daylogs.service';

@Injectable({
  providedIn: 'root'
})
export class DaylogsStore {

  private readonly _loading = new BehaviorSubject(false);
  private readonly _dates: BehaviorSubject<Date[]> = new BehaviorSubject([]);

  public loading$ = this._loading.asObservable();
  public dates$ = this._dates.asObservable();

  get dates(): Date[] {
    return this._dates.getValue();
  }

  constructor(private daylogsService: DaylogsService) { }

  getDaylogDates(date: Date) {
    this._loading.next(true);
    this.daylogsService.getDaylogsDates(date).subscribe(res => {
      this._dates.next(res);
      this._loading.next(false);
    });
  }
}
