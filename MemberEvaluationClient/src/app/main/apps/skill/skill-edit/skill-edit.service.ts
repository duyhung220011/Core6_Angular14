import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Skill } from 'app/auth/models/skill';
import { environment } from 'environments/environment';

import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';


@Injectable()
export class SkillEditService implements Resolve<Skill> {
  public apiData: any;
  public onSkillEditChanged: BehaviorSubject<Skill>;

  /**
   * Constructor
   */
  constructor(private _httpClient: HttpClient) {
    // Set the defaults
    this.onSkillEditChanged = new BehaviorSubject<Skill>(null);
  }
  public get accountValue(): Skill {
    return this.onSkillEditChanged.value;
  }
  /**
   * Resolver
   */
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Skill> | Promise<Skill> | any {
    return new Promise<void>((resolve, reject) => {
      Promise.all([this.getAllData()]).then(() => {
        resolve();
      }, reject);
    });
  }

  /**
   * Get API Data
   */
  getAllData(): Promise<Skill[]> {
    return new Promise((resolve, reject) => {
      this._httpClient.get(`${environment.apiUrl}/skill`).subscribe((response: Skill) => {
        this.apiData = response;
        this.onSkillEditChanged.next(this.apiData);
        resolve(this.apiData);
      }, reject);
    });
  }

  getById(id: string) {
    return this._httpClient.get<Skill>(`${environment.apiUrl}/skill/${id}`);
  }

  update(id, params) {
    return this._httpClient.put(`${environment.apiUrl}/skill/${id}`, params)
        .pipe(map((account: any) => {
            // update the current account if it was updated
            if (account.id === this.accountValue.id) {
                // publish updated account to subscribers
                account = { ...this.accountValue, ...account };
                this.onSkillEditChanged.next(account);
            }
            return account;
        }));
  }
}
