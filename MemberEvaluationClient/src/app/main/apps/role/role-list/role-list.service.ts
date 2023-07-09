import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Role_new } from 'app/auth/models';
import { environment } from 'environments/environment';

import { BehaviorSubject, Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class RoleListService implements Resolve<any> {
  public rows: any;
  public onRoleListChanged: BehaviorSubject<any>;
  private accountSubject: BehaviorSubject<Role_new>;
  public account: Observable<Role_new>;
  /**
   * Constructor
   */
  constructor(private router: Router,private _httpClient: HttpClient) {
    // Set the defaults
    this.onRoleListChanged = new BehaviorSubject({});
    this.accountSubject = new BehaviorSubject<Role_new>(null);
    this.account = this.accountSubject.asObservable();
  }
  public get accountValue(): Role_new {
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
      this._httpClient.get(`${environment.apiUrl}/roles`).subscribe((response: any) => {
        this.rows = response;
        this.onRoleListChanged.next(this.rows);
        resolve(this.rows);
      }, reject);
    });
  }

  delete(id: string) {
    return this._httpClient.delete(`${environment.apiUrl}/roles/${id}`)
        .pipe(finalize(() => {
          //  auto logout if the logged in account was deleted
            if (id === this.accountValue.id)
                this.logout();
       }));
  }

  logout() {
    this.accountSubject.next(null);
    this.router.navigate(['/pages/authentication/login']);
  }
  
}
