import { Component, OnInit, OnDestroy } from '@angular/core';
import { User, Role } from '../../_models/user';
import { AdminService } from '../../_services/admin.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { UserEditModalComponent } from '../user-edit-modal/user-edit-modal.component';
import { Observable, Subject } from 'rxjs';
import { filter, withLatestFrom, takeUntil, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit, OnDestroy {

  bsModalRef: BsModalRef;
  users$: Observable<User[]>;
  roles$: Observable<Role[]>;
  user$: Observable<User>;
  isLoading$: Observable<boolean>;
  private destroy$: Subject<void> = new Subject();

  constructor(private adminService: AdminService, private modalService: BsModalService) { }

  ngOnInit(): void {
    this.adminService.loadUsersWithRoles();
    this.adminService.loadRoles();
    this.users$ = this.adminService.getUsersWithRoles;
    this.roles$ = this.adminService.getRoles;
    this.isLoading$ = this.adminService.getLoading;
    this.user$ = this.adminService.getUser;
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  editUser(userId: number) {
    this.adminService.loadUser(userId);

    this.user$.pipe(
      filter((user: User) => !!user && user.id === userId),
      distinctUntilChanged((prev, curr) => prev.id === curr.id),
      withLatestFrom(this.roles$),
      takeUntil(this.destroy$)
    ).subscribe(([user, roles]) => {
      const initialState = {user, roles};
      this.bsModalRef = this.modalService.show(UserEditModalComponent, {initialState} );
    });
  }
}
