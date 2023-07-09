import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { AuthenticationService } from 'app/auth/service';

// @Injectable({ providedIn: 'root' })
// export class AuthGuard implements CanActivate {

//   constructor(private _router: Router, private _authenticationService: AuthenticationService) {}

//   // canActivate
//   canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
//     const currentUser = this._authenticationService.currentUserValue;

//     if (currentUser) {
//       // check if route is restricted by role
//       if (route.data.roles && route.data.roles.indexOf(currentUser.role) === -1) {
//         // role not authorised so redirect to not-authorized page
//         this._router.navigate(['/pages/miscellaneous/not-authorized']);
//         return false;
//       }

//       // authorised so return true
//       return true;
//     }

//     // not logged in so redirect to login page with the return url
//     this._router.navigate(['/pages/authentication/login'], { queryParams: { returnUrl: state.url } });
//     return false;
//   }
// }
@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private _authenticationService: AuthenticationService, private _router: Router) {}

  canActivate( route: ActivatedRouteSnapshot, state: RouterStateSnapshot ){
    //| Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    const currentUser = this._authenticationService.currentUserValue;
    if (currentUser) {
      if (!this._authenticationService.isAuthenticated()) {
        this._router.navigate(['/pages/authentication/login']);
        return false;
      }
    return true;
    }
    this._router.navigate(['/pages/authentication/login'], { queryParams: { returnUrl: state.url } });
    return false;
  }
}