import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Urls } from '../urls';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router,
              private toastr: ToastrService) { }

  canActivate(next: ActivatedRouteSnapshot): boolean {
    const roles = next.firstChild.data.roles as Array<string>;
    if (roles) {
      const match = this.authService.roleMatch(roles);
      if (match) {
        return true;
      } else {
        this.router.navigate([Urls.homeUrl]);
        this.toastr.error('You are not authorized to access this area');
      }
    }

    if (this.authService.loggedIn()) {
      return true;
    }

    this.toastr.error('You shall not pass!!!');
    this.router.navigate([Urls.homeUrl]);
    return false;
  }
}
