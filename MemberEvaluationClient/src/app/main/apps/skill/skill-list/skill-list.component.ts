import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { ColumnMode, DatatableComponent } from '@swimlane/ngx-datatable';

import { Subject } from 'rxjs';
import { first, takeUntil } from 'rxjs/operators';

import { CoreConfigService } from '@core/services/config.service';
import { CoreSidebarService } from '@core/components/core-sidebar/core-sidebar.service';

import { SkillListService } from 'app/main/apps/skill/skill-list/skill-list.service';

@Component({
  selector: 'app-skill-list',
  templateUrl: './skill-list.component.html',
  styleUrls: ['./skill-list.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class SkillListComponent implements OnInit {
  // Public
  public sidebarToggleRef = false;
  public rows;
  public selectedOption = 10;
  public ColumnMode = ColumnMode;
  public temp = [];
  public previousSkillFilter = '';
  

  public selectSkill: any = [
    
    { name: 'Level 1', value: 'Level 1' },
    { name: 'Level 2', value: 'Level 2' },
    { name: 'Level 3', value: 'Level 3' },
    { name: 'Level 4', value: 'Level 4' },
    { name: 'Level 5', value: 'Level 5' }
  ];

  

  public selectedSkill = [];
  public searchValue = '';

  // Decorator
  @ViewChild(DatatableComponent) table: DatatableComponent;

  // Private
  private tempData = [];
  private _unsubscribeAll: Subject<any>;

  /**
   * Constructor
   */
  constructor(
    private _skillListService: SkillListService,
    private _coreSidebarService: CoreSidebarService,
    private _coreConfigService: CoreConfigService
  ) {
    this._unsubscribeAll = new Subject();
  }

  // Public Methods
  // -----------------------------------------------------------------------------------------------------

  /**
   * filterUpdate
   */
  filterUpdate(event) {
    // Reset ng-select on search
    this.selectedSkill = this.selectSkill[0];

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
  filterRows(skillFilter): any[] {
    // Reset search on select change
    this.searchValue = '';

    skillFilter = skillFilter.toLowerCase();
    

    return this.tempData.filter(row => {
      const isPartialNameMatch = row.level.toLowerCase().indexOf(skillFilter) !== -1 || !skillFilter;
      
      return isPartialNameMatch ;
    });
  }

  // Lifecycle Hooks
  // -----------------------------------------------------------------------------------------------------
  /**
   * On init
   */
  ngOnInit(): void {
    // Subscribe config change
    this._coreConfigService.config.pipe(takeUntil(this._unsubscribeAll)).subscribe(config => {
      //! If we have zoomIn route Transition then load datatable after 450ms(Transition will finish in 400ms)
      if (config.layout.animation === 'zoomIn') {
        setTimeout(() => {
          this._skillListService.onSkillListChanged.pipe(takeUntil(this._unsubscribeAll)).subscribe(response => {
            this.rows = response;
            this.tempData = this.rows;
          });
        }, 450);
      } else {
        this._skillListService.onSkillListChanged.pipe(takeUntil(this._unsubscribeAll)).subscribe(response => {
          this.rows = response;
          this.tempData = this.rows;
        });
      }
    });
  }

  deleteAccount(id: string) {
    const account = this.rows.find(x => x.id === id);
    account.isDeleting = true;
    this._skillListService.delete(id)
        .pipe(first())
        .subscribe(() => {
            this.rows = this.rows.filter(x => x.id !== id) 
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
