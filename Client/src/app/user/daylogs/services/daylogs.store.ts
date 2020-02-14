import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { DaylogsService } from './daylogs.service';
import { DaylogDto } from 'src/app/_models/daylogDto';

@Injectable({
  providedIn: 'root'
})
export class DaylogsStore {

  private readonly _loading = new BehaviorSubject(false);
  private readonly _dates: BehaviorSubject<Date[]> = new BehaviorSubject([]);
  private readonly _daylog: BehaviorSubject<DaylogDto> = new BehaviorSubject(null);

  public loading$ = this._loading.asObservable();
  public dates$ = this._dates.asObservable();
  public daylog$ = this._daylog.asObservable();

  constructor(private daylogsService: DaylogsService) { }

  getDaylogDates(date: Date) {
    this._loading.next(true);
    this.daylogsService.getDaylogsDates(date).subscribe(res => {
      this._dates.next(res);
      this._loading.next(false);
    });
  }

  getDaylog(date: Date) {
    this._loading.next(true);
    this.daylogsService.getDaylog(date).subscribe(res => {
      this._daylog.next(res);
      this._loading.next(false);
    });
  }
}
