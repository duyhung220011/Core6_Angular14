import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { environment } from 'environments/environment';

import { BehaviorSubject, Observable } from 'rxjs';

const baseUrl = `${environment.apiUrl}/roles/add`;

@Injectable()
export class RoleNewService {
  public apiData: any;
  public onRoleNewChanged: BehaviorSubject<any>;

  /**
   * Constructor
   *
   * @param {HttpClient} _httpClient
   */
  constructor(private _httpClient: HttpClient) {
    // Set the defaults
    this.onRoleNewChanged = new BehaviorSubject({});
  }

   create(params) {
    return this._httpClient.post(baseUrl, params);
  }
}
