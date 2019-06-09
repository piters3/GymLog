import { Injectable } from '@angular/core';
import { HttpClient } from '../../../node_modules/@angular/common/http';
import { User } from '../_models/user';
import { Urls } from '../urls';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  getUsersWithRoles() {
    return this.http.get(Urls.usersWithRolesUrl);
  }

  updateUserRoles(user: User, roles: {}) {
    return this.http.post(Urls.editUserRolesUrl + user.userName, roles);
  }
}