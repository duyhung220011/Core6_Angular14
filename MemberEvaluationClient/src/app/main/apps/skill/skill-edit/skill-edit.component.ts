import { Component, OnInit, OnDestroy, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';

import { Subject } from 'rxjs';
import { first, takeUntil } from 'rxjs/operators';
import { FlatpickrOptions } from 'ng2-flatpickr';
import { cloneDeep } from 'lodash';

import { SkillEditService } from 'app/main/apps/skill/skill-edit/skill-edit.service';
import { AlertService } from 'app/main/service/alert.service';
import { MustMatch } from 'app/main/forms/form-validation/_helpers/must-match.validator';

@Component({
  selector: 'app-skill-edit',
  templateUrl: './skill-edit.component.html',
  styleUrls: ['./skill-edit.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class SkillEditComponent implements OnInit, OnDestroy {
  // Public
  public url = this.router.url;
  public urlLastValue;
  public rows;
  public currentRow;
  public tempRow;
  public avatarImage: string;

  form: FormGroup;
  id: string;
  submitted = false;
  isLoading = false;
  
  @ViewChild('accountForm') accountForm: NgForm;

  public birthDateOptions: FlatpickrOptions = {
    altInput: true
  };


  // Private
  private _unsubscribeAll: Subject<any>;

  /**
   * Constructor
   *
   * 
   */
  constructor(
    private formBuilder: FormBuilder,
    private router: Router, 
    private route: ActivatedRoute,
    private _skillEditService: SkillEditService,
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
   */
  submit(form) {
    if (form.valid) {
      console.log('Submitted...!');
    }
  }
  onSubmit() {

    this.submitted = true;

    if (this.form.invalid) {
      return;
    }
    this.updateAccount();
  }

  private updateAccount() {
    this._skillEditService.update(this.id, this.form.value)
        .pipe(first())
        .subscribe({
            next: () => {
                this.router.navigate(['../../'], { relativeTo: this.route });
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
    this._skillEditService.onSkillEditChanged.pipe(takeUntil(this._unsubscribeAll)).subscribe(response => {
      this.rows = response;
      this.rows.map(row => {
        if (row.id == this.urlLastValue) {
          this.currentRow = row;
          this.tempRow = cloneDeep(row);
        }
      });
    });
    this.form = this.formBuilder.group({
      skillId: ['', Validators.required],
      title: ['', Validators.required],
      description: ['', Validators.required],
      level: ['', Validators.required],
      grade: ['', Validators.required]
    }); 
    
  };

  /**
   * On destroy
   */
  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }
}
