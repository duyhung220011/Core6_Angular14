import { Component, OnInit, OnDestroy, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';

import { Subject } from 'rxjs';
import { first, takeUntil } from 'rxjs/operators';
import { FlatpickrOptions } from 'ng2-flatpickr';
import { cloneDeep } from 'lodash';

import { UserEditService } from 'app/main/apps/user/user-edit/user-edit.service';
import { AlertService } from 'app/main/service/alert.service';
import { MustMatch } from 'app/main/forms/form-validation/_helpers/must-match.validator';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class UserEditComponent implements OnInit, OnDestroy {
  // Public
  public url = this.router.url;
  public urlLastValue;
  public rows;
  public currentRow;
  public tempRow;
  public avatarImage: string;

  form: FormGroup;
  fromReview:FormGroup;
  id: string;
  submitted = false;
  isLoading = false;
  errorMessage: string;
  
  @ViewChild('accountForm') accountForm: NgForm;

  public birthDateOptions: FlatpickrOptions = {
    altInput: true
  };


  // Private
  private _unsubscribeAll: Subject<any>;

  /**
   * Constructor
   */
  constructor(
    private formBuilder: FormBuilder,
    private router: Router, 
    private route: ActivatedRoute,
    private _userEditService: UserEditService,
    private alertService: AlertService
    ) {
    this._unsubscribeAll = new Subject();
    this.urlLastValue = this.url.substr(this.url.lastIndexOf('/') + 1);
  }
  

  // Public Methods
  // -----------------------------------------------------------------------------------------------------

  /**
   * Reset Form With Default Values
   */
  resetFormWithDefaultValues() {
    this.accountForm.resetForm(this.tempRow);
  }

  /**
   * Submit
   *
   * @param form
   * @param fromReview
   */
   handleError(error: any): void {
    this.hideLoading();
    this.errorMessage = 'Error Loading Roles';
    console.log(error);
    
    setTimeout(()=>this.errorMessage = undefined, 4000)
  }
  get f() { return this.fromReview.controls; }
  submit(form) {
    if (form.valid) {
      console.log('Submitted...!');
    }
  }
  // submitReview(fromReview) {
  //   if (fromReview.valid) {
  //     console.log('Submitted...!');
  //   }
  // }
  onSubmit() {

    this.submitted = true;

    if (this.form.invalid) {
      return;
    }
    this.updateAccount();
   // this.router.navigate(['/apps/user/user-list'], { relativeTo: this.route });
    // this.router.navigate(['/apps/user/user-list/user-list.component'], { relativeTo: this.route});
    
  }
  // onClick() {

  //   this.submitted = true;

  //   if (this.fromReview.invalid) {
  //     return;
  //   }
    
  //   this.createAccount();
  // }
  
  showLoading() {
    this.isLoading = true;
  }

  hideLoading() {
    this.isLoading = false;
  }
  private updateAccount() {
    this._userEditService.update(this.id, this.form.value)
        .pipe(first())
        .subscribe({
            next: () => {
                this.router.navigate(['/apps/user/user-list'], { relativeTo: this.route});
            },
            error: error => {
                this.alertService.error(error);
                this.isLoading = false;
            }
        });
  }
  private createAccount() {
    this._userEditService.create(this.fromReview.value)
        .pipe(first())
        .subscribe({
            next: () => {
                this.router.navigate(['../user/user-view'], { relativeTo: this.route });
                this.isLoading = true;
            },
            error: error => {
                this.alertService.error(error);
                this.isLoading = false;
            }
        });
}
  // Lifecycle Hooks
  // -----------------------------------------------------------------------------------------------------
  /**
   * On init
   */
  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this._userEditService.onUserEditChanged.pipe(takeUntil(this._unsubscribeAll)).subscribe(response => {
      this.rows = response;
      this.rows.map(row => {
        if (row.id == this.urlLastValue) {
          this.currentRow = row;
          this.tempRow = cloneDeep(row);
        }
      });
    });
    this.form = this.formBuilder.group({
      userId: ['', Validators.required],
      fullName: ['', Validators.required],
      password: ['', [Validators.minLength(6),Validators.required]],
      confirmPassword: [''],
      email: ['', [Validators.required,Validators.email]],
      userRole: ['', Validators.required],
      department: ['', Validators.required]
      // title: ['', Validators.required],
      // grade: ['', Validators.required],
      // reason: ['', Validators.required],
      // comment: ['', Validators.required],
    }, 
    { validator: MustMatch('password', 'confirmPassword') });
    // this.fromReview = this.formBuilder.group({
    //   title: ['', Validators.required],
    //   grade: ['', Validators.required],
    //   reason: ['', Validators.required],
    //   comment: ['', Validators.required],
    // }); 
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
