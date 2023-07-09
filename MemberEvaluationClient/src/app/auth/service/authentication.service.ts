import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

import { environment } from 'environments/environment';
import { User, Role } from 'app/auth/models';
import { ToastrService } from 'ngx-toastr';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  authServiceUrl = `${environment.protocol}${environment.apiUrl}/${environment.authService}`;
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(
    private _http: HttpClient,
    private jwtHelper: JwtHelperService
    ) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(sessionStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
    // ) {
    // this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(sessionStorage.getItem('currentUser')));
    // this.currentUser = this.currentUserSubject.asObservable();
  }

  // getter: currentUserValue
  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  // login(userId: string, password: string) {
  //   return this._http.post<any>(`${this.authServiceUrl}/auth/login`, { userId, password })
  //     .pipe(map(currentUser => {
  //         // login successful if there's a jwt token in the response
  //         if (currentUser && currentUser.token) {
  //           // store user details and jwt token in local storage to keep user logged in between page refreshes
  //           sessionStorage.setItem('currentUser', JSON.stringify(currentUser));

  //           // notify
  //           this.currentUserSubject.next(currentUser);
  //         }

  //         return currentUser;
  //       })
  //     );
  // }
  login(formData) {
    return this._http
      .post<any>(`${this.authServiceUrl}/auth/login`, formData)
      .pipe(map(user => {
          // login successful if there's a jwt token in the response
          if (user && user.token) {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            sessionStorage.setItem('currentUser', JSON.stringify(user));
            // localStorage.setItem('currentUser', JSON.stringify(user));
            // Display welcome toast!
            // setTimeout(() => {
            //   this._toastrService.success(
            //     'You have successfully logged in as an ' +
            //       user.role +
            //       ' user to Vuexy. Now you can start to explore. Enjoy! 🎉',
            //     '👋 Welcome, ' + user.firstName + '!',
            //     { toastClass: 'toast ngx-toastr', closeButton: true }
            //   );
            // }, 2500);

            // notify
            this.currentUserSubject.next(user);
          }

          return user;
        })
      );
  }
  /**
   * User logout
   *
   */
  isAuthenticated(): boolean {
    const token = this.fetchFromSessionStorage()?.token;
    return !this.jwtHelper.isTokenExpired(token);
  }
  logout() {
    // remove user from local storage to log user out
    sessionStorage.removeItem('currentUser');
    // notify
    this.currentUserSubject.next(null);
  }
  getRole() {
    return this.fetchFromSessionStorage()?.role;
  }

  fetchFromSessionStorage() {
    return JSON.parse(sessionStorage.getItem('currentUser'));
  }

}
