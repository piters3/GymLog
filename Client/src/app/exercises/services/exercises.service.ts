import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Exercise } from 'src/app/_models/exercise';
import { Urls } from 'src/app/urls';

@Injectable({
  providedIn: 'root'
})
export class ExercisesService {

  constructor(private http: HttpClient) { }

  get(id: number) {
    return this.http.get<Exercise>(Urls.exercisesUrl + id);
  }

  getAll() {
    return this.http.get<Exercise[]>(Urls.exercisesUrl);
  }
}
