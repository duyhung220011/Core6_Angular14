import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { CoreCommonModule } from '@core/common.module';

import { AuthForgotPasswordV1Component } from './auth-forgot-password/auth-forgot-password.component';
import { AuthResetPasswordV1Component } from './auth-reset-password-v1/auth-reset-password-v1.component';
import { AuthRegisterV1Component } from './auth-register/auth-register.component';
import { AuthLoginV1Component } from './auth-login/auth-login.component';

// routing
const routes: Routes = [
  {
    path: 'authentication/login',
    component: AuthLoginV1Component
  },
  {
    path: 'authentication/register',
    component: AuthRegisterV1Component
  },
  {
    path: 'authentication/reset-password',
    component: AuthResetPasswordV1Component
  },
  {
    path: 'authentication/forgot-password',
    component: AuthForgotPasswordV1Component
  }
];

@NgModule({
  declarations: [
    AuthLoginV1Component,
    AuthRegisterV1Component,
    AuthForgotPasswordV1Component,
    AuthResetPasswordV1Component
  ],
  imports: [CommonModule, RouterModule.forChild(routes), NgbModule, FormsModule, ReactiveFormsModule, CoreCommonModule]
})
export class AuthenticationModule {}
