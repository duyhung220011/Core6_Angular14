import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder,FormControl, FormGroup, Validators } from '@angular/forms';

import { first, takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { Subscription } from 'rxjs';
import { CoreConfigService } from '@core/services/config.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'app/auth/service';
import { AlertService } from 'app/main/service/alert.service';

@Component({
  selector: 'app-auth-login',
  templateUrl: './auth-login.component.html',
  styleUrls: ['./auth-login.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AuthLoginV1Component implements OnInit {
  //  Public
  public coreConfig: any;
  public loginForm: FormGroup;
  public loading = false;
  public submitted = false;
  loginSubscription: Subscription;
  public passwordTextType: boolean;
  public returnUrl: string;
  public error = '';
  
  // Private
  private _unsubscribeAll: Subject<any>;

  /**
   * Constructor
   *
   * @param {CoreConfigService} _coreConfigService
   * @param {FormBuilder} _formBuilder
   */
  // constructor(
  //   private _coreConfigService: CoreConfigService, 
  //   private _formBuilder: FormBuilder,
  //   private _route: ActivatedRoute,
  //   private _router: Router,
  //   private _authenticationService: AuthenticationService,
  //   private alertService: AlertService
  //   ) {
  //   // redirect to home if already logged in
  //   if (this._authenticationService.currentUserValue) {
  //     this._router.navigate(['/']);
  //   }

  //   this._unsubscribeAll = new Subject();

  //   // Configure the layout
  //   this._coreConfigService.config = {
  //     layout: {
  //       navbar: {
  //         hidden: true
  //       },
  //       menu: {
  //         hidden: true
  //       },
  //       footer: {
  //         hidden: true
  //       },
  //       customizer: false,
  //       enableLocalStorage: false
  //     }
  //   };
  // }

  // // convenience getter for easy access to form fields
  // get f() {
  //   return this.loginForm.controls;
  // }

  // /**
  //  * Toggle password
  //  */
  // togglePasswordTextType() {
  //   this.passwordTextType = !this.passwordTextType;
  // }

  // /**
  //  * On Submit
  //  */
  // onSubmit() {
  //   this.submitted = true;

  //   // stop here if form is invalid
  //   if (this.loginForm.invalid) {
  //     return;
  //   }

  //   // Login
  //   this.loading = true;
  //   this._authenticationService.login(this.f.userId.value, this.f.password.value)
  //     .pipe(first())
  //     .subscribe({
  //       next: () => {
  //             // get return url from query parameters or default to home page
  //             const returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/';
  //             this._router.navigateByUrl(returnUrl);
  //         },
  //         error: error => {
  //             this.alertService.error(error);
  //             this.loading = false;
  //         }
  //     });
  // }

  // // Lifecycle Hooks
  // // -----------------------------------------------------------------------------------------------------

  // /**
  //  * On init
  //  */
  // ngOnInit(): void {
  //   this.loginForm = this._formBuilder.group({
  //     userId: ['100000', [Validators.required]],
  //     password: ['123456', Validators.required]
  //   });

  //   // get return url from route parameters or default to '/'
  //   this.returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/';
    
  //   // Subscribe to config changes
  //   this._coreConfigService.config.pipe(takeUntil(this._unsubscribeAll)).subscribe(config => {
  //     this.coreConfig = config;
  //   });
  // }
  
  // /**
  //  * On destroy
  //  */
  // ngOnDestroy(): void {
  //   // Unsubscribe from all subscriptions
  //   this._unsubscribeAll.next();
  //   this._unsubscribeAll.complete();
  // }
  constructor(
    private _coreConfigService: CoreConfigService,
    private _formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _router: Router,
    private _authenticationService: AuthenticationService
  ) {
    // redirect to home if already logged in
    if (this._authenticationService.currentUserValue) {
      this._router.navigate(['/']);
    }

    this._unsubscribeAll = new Subject();

    // Configure the layout
    this._coreConfigService.config = {
      layout: {
        navbar: {
          hidden: true
        },
        menu: {
          hidden: true
        },
        footer: {
          hidden: true
        },
        customizer: false,
        enableLocalStorage: false
      }
    };
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.loginForm.controls;
  }

  /**
   * Toggle password
   */
  togglePasswordTextType() {
    this.passwordTextType = !this.passwordTextType;
  }

  login() {
    this.submitted = true;
    if (this.loginForm.valid) {
      this.submitData(this.loginForm.value);
    }
  }
  submitData(formData: any) {
    this.loading = true;
    this.loginSubscription = this._authenticationService.login(formData).subscribe(
      (response) => {
        this._router.navigate([[this.returnUrl]]);
        this.loading = false;
      },
      (error) => {
        this.error = error;
        this.loading = false;
        if (error.error.message === 'FieldException')
          error.error.errors.forEach((element) =>
            this.loginForm.controls[element.field]?.setErrors({
              serverValidationError: element.message,
            })
          );
        else throw new Error(error);
      }
    );
  }

  
  // Lifecycle Hooks
  // -----------------------------------------------------------------------------------------------------

  /**
   * On init
   */
   ngOnInit(): void {
    // this._authenticationService.redirectIfLoggedIn();
    this.initForm();
  }

  initForm() {
    this.loginForm = new FormGroup({
      username: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
    });

    // get return url from route parameters or default to '/'
    this.returnUrl = this._route.snapshot.queryParams['dashboard'] || '/';

    // Subscribe to config changes
    this._coreConfigService.config.pipe(takeUntil(this._unsubscribeAll)).subscribe(config => {
      this.coreConfig = config;
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
