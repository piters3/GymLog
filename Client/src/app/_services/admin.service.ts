import { Injectable } from '@angular/core';
import { HttpClient } from '../../../node_modules/@angular/common/http';
import { User } from '../_models/user';
import { Urls } from '../urls';
import { Observable, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private users: BehaviorSubject<User[]> = new BehaviorSubject([]);
  private loading: BehaviorSubject<boolean> = new BehaviorSubject(false);

  constructor(private http: HttpClient) { }

  get getUsers(): Observable<any> {
    return this.users.asObservable();
  }

  get getLoading(): Observable<any> {
    return this.loading.asObservable();
  }

  updateUsers() {
    this.loading.next(true);
    this.http.get<User[]>(Urls.usersWithRolesUrl).subscribe(res => {
      this.users.next(res);
      this.loading.next(false);
    });
  }

  updateUserRoles(user: User, roles: {}) {
    return this.http.post(Urls.editUserRolesUrl + user.userName, roles);
  }
}
