import { CoreMenu } from '@core/types';

//? DOC: http://localhost:7777/demo/vuexy-angular-admin-dashboard-template/documentation/guide/development/navigation-menus.html#interface

export const menu: CoreMenu[] = [
  // Dashboard

  // Apps & Pages
  {
    id: 'apps',
    type: 'section',
    title: 'List Employee',
    translate: 'MENU.APPS.SECTION',
    icon: 'package',
    children: [
      {
        id: 'users',
        title: 'User',
        translate: 'MENU.APPS.USER.COLLAPSIBLE',
        type: 'collapsible',
        icon: 'user',
        children: [
          {
            id: 'list',
            title: 'List',
            translate: 'MENU.APPS.USER.LIST',
            type: 'item',
            icon: 'circle',
            url: 'apps/user/user-list'
          },
          {
            id: 'new',
            title: 'Add new',
            translate: 'MENU.APPS.USER.EDIT',
            type: 'item',
            icon: 'circle',
            url: 'apps/user/user-new'
          }
        ]
      },
      {
        id: 'department',
        title: 'Department',
        translate: 'MENU.APPS.DEPARTMENT.COLLAPSIBLE',
        type: 'collapsible',
        icon: 'briefcase',
        children: [
          {
            id: 'list',
            title: 'List',
            translate: 'MENU.APPS.DEPARTMENT.LIST',
            type: 'item',
            icon: 'circle',
            url: 'apps/department/department-list'
          },
          {
            id: 'new',
            title: 'Add new',
            translate: 'MENU.APPS.DEPARTMENT.EDIT',
            type: 'item',
            icon: 'circle',
            url: 'apps/department/department-new'
          }
        ]
      },
      {
        id: 'role',
        title: 'Role',
        translate: 'MENU.APPS.ROLE.COLLAPSIBLE',
        type: 'collapsible',
        icon: 'users',
        children: [
          {
            id: 'list',
            title: 'List',
            translate: 'MENU.APPS.ROLE.LIST',
            type: 'item',
            icon: 'circle',
            url: 'apps/role/role-list'
          },
          {
            id: 'new',
            title: 'Add new',
            translate: 'MENU.APPS.ROLE.EDIT',
            type: 'item',
            icon: 'circle',
            url: 'apps/role/role-new'
          }
        ]
      },
      {
        id: 'skill',
        title: 'Skill',
        translate: 'MENU.APPS.SKILL.COLLAPSIBLE',
        type: 'collapsible',
        icon: 'chevrons-up',
        children: [
          {
            id: 'list',
            title: 'List',
            translate: 'MENU.APPS.SKILL.LIST',
            type: 'item',
            icon: 'circle',
            url: 'apps/skill/skill-list'
          },
          {
            id: 'new',
            title: 'Add new',
            translate: 'MENU.APPS.SKILL.EDIT',
            type: 'item',
            icon: 'circle',
            url: 'apps/skill/skill-new'
          }
        ]
      },
      {
        id: 'hung',
        title: 'Hung',
        translate: 'MENU.APPS.HUNG.COLLAPSIBLE',
        type: 'collapsible',
        icon: 'chevrons-up',
        children: [
          {
            id: 'list',
            title: 'List',
            translate: 'MENU.APPS.SKILL.LIST',
            type: 'item',
            icon: 'circle',
            //url: 'apps/skill/skill-list'
          },
          {
            id: 'new',
            title: 'Add new',
            translate: 'MENU.APPS.SKILL.EDIT',
            type: 'item',
            icon: 'circle',
            //url: 'apps/skill/skill-new'
          }
        ]
      }
    ]
  }
];
