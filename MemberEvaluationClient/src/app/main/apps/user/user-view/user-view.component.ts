import { Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { ColumnMode, DatatableComponent } from '@swimlane/ngx-datatable';
import { Router } from '@angular/router';

import { Subject } from 'rxjs';
import { first,takeUntil } from 'rxjs/operators';
import { CoreConfigService } from '@core/services/config.service';
import { CoreSidebarService } from '@core/components/core-sidebar/core-sidebar.service';

import { UserViewService } from 'app/main/apps/user/user-view/user-view.service';

@Component({
  selector: 'app-user-view',
  templateUrl: './user-view.component.html',
  styleUrls: ['./user-view.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class UserViewComponent implements OnInit, OnDestroy {
  // public
  public url = this.router.url;
  public lastValue;
  public data;
  public sidebarToggleRef = false;
  public rows;
  public selectedOption = 10;
  public ColumnMode = ColumnMode;
  public temp = [];
  public previousSkillFilter = '';
  public searchValue;
  // Decorator
  @ViewChild(DatatableComponent) table: DatatableComponent;
  // private
  private _unsubscribeAll: Subject<any>;
  private tempData = [];
 
  /**
   * Constructor
   *
   * @param {Router} router
   * @param {UserViewService} _userViewService
   */
  constructor(
    private router: Router, 
    private _userViewService: UserViewService,
   // private _reportListService: ReportListService,
    private _coreSidebarService: CoreSidebarService,
    private _coreConfigService: CoreConfigService
    ) {
    this._unsubscribeAll = new Subject();
    this.lastValue = this.url.substr(this.url.lastIndexOf('/') + 1);
  }
  filterUpdate(event) {
    // Reset ng-select on search
    

    const val = event.target.value.toLowerCase();

    // Filter Our Data
    const temp = this.tempData.filter(function (d) {
      return d.title.toLowerCase().indexOf(val) !== -1 || !val;
    });

    // Update The Rows
    this.rows = temp;
    // Whenever The Filter Changes, Always Go Back To The First Page
    this.table.offset = 0;
  }

  /**
   * Toggle the sidebar
   */
  toggleSidebar(name): void {
    this._coreSidebarService.getSidebarRegistry(name).toggleOpen();
  }

  /**
   * Filter By Roles
   */
  filterByRole(event) {
    const filter = event ? event.value : '';
    this.previousSkillFilter = filter;
    this.temp = this.filterRows(filter);
    this.rows = this.temp;
  }

  

  /**
   * Filter Rows
   */
  filterRows(roleFilter): any[] {
    // Reset search on select change
    this.searchValue = '';

    roleFilter = roleFilter.toLowerCase();
    

    return this.tempData.filter(row => {
      const isPartialNameMatch = row.title.toLowerCase().indexOf(roleFilter) !== -1 || !roleFilter;
      
      return isPartialNameMatch ;
    });
  }
  // Lifecycle Hooks
  // -----------------------------------------------------------------------------------------------------
  /**
   * On init
   */
  ngOnInit(): void {
    this._userViewService.onUserViewChanged.pipe(takeUntil(this._unsubscribeAll)).subscribe(response => {
      this.data = response;
    });
    this._coreConfigService.config.pipe(takeUntil(this._unsubscribeAll)).subscribe(config => {
      //! If we have zoomIn route Transition then load datatable after 450ms(Transition will finish in 400ms)
      if (config.layout.animation === 'zoomIn') {
        setTimeout(() => {
          this._userViewService.onReportViewChanged.pipe(takeUntil(this._unsubscribeAll)).subscribe(response1 => {
            this.rows = response1;
            this.tempData = this.rows;
          });
        }, 450);
      } else {
        this._userViewService.onReportViewChanged.pipe(takeUntil(this._unsubscribeAll)).subscribe(response1 => {
          this.rows = response1;
          this.tempData = this.rows;
        });
      }
    });
  }

  /**
   * On destroy
   */
  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }
}
