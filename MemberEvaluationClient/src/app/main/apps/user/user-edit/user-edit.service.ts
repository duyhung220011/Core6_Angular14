import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { User } from 'app/auth/models';
import { environment } from 'environments/environment';

import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
const baseUrl = `${environment.apiUrl}/reports/add`;

@Injectable()
export class UserEditService implements Resolve<User> {
  public apiData: any;
  public onUserEditChanged: BehaviorSubject<User>;
  public onReportNewChanged: BehaviorSubject<any>;
  /**
   * Constructor
   *  @param {HttpClient} _httpClient
   */
  constructor(private _httpClient: HttpClient) {
    // Set the defaults
    this.onUserEditChanged = new BehaviorSubject<User>(null);
    this.onReportNewChanged = new BehaviorSubject({});
  }
  public get accountValue(): User {
    return this.onUserEditChanged.value;
  }
  create(params) {
    return this._httpClient.post(baseUrl, params);
  }
  /**
   * Resolver
   */
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<User> | Promise<User> | any {
    return new Promise<void>((resolve, reject) => {
      Promise.all([this.getAllData()]).then(() => {
        resolve();
      }, reject);
    });
  }

  /**
   * Get API Data
   */
  getAllData(): Promise<User[]> {
    return new Promise((resolve, reject) => {
      this._httpClient.get(`${environment.apiUrl}/users`).subscribe((response: User) => {
        this.apiData = response;
        this.onUserEditChanged.next(this.apiData);
        resolve(this.apiData);
      }, reject);
    });
  }

  getById(id: string) {
    return this._httpClient.get<User>(`${environment.apiUrl}/users/${id}`);
  }

  update(id, params) {
    return this._httpClient.put(`${environment.apiUrl}/users/${id}`, params)
        .pipe(map((account: any) => {
            // update the current account if it was updated
            if (account.id === this.accountValue.userId) {
                // publish updated account to subscribers
                account = { ...this.accountValue, ...account };
                this.onUserEditChanged.next(account);
            }
            return account;
        }));
  }
}
