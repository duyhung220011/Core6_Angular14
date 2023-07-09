import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { first } from 'rxjs/operators';

import { UserNewService } from 'app/main/apps/user/user-new/user-new.service';
import { AlertService } from 'app/main/service/alert.service';
import { MustMatch } from 'app/main/forms/form-validation/_helpers/must-match.validator';

@Component({
  selector: 'app-user-new',
  templateUrl: './user-new.component.html',
  styleUrls: ['./user-new.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class UserNewComponent implements OnInit {
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
      private userService: UserNewService
    ) {}

    ngOnInit() {
      this.form = this.formBuilder.group({
        userId: ['', Validators.required],
        fullName: ['', Validators.required],
        password: ['', [Validators.minLength(6),Validators.required]],
        confirmPassword: [''],
        department: ['', Validators.required],
        email: ['', Validators.required,Validators.email],
        userRole: ['', Validators.required]
      }, 
      { validator: MustMatch('password', 'confirmPassword') });
    }
  
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
      //if(this.createAccount){
        //return this.router.navigate(['/apps/user/user-list'], { relativeTo: this.route });
      //}
      
    }
    
    showLoading() {
      this.isLoading = true;
    }
  
    hideLoading() {
      this.isLoading = false;
    }

    private createAccount() {
      this.userService.create(this.form.value)
          .pipe(first())
          .subscribe({
              next: () => {
                  //this.alertService.success('Account created successfully', { keepAfterRouteChange: true });
                     this.router.navigate(['/apps/user/user-list'], { relativeTo: this.route });
                     this.isLoading = true;
              },
              error: error => {
                  this.alertService.error(error);
                  this.isLoading = false;
              }
          });
  }
}
