import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { environment } from 'environments/environment';

import { BehaviorSubject, Observable } from 'rxjs';

const baseUrl = `${environment.apiUrl}/skill/add`;

@Injectable()
export class SkillNewService {
  public apiData: any;
  public onSkillNewChanged: BehaviorSubject<any>;

  /**
   * Constructor
   *
   * @param {HttpClient} _httpClient
   */
  constructor(private _httpClient: HttpClient) {
    // Set the defaults
    this.onSkillNewChanged = new BehaviorSubject({});
  }

   create(params) {
    return this._httpClient.post(baseUrl, params);
  }
}
