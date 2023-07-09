import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { Ng2FlatpickrModule } from 'ng2-flatpickr';

import { CoreCommonModule } from '@core/common.module';
import { CoreDirectivesModule } from '@core/directives/directives';
import { CorePipesModule } from '@core/pipes/pipes.module';
import { CoreSidebarModule } from '@core/components';

import { DepartmentEditComponent } from 'app/main/apps/department/Department-edit/department-edit.component';
import { DepartmentEditService } from 'app/main/apps/department/Department-edit/department-edit.service';

import { DepartmentListComponent } from 'app/main/apps/department/Department-list/department-list.component';
import { DepartmentListService } from 'app/main/apps/department/Department-list/department-list.service';

import { DepartmentViewComponent } from 'app/main/apps/department/Department-view/department-view.component';
import { DepartmentViewService } from 'app/main/apps/department/Department-view/department-view.service';
import { DepartmentNewComponent } from './Department-new/department-new.component';
import { DepartmentNewService } from './Department-new/department-new.service';

// routing
const routes: Routes = [
  {
    path: 'department-list',
    component: DepartmentListComponent,
    resolve: {
      uls: DepartmentListService
    },
    data: { animation: 'DepartmentListComponent' }
  },
  {
    path: 'department-view/:id',
    component: DepartmentViewComponent,
    resolve: {
      data: DepartmentViewService
    },
    data: { path: 'view/:id', animation: 'DepartmentViewComponent' }
  },
  {
    path: 'department-new',
    component: DepartmentNewComponent,
    data: { animation: 'DepartmentEditComponent' }
  },
  {
    path: 'department-edit/:id',
    component: DepartmentEditComponent,
    resolve: {
      ues: DepartmentEditService
    },
    data: { animation: 'DepartmentEditComponent' }
  },
  {
    path: 'department-view',
    redirectTo: '/apps/department/Department-view/2' // Redirection
  },
  {
    path: 'department-edit',
    redirectTo: '/apps/department/Department-edit/2' // Redirection
  }
];

@NgModule({
  declarations: [DepartmentListComponent, DepartmentViewComponent, DepartmentEditComponent, DepartmentNewComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    CoreCommonModule,
    FormsModule,
    NgbModule,
    NgSelectModule,
    Ng2FlatpickrModule,
    NgxDatatableModule,
    CorePipesModule,
    CoreDirectivesModule,
    CoreSidebarModule
  ],
  providers: [DepartmentListService, DepartmentViewService, DepartmentEditService, DepartmentNewService]
})
export class DepartmentModule {}
