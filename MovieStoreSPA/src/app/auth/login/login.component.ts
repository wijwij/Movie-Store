import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
  });
  constructor() {}

  ngOnInit(): void {}
  /**
   * Route Guards (filters in .Net)
   *   Angular will check the toke created is valid
   *   backend: api will check the JWT token from angular and only if its valid it will send the data
   */

  onSubmit() {
    console.log(this.loginForm.value);
    // console.log(this.loginForm.invalid);
  }
}
