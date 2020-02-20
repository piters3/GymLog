import { Component } from '@angular/core';
import { BaseComponent } from 'src/app/_shared/components/base/base.component';
import { DaylogDto } from 'src/app/_models/daylogDto';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DaylogsStore } from '../daylogs/services/daylogs.store';
import { Observable } from 'rxjs';
import { Exercise } from 'src/app/_models/exercise';
import { ExercisesStore } from 'src/app/exercises/services/exercises.store';
import { BsModalService } from 'ngx-bootstrap';
import { AddDaylogModalComponent } from './add-daylog-modal/add-daylog-modal.component';
import { distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { WorkoutDto } from 'src/app/_models/WorkoutDto';
import { ActivatedRoute } from '@angular/router';

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
    private modalService: BsModalService) {
    super();
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.routeDate = params.get('date');
      this.daylog = { id: 0, date: new Date(this.routeDate), workouts: [] }
    });

    this.loading$ = this.daylogsStore.loading$;
    this.exercises$ = this.exerciesStore.exercises$;
    this.exerciesStore.getAll();
  }

  onAdd() {
    this.exercises$.pipe(
      // filter((exercises: Exercise[]) => !!user && user.id === userId),
      // distinctUntilChanged((prev, curr) => prev.id === curr.id),
      takeUntil(this.destroy$)
    ).subscribe((exercises) => {
      const modal = this.modalService.show(AddDaylogModalComponent, { initialState: { exercises } });
      modal.content.onClose.subscribe((x: WorkoutDto) => {
        this.daylog.workouts.push(x);
      })
    });
  }

  onSubmit(): void {
    console.log(this.daylog);
    this.daylogsStore.add(this.daylog);
  }
}
