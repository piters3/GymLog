import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsDatepickerConfig } from 'ngx-bootstrap';
import { User } from '../../_models/user';
import { FormGroup, FormBuilder, FormArray, AbstractControl, Validators } from '@angular/forms';
import { AdminService } from 'src/app/_services/admin.service';
import { Role } from 'src/app/_models/role';

@Component({
  selector: 'app-roles-modal',
  templateUrl: './user-edit-modal.component.html',
  styleUrls: ['./user-edit-modal.component.css']
})
export class UserEditModalComponent implements OnInit {
  form: FormGroup;
  user: User;
  roles: Role[];
  bsConfig: Partial<BsDatepickerConfig>;

  get rolesFormControls(): AbstractControl[] { return (this.form.get('roles') as FormArray).controls; }
  get isAdmin(): boolean { return this.user.userName === 'Admin'; }

  constructor(public bsModalRef: BsModalRef, public fb: FormBuilder, private adminService: AdminService) { }

  ngOnInit(): void {
    this.bsConfig = {
      containerClass: 'theme-red',
      dateInputFormat: 'YYYY-MM-DD'
    };
    this.createForms();
  }

  private createForms(): void {
    this.form = this.fb.group({
      username: [this.user.userName, Validators.required],
      name: this.user.name,
      surname: this.user.surname,
      email: [this.user.email, [Validators.required, Validators.email]],
      enabled: this.user.enabled,
      gender: this.user.gender,
      weight: this.user.weight,
      height: this.user.height,
      registerDate: this.user.registerDate,
      roles: this.fb.array([])
    });

    this.roles.forEach((item) => {
      (this.form.get('roles') as FormArray).push(this.fb.group({
        id: item.id,
        name: item.name,
        checked: this.fb.control({ value: this.isRoleSelected(item), disabled: (this.isAdmin && item.name === 'Admin') })
      }));
    });
  }

  isRoleSelected(role: Role) {
    return this.user.roles.some(x => x.name === role.name);
  }

  onSubmit(): void {
    const updatedUser: User = { ...this.form.value, id: this.user.id };
    updatedUser.roles = updatedUser.roles.filter((item: any) => item.checked);

    this.adminService.updateUser(updatedUser).subscribe(() => {
      this.bsModalRef.hide();
      this.adminService.loadUsersWithRoles();
    }, error => {
      console.log(error);
    });
  }
}
