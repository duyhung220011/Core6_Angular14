import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { environment } from 'environments/environment';

import { BehaviorSubject, Observable } from 'rxjs';

@Injectable()
export class UserNewService {
  public apiData: any;
  public onUserNewChanged: BehaviorSubject<any>;

  /**
   * Constructor
   */
  constructor(private _httpClient: HttpClient) {
    // Set the defaults
    this.onUserNewChanged = new BehaviorSubject({});
  }

   create(params) {
    return this._httpClient.post(`${environment.apiUrl}/users/add`, params);
  }
}
