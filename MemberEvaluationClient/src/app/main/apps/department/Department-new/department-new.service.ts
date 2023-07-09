import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { environment } from 'environments/environment';

import { BehaviorSubject, Observable } from 'rxjs';

const baseUrl = `${environment.apiUrl}/departments/add`;

@Injectable()
export class DepartmentNewService {
  public apiData: any;
  public onDepartmentNewChanged: BehaviorSubject<any>;

  /**
   * Constructor
   *
   * @param {HttpClient} _httpClient
   */
  constructor(private _httpClient: HttpClient) {
    // Set the defaults
    this.onDepartmentNewChanged = new BehaviorSubject({});
  }

   create(params) {
    return this._httpClient.post(baseUrl, params);
  }
}
