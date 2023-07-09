import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { first } from 'rxjs/operators';

import { RoleNewService } from 'app/main/apps/role/role-new/role-new.service';
import { AlertService } from 'app/main/service/alert.service';
import { MustMatch } from 'app/main/forms/form-validation/_helpers/must-match.validator';

@Component({
  selector: 'app-role-new',
  templateUrl: './role-new.component.html',
  styleUrls: ['./role-new.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class RoleNewComponent implements OnInit {
    // Form object to access form values
    form: FormGroup;
    // Boolean value to know whether form is submitted
    submitted = false;
    isLoading = false;
    errorMessage: string;

    constructor(
      private formBuilder: FormBuilder,
      private router: Router,
      private route: ActivatedRoute,
      private alertService: AlertService,
      private roleService: RoleNewService
    ) {}

    ngOnInit() {
      this.form = this.formBuilder.group({
        roleId: ['', Validators.required],
        roleName: ['', Validators.required],
       
      }, 
     
    )}
  
    handleError(error: any): void {
      this.hideLoading();
      this.errorMessage = 'Error Loading Roles';
      console.log(error);
      
      setTimeout(()=>this.errorMessage = undefined, 4000)
    }

  
    // convenience getter for easy access to form fields
    get f() { return this.form.controls; }
    
     onSubmit() {

      this.submitted = true;
      this.alertService.clear();

      if (this.form.invalid) {
        return;
      }
      this.createAccount();
    }
    
    showLoading() {
      this.isLoading = true;
    }
  
    hideLoading() {
      this.isLoading = false;
    }

    private createAccount() {
      this.roleService.create(this.form.value)
          .pipe(first())
          .subscribe({
              next: () => {
                  this.router.navigate(['/apps/role/role-list'], { relativeTo: this.route });
                  this.isLoading = true;
                },
              error: error => {
                  this.alertService.error(error);
                  this.isLoading = false;
              }
          });
  }
}
