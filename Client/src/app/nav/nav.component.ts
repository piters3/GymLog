import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Urls } from '../urls';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(public authService: AuthService, private router: Router, private toastr: ToastrService,
              public i18n: TranslateService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.toastr.success(this.i18n.instant('SuccessfulLogin'));
    }, error => {
      this.toastr.error(error);
    }, () => {
      this.router.navigate([Urls.homeUrl]);
    });
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
