import { Component } from '@angular/core';
import { BaseComponent } from '../_shared/components/base/base.component';
import { Exercise } from '../_models/exercise';
import { Observable } from 'rxjs';
import { ExercisesStore } from './services/exercises.store';

@Component({
  selector: 'app-exercises',
  templateUrl: './exercises.component.html',
  styleUrls: ['./exercises.component.css']
})
export class ExercisesComponent extends BaseComponent {
  exercises$: Observable<Exercise[]>;

  constructor(private exercisesStore: ExercisesStore) {
    super();
  }

  ngOnInit() {
    this.loading$ = this.exercisesStore.loading$;
    this.exercises$ = this.exercisesStore.exercises$;
    this.exercisesStore.getAll();
  }

}
