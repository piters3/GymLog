import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/user';
import { AdminService } from '../../_services/admin.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { RolesModalComponent } from '../roles-modal/roles-modal.component';
import { NgxUiLoaderService } from 'ngx-ui-loader';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {
  users: User[];
  bsModalRef: BsModalRef;

  constructor(private adminService: AdminService, private modalService: BsModalService, private spinner: NgxUiLoaderService) { }

  ngOnInit() {
    this.getUsersWithRoles();
  }

  getUsersWithRoles() {
    this.spinner.start();
    this.adminService.getUsersWithRoles().subscribe((users: User[]) => {
      this.users = users;
      this.spinner.stop();
    }, error => {
      console.log(error);
      this.spinner.stop();
    });
  }

  editRolesModal(user: User) {
    const initialState = {
      user,
      roles: this.getRolesArray(user)
    };
    this.bsModalRef = this.modalService.show(RolesModalComponent, {initialState});
    this.bsModalRef.content.updateSelectedRoles.subscribe((values) => {
      const rolesToUpdate = {
        roleNames: [...values.filter(el => el.checked === true).map(el => el.name)]
      };
      if (rolesToUpdate) {
        this.spinner.start();
        this.adminService.updateUserRoles(user, rolesToUpdate).subscribe(() => {
          user.roles = [...rolesToUpdate.roleNames];
          this.spinner.stop();
        }, error => {
          console.log(error);
          this.spinner.stop();
        });
      }
    });
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
