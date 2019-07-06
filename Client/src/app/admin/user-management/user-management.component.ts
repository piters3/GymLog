import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/user';
import { AdminService } from '../../_services/admin.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { RolesModalComponent } from '../roles-modal/roles-modal.component';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {

  bsModalRef: BsModalRef;
  users$: Observable<User[]>;
  isLoading$: Observable<boolean>;

  constructor(
    private adminService: AdminService,
    private modalService: BsModalService) { }

  ngOnInit(): void {
    this.adminService.updateUsers();
    this.users$ = this.adminService.getUsers;
    this.isLoading$ = this.adminService.getLoading;
  }

  editRolesModal(user: User) {
    const initialState = {
      user,
      roles: this.getRolesArray(user)
    };
    this.bsModalRef = this.modalService.show(RolesModalComponent, {initialState} );
  }

  private getRolesArray(user: User) {
    const roles = [];
    const userRoles = user.roles;
    const availableRoles: any[] = [
      { name: 'Admin', value: 'Admin' },
      { name: 'User', value: 'User' }
    ];

    for (const ar of availableRoles) {
      let isMatch = false;
      for (const ur of userRoles) {
        if (ar.name === ur) {
          isMatch = true;
          ar.checked = true;
          roles.push(ar);
          break;
        }
      }
      if (!isMatch) {
        ar.checked = false;
        roles.push(ar);
      }
    }
    return roles;
  }
}
