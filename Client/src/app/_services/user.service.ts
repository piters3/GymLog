import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { Urls } from '../urls';

const httpOptions = {
  headers: new HttpHeaders({
    'Authorization': 'Bearer ' + localStorage.getItem('token')
  })
};

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) {}

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(Urls.usersUrl, httpOptions);
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(Urls.userUrl + id, httpOptions);
  }

  updateUser(id: number, user: User) {
    return this.http.put(Urls.userUrl + id, user);
  }
}
