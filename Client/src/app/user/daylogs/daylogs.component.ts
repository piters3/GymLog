import { Component } from '@angular/core';
import { DaylogsStore } from './services/daylogs.store';
import { BaseComponent } from 'src/app/_shared/components/base/base.component';
import { BsDatepickerConfig, BsDatepickerViewMode } from 'ngx-bootstrap/datepicker';
import { Observable } from 'rxjs';
import { distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { Month } from './month';

@Component({
  selector: 'app-logs',
  templateUrl: './daylogs.component.html',
  styleUrls: ['./daylogs.component.css']
})
export class DaylogsComponent extends BaseComponent {
  dates$: Observable<Date[]>;
  initialValue: Date = new Date();
  minMode: BsDatepickerViewMode = 'month';
  bsConfig: Partial<BsDatepickerConfig>;
  selectedDate = new Date();
  month: Month;
  filledDays: number[];

  constructor(private daylogsStore: DaylogsStore) {
    super();
  }

  ngOnInit() {
    this.bsConfig = {
      minMode: this.minMode,
      dateInputFormat: 'YYYY-MM-DD',
      value: new Date()
    }

    this.loading$ = this.daylogsStore.loading$;
    this.dates$ = this.daylogsStore.dates$;

    this.dates$.pipe(
      distinctUntilChanged(),
      takeUntil(this.destroy$)
    ).subscribe((dates) => {
      this.filledDays = dates.map(x => new Date(x).getDate());
      this.month = new Month(this.selectedDate, this.filledDays);
    });
  }

  onValueChange(value: Date): void {
    this.selectedDate = value;
    this.daylogsStore.getDaylogDates(this.selectedDate);
  }


}
