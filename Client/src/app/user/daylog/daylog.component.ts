import { Component } from '@angular/core';
import { BaseComponent } from 'src/app/_shared/components/base/base.component';
import { DaylogsStore } from '../daylogs/services/daylogs.store';
import { DaylogDto } from 'src/app/_models/daylogDto';
import { Observable } from 'rxjs';
import { distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-daylog',
  templateUrl: './daylog.component.html',
  styleUrls: ['./daylog.component.css']
})
export class DaylogComponent extends BaseComponent {
  daylog$: Observable<DaylogDto>;
  routeDate: Date;

  constructor(private route: ActivatedRoute, private daylogsStore: DaylogsStore) {
    super();
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.routeDate = new Date(params.get('date'));
    });

    this.loading$ = this.daylogsStore.loading$;
    this.daylog$ = this.daylogsStore.daylog$;
    this.daylogsStore.getDaylog(this.routeDate);

    this.daylog$.pipe(
      distinctUntilChanged(),
      takeUntil(this.destroy$)
    ).subscribe((daylog) => {
      // console.log(daylog)
    });
  }

}
