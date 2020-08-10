import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { User } from 'src/app/shared/models/user';
import { Login } from 'src/app/shared/models/login';
import { AuthService } from 'src/app/core/services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    // password should have at least 8 characters with at least one uppercase, one lowercase and one number
    // But don't put these validation at the login form
    password: new FormControl('', Validators.required),
  });
  // use this property to display warning message in the UI
  invalidLogin: boolean;
  errorMsg: string;
  // redirect to previous page after successful login
  returnUrl: string;
  // return object from web api
  user: User;
  login: Login;

  constructor(
    private authService: AuthService,
    private activatedRouter: ActivatedRoute,
    // redirect between pages
    private router: Router
  ) {}

  ngOnInit(): void {
    // ToDo if user successfully login, return to the previous page.
    // init return url
  }
  /**
   * Two types of forms in angular
   *   1. Template driven forms - simple forms where form logic is not complex
   *   2. reactive forms - complex forms, with complex validation
   * Route Guards (filters in .Net)
   *   Angular will check the toke created is valid
   *   backend: api will check the JWT token from angular and only if its valid it will send the data
   */

  onSubmit() {
    if (this.loginForm.valid) {
      this.login = { ...this.loginForm.value };

      this.authService.login(this.login).subscribe(
        (response) => {
          if (response) this.invalidLogin = false;
          // ToDo decode the response and save to user object
          this.router.navigate(['/']);
        },
        (err) => {
          this.invalidLogin = true;
          this.errorMsg = err.error?.errorMessage;
        }
      );
    }
  }
}
