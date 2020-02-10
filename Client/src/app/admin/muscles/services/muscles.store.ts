import { Injectable } from '@angular/core';
import { Muscle } from '../../../_models/muscle';
import { MusclesService } from './muscles.service';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MusclesStore {

  private readonly _loading: BehaviorSubject<boolean> = new BehaviorSubject(false);
  private readonly _success: BehaviorSubject<boolean> = new BehaviorSubject(false);
  private readonly _muscle: BehaviorSubject<Muscle> = new BehaviorSubject(null);
  private readonly _muscles: BehaviorSubject<Muscle[]> = new BehaviorSubject([]);

  public state$ = {
    loading$: this._loading.asObservable(),
    success$: this._success.asObservable(),
    muscle$: this._muscle.asObservable(),
    muscles$: this._muscles.asObservable()
  }

  public loading$ = this._loading.asObservable();
  public success$ = this._success.asObservable();
  public muscle$ = this._muscle.asObservable();
  public muscles$ = this._muscles.asObservable();

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

  update(muscle: Muscle) {
    this._loading.next(true);
    this.musclesService.update(muscle).subscribe(res => {
      if (res.status === 202)
        this._success.next(true)
      this._loading.next(false);
    });
  }

  delete(id: number) {
    this._loading.next(true);
    this.musclesService.delete(id).subscribe((res) => {
      if (res.status === 202)
        this._success.next(true)
      this._loading.next(false);
    });
  }

  resetSuccess() {
    this._success.next(false);
  }
}
