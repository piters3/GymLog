import { Injectable } from '@angular/core';
import { Muscle } from '../../../_models/muscle';
import { MusclesService } from './muscles.service';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MusclesStore {

  private readonly _loading = new BehaviorSubject(false);
  private readonly _modalLoading = new BehaviorSubject(false);
  private readonly _success = new BehaviorSubject(false);
  private readonly _muscle: BehaviorSubject<Muscle> = new BehaviorSubject(null);
  private readonly _muscles: BehaviorSubject<Muscle[]> = new BehaviorSubject([]);

  public loading$ = this._loading.asObservable();
  public modalLoading$ = this._modalLoading.asObservable();
  public success$ = this._success.asObservable();
  public muscle$ = this._muscle.asObservable();
  public muscles$ = this._muscles.asObservable();

  get muscles(): Muscle[] {
    return this._muscles.getValue();
  }

  constructor(private musclesService: MusclesService) { }

  get(id: number) {
    this._loading.next(true);
    this.musclesService.get(id).subscribe(res => {
      this._muscle.next(res);
      this._loading.next(false);
    });
  }

  resetMuscleState() {
    this._muscle.next(null);
  }

  getAll() {
    this._loading.next(true);
    this.musclesService.getAll().subscribe(res => {
      this._muscles.next(res);
      this._loading.next(false);
    });
  }

  add(muscle: Muscle) {
    this._modalLoading.next(true);
    this.musclesService.add(muscle).subscribe(res => {
      if (res.id) {
        this._muscles.next([...this.muscles, res]);
        this._success.next(true)
      }
      this._modalLoading.next(false);
    });
  }

  update(muscle: Muscle) {
    this._modalLoading.next(true);
    this.musclesService.update(muscle).subscribe(res => {
      if (res.status === 202) {
        const index = this.muscles.findIndex(x => x.id === muscle.id);
        this.muscles[index] = muscle;
        this._muscles.next(this.muscles);
        this._success.next(true)
      }
      this._modalLoading.next(false);
    });
  }

  delete(id: number) {
    this._loading.next(true);
    this.musclesService.delete(id).subscribe((res) => {
      if (res.status === 202)
        this._muscles.next(this.muscles.filter(x => x.id !== id));
      this._loading.next(false);
    });
  }

  resetSuccess() {
    this._success.next(false);
  }
}
