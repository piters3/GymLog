import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Muscle } from '../../../_models/muscle';
import { Urls } from '../../../urls';
import { Observable, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MusclesService {

  private loading: BehaviorSubject<boolean> = new BehaviorSubject(false);
  private muscle: BehaviorSubject<Muscle> = new BehaviorSubject(null);
  private muscles: BehaviorSubject<Muscle[]> = new BehaviorSubject([]);

  get getLoading(): Observable<boolean> { return this.loading.asObservable(); }
  get getMuscle(): Observable<Muscle> { return this.muscle.asObservable(); }
  get getMuscles(): Observable<Muscle[]> { return this.muscles.asObservable(); }

  constructor(private http: HttpClient) { }

  get(id: number) {
    this.loading.next(true);
    this.http.get<Muscle>(Urls.musclesUrl + id).subscribe(res => {
      this.muscle.next(res);
      this.loading.next(false);
    });
  }

  getAll() {
    this.loading.next(true);
    this.http.get<Muscle[]>(Urls.musclesUrl).subscribe(res => {
      this.muscles.next(res);
      this.loading.next(false);
    });
  }

  add(muscle: Muscle) {
    return this.http.post(Urls.musclesUrl, muscle);
  }

  update(muscle: Muscle) {
    return this.http.put(Urls.musclesUrl + muscle.id, muscle);
  }

  delete(id: number) {
    this.loading.next(true);
    this.http.delete(Urls.musclesUrl + id).subscribe(() => {
      // this.muscles.next(this.muscles.getValue())
      this.loading.next(false);
    });
  }
}
