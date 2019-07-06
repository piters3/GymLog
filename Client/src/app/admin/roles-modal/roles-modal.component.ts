import { Component, OnInit } from '@angular/core';
import { BsModalRef } from '../../../../node_modules/ngx-bootstrap';
import { User } from '../../_models/user';
import { FormGroup, FormBuilder, FormArray, AbstractControl } from '@angular/forms';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-roles-modal',
  templateUrl: './roles-modal.component.html',
  styleUrls: ['./roles-modal.component.css']
})
export class RolesModalComponent implements OnInit {
  user: User;
  roles: any[];
  form: FormGroup;

  get formControls(): AbstractControl[] {
    return (this.form.get('role') as FormArray).controls;
  }

  get isAdmin(): boolean {
    return this.user.userName === 'Admin';
  }

  constructor(public bsModalRef: BsModalRef, public fb: FormBuilder, private adminService: AdminService) { }

  ngOnInit(): void {
    this.createForms();
  }

  onSubmit(): void {
    const rolesToUpdate = {
      roleNames: this.form.getRawValue().role.filter((item: any) => item.checked).map((item: any) => item.ID)
    };

    this.adminService.updateUserRoles(this.user, rolesToUpdate).subscribe(() => {
      this.bsModalRef.hide();
      this.adminService.updateUsers();
    }, error => {
      console.log(error);
    });
  }

  private createForms(): void {
    this.form = this.fb.group({
      username: this.user.userName,
      role: this.fb.array([])
    });

    this.roles.forEach((item) => {
      (this.form.get('role') as FormArray).push(this.fb.group({
        ID: item.name,
        checked: this.fb.control({ value: item.checked, disabled: (this.isAdmin && item.name === 'Admin') })
      }));
    });
  }
}
