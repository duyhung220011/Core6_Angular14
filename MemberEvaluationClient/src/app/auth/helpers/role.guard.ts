
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from 'app/auth/service';

@Injectable({
  providedIn: 'root',
})
export class RoleGuard implements CanActivate {
  constructor(private _authenticationService: AuthenticationService, private _router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    return this.checkUserLogin(route, state.url);
  }

  checkUserLogin(route: ActivatedRouteSnapshot, url: any): boolean {
    const currentUser = this._authenticationService.currentUserValue;
    if (currentUser) {
      if (this._authenticationService.isAuthenticated()) {
        const userRole = this._authenticationService.getRole();
        if (route.data.role && route.data.role.indexOf(userRole) === -1) {
          this._router.navigate(['/pages/miscellaneous/not-authorized']);
          return false;
        }
        return true;
      }

      this._router.navigate(['/pages/authentication/login']);
      return false;
    }
  }
}
