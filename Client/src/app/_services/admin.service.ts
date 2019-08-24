import { Injectable } from '@angular/core';
import { HttpClient } from '../../../node_modules/@angular/common/http';
import { User, Role } from '../_models/user';
import { Urls } from '../urls';
import { Observable, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private usersWithRoles: BehaviorSubject<User[]> = new BehaviorSubject([]);
  private user: BehaviorSubject<User> = new BehaviorSubject(null);
  private roles: BehaviorSubject<Role[]> = new BehaviorSubject([]);
  private loading: BehaviorSubject<boolean> = new BehaviorSubject(false);

  constructor(private http: HttpClient) { }

  get getUsersWithRoles(): Observable<User[]> {
    return this.usersWithRoles;
  }

  get getRoles(): Observable<Role[]> {
    return this.roles;
  }

  get getUser(): Observable<User> {
    return this.user;
  }


  get getLoading(): Observable<boolean> {
    return this.loading;
  }

  loadUsersWithRoles() {
    this.loading.next(true);
    this.http.get<User[]>(Urls.usersWithRolesUrl).subscribe(res => {
      this.usersWithRoles.next(res);
      this.loading.next(false);
    });
  }

  loadUser(userId: number) {
    this.loading.next(true);
    this.http.get<User>(Urls.userUrl + userId).subscribe(res => {
      this.user.next(res);
      this.loading.next(false);
    });
  }

  loadRoles() {
    this.loading.next(true);
    this.http.get<Role[]>(Urls.rolesUrl).subscribe(res => {
      this.roles.next(res);
      this.loading.next(false);
    });
  }

  updateUser(user: User) {
    return this.http.post(Urls.updateUserUrl + user.id, user);
  }
}
