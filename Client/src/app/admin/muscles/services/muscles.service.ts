import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Muscle } from '../../../_models/muscle';
import { Urls } from '../../../urls';

@Injectable({
  providedIn: 'root'
})
export class MusclesService {

  constructor(private http: HttpClient) { }

  get(id: number) {
    return this.http.get<Muscle>(Urls.musclesUrl + id);
  }

  getAll() {
    return this.http.get<Muscle[]>(Urls.musclesUrl);
  }

  add(muscle: Muscle) {
    return this.http.post<Muscle>(Urls.musclesUrl, muscle);
  }

  update(muscle: Muscle) {
    return this.http.put(Urls.musclesUrl + muscle.id, muscle, { observe: 'response' });
  }

  delete(id: number) {
    return this.http.delete(Urls.musclesUrl + id, { observe: 'response' });
  }
}
