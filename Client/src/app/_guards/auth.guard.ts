import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../nav/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Urls } from '../urls';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router,
              private toastr: ToastrService, public i18n: TranslateService) { }

  canActivate(next: ActivatedRouteSnapshot): boolean {
    const roles = next.firstChild.data.roles as Array<string>;
    if (roles) {
      const match = this.authService.roleMatch(roles);
      if (match) {
        return true;
      } else {
        this.router.navigate([Urls.homeUrl]);
        this.toastr.error(this.i18n.instant('NotAuthorized'));
      }
    }

    if (this.authService.loggedIn()) {
      return true;
    }

    this.toastr.error(this.i18n.instant('YouShallNotPass'));
    this.router.navigate([Urls.homeUrl]);
    return false;
  }
}
