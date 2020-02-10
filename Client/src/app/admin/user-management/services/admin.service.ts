import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../../../_models/user';
import { Urls } from '../../../urls';
import { Role } from '../../../_models/role';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  getUsersWithRoles() {
    return this.http.get<User[]>(Urls.usersWithRolesUrl);
  }

  getUser(userId: number) {
    return this.http.get<User>(Urls.userUrl + userId);
  }

  getRoles() {
    return this.http.get<Role[]>(Urls.rolesUrl);
  }

  updateUser(user: User) {
    return this.http.post(Urls.updateUserUrl + user.id, user, { observe: 'response' });
  }
}
