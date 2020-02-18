import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Exercise } from 'src/app/_models/exercise';
import { ExercisesService } from './exercises.service';

@Injectable({
  providedIn: 'root'
})
export class ExercisesStore {

  private readonly _loading = new BehaviorSubject(false);
  private readonly _exercise: BehaviorSubject<Exercise> = new BehaviorSubject(null);
  private readonly _exercises: BehaviorSubject<Exercise[]> = new BehaviorSubject([]);

  public loading$ = this._loading.asObservable();
  public exercises$ = this._exercises.asObservable();
  public exercise$ = this._exercise.asObservable();

  constructor(private exercisesService: ExercisesService) { }

  getAll() {
    this._loading.next(true);
    this.exercisesService.getAll().subscribe(res => {
      this._exercises.next(res);
      this._loading.next(false);
    });
  }

  get(id: number) {
    this._loading.next(true);
    this.exercisesService.get(id).subscribe(res => {
      this._exercise.next(res);
      this._loading.next(false);
    });
  }
}
