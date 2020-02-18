import { Component } from '@angular/core';
import { ExercisesStore } from '../exercises/services/exercises.store';
import { BaseComponent } from '../_shared/components/base/base.component';
import { Exercise } from '../_models/exercise';
import { Observable } from 'rxjs';
import { distinctUntilChanged, takeUntil, filter } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-exercise',
  templateUrl: './exercise.component.html',
  styleUrls: ['./exercise.component.css']
})
export class ExerciseComponent extends BaseComponent {
  exercise$: Observable<Exercise>;
  id: number;

  constructor(private route: ActivatedRoute, private exercisesStore: ExercisesStore) {
    super();
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.id = Number(params.get('id'));
    });

    this.loading$ = this.exercisesStore.loading$;
    this.exercise$ = this.exercisesStore.exercise$;
    this.exercisesStore.get(this.id);

    this.exercise$.pipe(
      filter(x => !!x),
      distinctUntilChanged(),
      takeUntil(this.destroy$)
    ).subscribe((exercise) => {
      console.log(exercise)
    });
  }

}
