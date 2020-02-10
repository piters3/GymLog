import { Injectable } from '@angular/core';
import { User } from '../../../_models/user';
import { BehaviorSubject } from 'rxjs';
import { Role } from '../../../_models/role';
import { AdminService } from './admin.service';

@Injectable({
  providedIn: 'root'
})
export class AdminStore {

  private readonly _loading: BehaviorSubject<boolean> = new BehaviorSubject(false);
  private readonly _success: BehaviorSubject<boolean> = new BehaviorSubject(false);
  private readonly _usersWithRoles: BehaviorSubject<User[]> = new BehaviorSubject([]);
  private readonly _user: BehaviorSubject<User> = new BehaviorSubject(null);
  private readonly _roles: BehaviorSubject<Role[]> = new BehaviorSubject([]);

  public success$ = this._success.asObservable();
  public loading$ = this._loading.asObservable();
  public usersWithRoles$ = this._usersWithRoles.asObservable();
  public roles$ = this._roles.asObservable();
  public user$ = this._user.asObservable();

  constructor(private adminService: AdminService) { }

  loadUsersWithRoles() {
    this._loading.next(true);
    this.adminService.getUsersWithRoles().subscribe(res => {
      this._usersWithRoles.next(res);
      this._loading.next(false);
    });
  }

  loadUser(userId: number) {
    this._loading.next(true);
    this.adminService.getUser(userId).subscribe(res => {
      this._user.next(res);
      this._loading.next(false);
    });
  }

  resetUserState() {
    this._user.next(null);
  }

  loadRoles() {
    this._loading.next(true);
    this.adminService.getRoles().subscribe(res => {
      this._roles.next(res);
      this._loading.next(false);
    });
  }

  updateUser(user: User) {
    this._loading.next(true);
    this.adminService.updateUser(user).subscribe(res => {
      if (res.status === 202)
        this._success.next(true)
      this._loading.next(false);
    });
  }

  resetSuccess() {
    this._success.next(false);
  }
}
