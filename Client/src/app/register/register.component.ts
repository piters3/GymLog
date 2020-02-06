import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { UserRegisterModel } from '../_models/userRegisterModel';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { CustomValidators } from '../_shared/custom-validators/custom-validators';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  registerModel: UserRegisterModel;
  form: FormGroup;

  get usernameForm(): AbstractControl { return this.form.get('username'); }
  get passwordForm(): AbstractControl { return this.form.get('password'); }
  get confirmPasswordForm(): AbstractControl { return this.form.get('confirmPassword'); }

  constructor(private authService: AuthService, private toastr: ToastrService, public fb: FormBuilder) {
  }

  ngOnInit() {
    this.createForms();
  }

  private createForms() {
    this.form = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', Validators.required]
    }, { validator: CustomValidators.passwordMatch });
  }

  register() {
    if (this.form.valid) {
      this.registerModel = { ...this.form.value };
      this.authService.register(this.registerModel).subscribe(() => {
        this.toastr.success('registration successful');
      }, error => {
        this.toastr.error(error);
      });
    }
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
