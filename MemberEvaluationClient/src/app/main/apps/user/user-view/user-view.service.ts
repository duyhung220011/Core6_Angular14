import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve,Router, RouterStateSnapshot } from '@angular/router';
import { environment } from 'environments/environment';
import { Report } from 'app/auth/models/report';
import { BehaviorSubject, Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
@Injectable({ providedIn: 'root' })
export class UserViewService implements Resolve<any> {
  public rows: any;
  public onUserViewChanged: BehaviorSubject<any>;
  public onReportViewChanged: BehaviorSubject<any>;
  public id;
  private accountSubject: BehaviorSubject<Report>;
  public account: Observable<Report>;
  /**
   * Constructor
   *
   * @param {HttpClient} _httpClient
   */
  constructor(
    private router: Router,
    private _httpClient: HttpClient
    
    ) {
    // Set the defaults
    this.onUserViewChanged = new BehaviorSubject({});
    this.onReportViewChanged = new BehaviorSubject({});
    this.accountSubject = new BehaviorSubject<Report>(null);
    this.account = this.accountSubject.asObservable();
  }
  public get accountValue(): Report {
    return this.accountSubject.value;
  }
  /**
   * Resolver
   *
   * @param {ActivatedRouteSnapshot} route
   * @param {RouterStateSnapshot} state
   * @returns {Observable<any> | Promise<any> | any}
   */
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {
    let currentId = Number(route.paramMap.get('id'));
    return new Promise<void>((resolve, reject) => {
      Promise.all([this.getApiData(currentId)]).then(() => {
        resolve();
      }, reject);
      Promise.all([this.getDataTableRows()]).then(() => {
        resolve();
      }, reject);
    });
  }

  /**
   * Get rows
   */
  getApiData(id: number): Promise<any[]> {

    return new Promise((resolve, reject) => {
      this._httpClient.get(`${environment.apiUrl}/users/${id}`).subscribe((response: any) => {
        this.rows = response;
        this.onUserViewChanged.next(this.rows);
        resolve(this.rows);
      }, reject);
    });
  }
  getDataTableRows(): Promise<any[]> {
    return new Promise((resolve, reject) => {
      this._httpClient.get(`${environment.apiUrl}/reports`).subscribe((response: any) => {
        this.rows = response;
        this.onReportViewChanged.next(this.rows);
        resolve(this.rows);
      }, reject);
    });
  }
  delete(id: string) {
    return this._httpClient.delete(`${environment.apiUrl}/reports/${id}`)
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
