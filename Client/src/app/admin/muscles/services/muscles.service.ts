import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Muscle } from '../../../_models/muscle';
import { Urls } from '../../../urls';
import { Observable, BehaviorSubject } from 'rxjs';
import { finalize } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MusclesService {

  private loading$: BehaviorSubject<boolean> = new BehaviorSubject(false);
  private muscle$: BehaviorSubject<Muscle> = new BehaviorSubject(null);
  private muscles$: BehaviorSubject<Muscle[]> = new BehaviorSubject([]);
  private isSuccess$: BehaviorSubject<boolean> = new BehaviorSubject(false);

  get getLoading(): Observable<boolean> { return this.loading$.asObservable(); }
  get getMuscle(): Observable<Muscle> { return this.muscle$.asObservable(); }
  get getMuscles(): Observable<Muscle[]> { return this.muscles$.asObservable(); }
  get getSucces(): Observable<boolean> { return this.isSuccess$.asObservable(); }

  constructor(private http: HttpClient) { }

  get(id: number) {
    this.loading$.next(true);
    this.http.get<Muscle>(Urls.musclesUrl + id).subscribe(res => {
      this.muscle$.next(res);
      this.loading$.next(false);
    });
  }

  clearGetState() {
    this.muscle$.next(null);
  }

  getAll() {
    this.loading$.next(true);
    this.http.get<Muscle[]>(Urls.musclesUrl).subscribe(res => {
      this.muscles$.next(res);
      this.loading$.next(false);
    });
  }

  add(muscle: Muscle) {
    return this.http.post(Urls.musclesUrl, muscle);
  }

  update(muscle: Muscle) {
    return this.http.put(Urls.musclesUrl + muscle.id, muscle);
  }

  // delete(id: number) {
  //   this.loading$.next(true);
  //   this.http.delete(Urls.musclesUrl + id).pipe(
  //     finalize(() => this.isSuccess$.next(true))
  //   ).subscribe(() => {
  //     // this.muscles.next(this.muscles.getValue())
  //     this.loading$.next(false);
  //   });
  // }

  delete(id: number) {
    this.loading$.next(true);
    this.http.delete(Urls.musclesUrl + id, { observe: 'response' }).subscribe((res) => {
      if (res.status === 202)
        this.isSuccess$.next(true)
      this.loading$.next(false);
    });
  }

  clearDeleteState() {
    this.isSuccess$.next(false);
  }


}
