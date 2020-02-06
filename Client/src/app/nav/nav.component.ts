import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Urls } from '../urls';
import { TranslateService } from '@ngx-translate/core';
import { LoginModel } from '../_models/LoginModel';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  loginModel: LoginModel;
  form: FormGroup;

  constructor(public authService: AuthService, private router: Router, private toastr: ToastrService,
    public i18n: TranslateService, public fb: FormBuilder) { }

  ngOnInit() {
    this.createForms();
  }

  private createForms() {
    this.form = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  login() {
    if (this.form.valid) {
      this.loginModel = { ...this.form.value };
      this.authService.login(this.loginModel).subscribe(next => {
        this.toastr.success(this.i18n.instant('SuccessfulLogin'));
      }, error => {
        this.toastr.error(error);
      }, () => {
        this.router.navigate([Urls.homeUrl]);
      });
    }
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.toastr.info(this.i18n.instant('LoggedOut'));
    this.router.navigate([Urls.homeUrl]);
  }
}
