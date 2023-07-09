import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Department } from 'app/auth/models';
import { environment } from 'environments/environment';

import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';


@Injectable()
export class DepartmentEditService implements Resolve<Department> {
  public apiData: any;
  public onDepartmentEditChanged: BehaviorSubject<Department>;

  /**
   * Constructor
   */
  constructor(private _httpClient: HttpClient) {
    // Set the defaults
    this.onDepartmentEditChanged = new BehaviorSubject<Department>(null);
  }
  public get accountValue(): Department {
    return this.onDepartmentEditChanged.value;
  }
  /**
   * Resolver
   */
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Department> | Promise<Department> | any {
    return new Promise<void>((resolve, reject) => {
      Promise.all([this.getAllData()]).then(() => {
        resolve();
      }, reject);
    });
  }

  /**
   * Get API Data
   */
  getAllData(): Promise<Department[]> {
    return new Promise((resolve, reject) => {
      this._httpClient.get(`${environment.apiUrl}/departments`).subscribe((response: Department) => {
        this.apiData = response;
        this.onDepartmentEditChanged.next(this.apiData);
        resolve(this.apiData);
      }, reject);
    });
  }

  getById(id: string) {
    return this._httpClient.get<Department>(`${environment.apiUrl}/departments/${id}`);
  }

  update(id, params) {
    return this._httpClient.put(`${environment.apiUrl}/departments/${id}`, params)
        .pipe(map((account: any) => {
            // update the current account if it was updated
            if (account.id === this.accountValue.id) {
                // publish updated account to subscribers
                account = { ...this.accountValue, ...account };
                this.onDepartmentEditChanged.next(account);
            }
            return account;
        }));
  }
}
