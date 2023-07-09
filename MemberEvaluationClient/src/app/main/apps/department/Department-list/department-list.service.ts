import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Department } from 'app/auth/models';
import { environment } from 'environments/environment';

import { BehaviorSubject, Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class DepartmentListService implements Resolve<any> {
  public rows: any;
  public onDepartmentListChanged: BehaviorSubject<any>;
  private accountSubject: BehaviorSubject<Department>;
  public account: Observable<Department>;
  /**
   * Constructor
   */
  constructor(private router: Router,private _httpClient: HttpClient) {
    // Set the defaults
    this.onDepartmentListChanged = new BehaviorSubject({});
    this.accountSubject = new BehaviorSubject<Department>(null);
    this.account = this.accountSubject.asObservable();
  }
  public get accountValue(): Department {
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
      this._httpClient.get(`${environment.apiUrl}/departments`).subscribe((response: any) => {
        this.rows = response;
        this.onDepartmentListChanged.next(this.rows);
        resolve(this.rows);
      }, reject);
    });
  }

  delete(id: string) {
    return this._httpClient.delete(`${environment.apiUrl}/departments/${id}`)
        .pipe(finalize(() => {
           // auto logout if the logged in account was deleted
            if (id === this.accountValue.id)
                this.logout();
       }));
  }

  logout() {
    this.accountSubject.next(null);
    this.router.navigate(['/pages/authentication/login']);
  }
  
}
