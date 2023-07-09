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

import { RoleEditComponent } from 'app/main/apps/role/role-edit/role-edit.component';
import { RoleEditService } from 'app/main/apps/role/role-edit/role-edit.service';

import { RoleListComponent } from 'app/main/apps/role/role-list/role-list.component';
import { RoleListService } from 'app/main/apps/role/role-list/role-list.service';

import { RoleViewComponent } from 'app/main/apps/role/role-view/role-view.component';
import { RoleViewService } from 'app/main/apps/role/role-view/role-view.service';
import { RoleNewComponent } from './role-new/role-new.component';
import { RoleNewService } from './role-new/role-new.service';

// routing
const routes: Routes = [
  {
    path: 'role-list',
    component: RoleListComponent,
    resolve: {
      uls: RoleListService
    },
    data: { animation: 'RoleListComponent' }
  },
  {
    path: 'role-view/:id',
    component: RoleViewComponent,
    resolve: {
      data: RoleViewService
    },
    data: { path: 'view/:id', animation: 'RoleViewComponent' }
  },
  {
    path: 'role-new',
    component: RoleNewComponent,
    data: { animation: 'RoleEditComponent' }
  },
  {
    path: 'role-edit/:id',
    component: RoleEditComponent,
    resolve: {
      ues: RoleEditService
    },
    data: { animation: 'RoleEditComponent' }
  },
  {
    path: 'role-view',
    redirectTo: '/apps/role/role-view/2' // Redirection
  },
  {
    path: 'role-edit',
    redirectTo: '/apps/role/role-edit/2' // Redirection
  }
];

@NgModule({
  declarations: [RoleListComponent, RoleViewComponent, RoleEditComponent, RoleNewComponent],
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
  providers: [RoleListService, RoleViewService, RoleEditService, RoleNewService]
})
export class RoleModule {}
