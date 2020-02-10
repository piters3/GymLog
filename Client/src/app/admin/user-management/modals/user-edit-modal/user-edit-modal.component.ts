import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsDatepickerConfig } from 'ngx-bootstrap';
import { User } from '../../../../_models/user';
import { FormGroup, FormBuilder, FormArray, AbstractControl, Validators } from '@angular/forms';
import { Role } from 'src/app/_models/role';
import { AdminStore } from '../../services/admin.store';
import { Observable } from 'rxjs';
import { filter, distinctUntilChanged } from 'rxjs/operators';

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
  success$: Observable<boolean>;

  get rolesFormControls(): AbstractControl[] { return (this.form.get('roles') as FormArray).controls; }
  get isAdmin(): boolean { return this.user.userName === 'Admin'; }

  constructor(public bsModalRef: BsModalRef, public fb: FormBuilder, private adminStore: AdminStore) { }

  ngOnInit(): void {
    this.bsConfig = {
      containerClass: 'theme-red',
      dateInputFormat: 'YYYY-MM-DD'
    };
    this.createForms();
    this.success$ = this.adminStore.success$;

    this.success$.pipe(
      filter(x => x),
      distinctUntilChanged()
    ).subscribe(() => {
      this.adminStore.resetUserState();
      this.bsModalRef.hide();
    });
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
    this.adminStore.updateUser(updatedUser);
  }
}
