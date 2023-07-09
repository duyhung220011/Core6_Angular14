import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { User } from 'app/auth/models';
import { UserPage } from 'app/main/model/pagination/user-page';
import { environment } from 'environments/environment';

import { BehaviorSubject, Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class UserListService implements Resolve<any> {
  public rows: any;
  public onUserListChanged: BehaviorSubject<any>;
  private accountSubject: BehaviorSubject<User>;
  public account: Observable<User>;
  /**
   * Constructor
   */
  constructor(private router: Router,private _httpClient: HttpClient) {
    // Set the defaults
    this.onUserListChanged = new BehaviorSubject({});
    this.accountSubject = new BehaviorSubject<User>(null);
    this.account = this.accountSubject.asObservable();
  }
  public get accountValue(): User {
    return this.accountSubject.value;
  }
  /**
   * Resolver
   */
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {
    return new Promise<void>((resolve, reject) => {
      Promise.all([this.getDataTableRows()]).then(() => {
        resolve();
      }, reject);
    });
  }

  /**
   * Get rows
   */
  getDataTableRows(): Promise<any[]> {
    return new Promise((resolve, reject) => {
      this._httpClient.get(`${environment.apiUrl}/users`).subscribe((response: any) => {
        this.rows = response;
        this.onUserListChanged.next(this.rows);
        resolve(this.rows);
      }, reject);
    });
  }

  getUserPages(pageNumber: number, pageSize = 10) {
    let params = new HttpParams();
    params = params.set('pageNumber', pageNumber.toString());
    params = params.set('pageSize', pageSize.toString());
    return this._httpClient.get<UserPage>(`${environment.apiUrl}/users/page`,
      {
        params: params
      });
  }
  
  // delete(id: string) {
  //   return this._httpClient.delete(`${environment.apiUrl}/users/${id}`)
  //       .pipe(finalize(() => {
  //           // auto logout if the logged in account was deleted
  //           if (id === this.accountValue.id)
  //               this.logout();
  //       }));
  // }

  logout() {
    this.accountSubject.next(null);
    this.router.navigate(['/pages/authentication/login']);
  }
  
}
