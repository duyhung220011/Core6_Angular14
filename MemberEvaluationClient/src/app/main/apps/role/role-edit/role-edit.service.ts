import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Role_new } from 'app/auth/models';
import { environment } from 'environments/environment';

import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';


@Injectable()
export class RoleEditService implements Resolve<Role_new> {
  public apiData: any;
  public onRoleEditChanged: BehaviorSubject<Role_new>;

  /**
   * Constructor
   */
  constructor(private _httpClient: HttpClient) {
    // Set the defaults
    this.onRoleEditChanged = new BehaviorSubject<Role_new>(null);
  }
  public get accountValue(): Role_new {
    return this.onRoleEditChanged.value;
  }
  /**
   * Resolver
   */
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Role_new> | Promise<Role_new> | any {
    return new Promise<void>((resolve, reject) => {
      Promise.all([this.getAllData()]).then(() => {
        resolve();
      }, reject);
    });
  }

  /**
   * Get API Data
   */
  getAllData(): Promise<Role_new[]> {
    return new Promise((resolve, reject) => {
      this._httpClient.get(`${environment.apiUrl}/roles`).subscribe((response: Role_new) => {
        this.apiData = response;
        this.onRoleEditChanged.next(this.apiData);
        resolve(this.apiData);
      }, reject);
    });
  }

  getById(id: string) {
    return this._httpClient.get<Role_new>(`${environment.apiUrl}/roles/${id}`);
  }

  update(id, params) {
    return this._httpClient.put(`${environment.apiUrl}/roles/${id}`, params)
        .pipe(map((account: any) => {
            // update the current account if it was updated
            if (account.id === this.accountValue.id) {
                // publish updated account to subscribers
                account = { ...this.accountValue, ...account };
                this.onRoleEditChanged.next(account);
            }
            return account;
        }));
  }
}
