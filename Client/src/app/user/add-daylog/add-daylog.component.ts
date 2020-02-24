import { Component } from '@angular/core';
import { BaseComponent } from 'src/app/_shared/components/base/base.component';
import { DaylogDto } from 'src/app/_models/daylogDto';
import { DaylogsStore } from '../daylogs/services/daylogs.store';
import { Observable } from 'rxjs';
import { Exercise } from 'src/app/_models/exercise';
import { ExercisesStore } from 'src/app/exercises/services/exercises.store';
import { BsModalService } from 'ngx-bootstrap';
import { AddDaylogModalComponent } from './add-daylog-modal/add-daylog-modal.component';
import { distinctUntilChanged, takeUntil, filter } from 'rxjs/operators';
import { WorkoutDto } from 'src/app/_models/WorkoutDto';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-daylog',
  templateUrl: './add-daylog.component.html',
  styleUrls: ['./add-daylog.component.css']
})
export class AddDaylogComponent extends BaseComponent {
  daylog: DaylogDto;
  exercises$: Observable<Exercise[]>;
  routeDate: string;

  constructor(private route: ActivatedRoute, private daylogsStore: DaylogsStore, private exerciesStore: ExercisesStore,
    private modalService: BsModalService, private router: Router, private toastr: ToastrService) {
    super();
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.routeDate = params.get('date');
      this.daylog = { id: 0, date: new Date(this.routeDate), workouts: [] }
    });

    this.loading$ = this.daylogsStore.loading$;
    this.success$ = this.daylogsStore.success$;
    this.exercises$ = this.exerciesStore.exercises$;
    this.exerciesStore.getAll();

    this.success$.pipe(
      filter(x => x),
      distinctUntilChanged()
    ).subscribe(() => {
      this.toastr.success('Daylog successfully added');
      this.daylogsStore.resetSuccess();
      this.router.navigate(['/logs']);
    });
  }

  onAdd() {
    this.exercises$.pipe(
      takeUntil(this.destroy$)
    ).subscribe((exercises) => {
      const modal = this.modalService.show(AddDaylogModalComponent, { initialState: { exercises } });
      modal.content.onClose.subscribe((x: WorkoutDto) => {
        this.daylog.workouts.push(x);
      })
    });
  }

  onSubmit(): void {
    this.daylogsStore.add(this.daylog);
  }
}
