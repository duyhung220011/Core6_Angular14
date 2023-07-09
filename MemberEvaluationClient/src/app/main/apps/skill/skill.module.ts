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

import { SkillEditComponent } from 'app/main/apps/skill/skill-edit/skill-edit.component';
import { SkillEditService } from 'app/main/apps/skill/skill-edit/skill-edit.service';

import { SkillListComponent } from 'app/main/apps/skill/skill-list/skill-list.component';
import { SkillListService } from 'app/main/apps/skill/skill-list/skill-list.service';

import { SkillViewComponent } from 'app/main/apps/skill/skill-view/skill-view.component';
import { SkillViewService } from 'app/main/apps/skill/skill-view/skill-view.service';
import { SkillNewComponent } from './skill-new/skill-new.component';
import { SkillNewService } from './skill-new/skill-new.service';

// routing
const routes: Routes = [
  {
    path: 'skill-list',
    component: SkillListComponent,
    resolve: {
      uls: SkillListService
    },
    data: { animation: 'SkillListComponent' }
  },
  {
    path: 'skill-view/:id',
    component: SkillViewComponent,
    resolve: {
      data: SkillViewService
    },
    data: { path: 'view/:id', animation: 'SkillViewComponent' }
  },
  {
    path: 'skill-new',
    component: SkillNewComponent,
    data: { animation: 'SkillNewComponent' }
  },
  {
    path: 'skill-edit/:id',
    component: SkillEditComponent,
    resolve: {
      ues: SkillEditService
    },
    data: { animation: 'SkillEditComponent' }
  },
  {
    path: 'skill-view',
    redirectTo: '/apps/skill/skill-view/2' // Redirection
  },
  {
    path: 'skill-edit',
    redirectTo: '/apps/skill/skill-edit/2' // Redirection
  }
];

@NgModule({
  declarations: [SkillListComponent, SkillViewComponent, SkillEditComponent, SkillNewComponent],
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
  providers: [SkillListService, SkillViewService, SkillEditService, SkillNewService]
})
export class SkillModule {}
