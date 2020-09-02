import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { ValidatorService } from 'src/app/core/services/validator.service';
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder,
} from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css'],
})
export class SignUpComponent implements OnInit {
  // Refactor to use FormBuilder service
  /*
  registerForm = new FormGroup({
    email: new FormControl('', [Validators.email, Validators.required]),
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    password: new FormControl('', [
      Validators.minLength(8),
      Validators.pattern(
        '(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}'
      ),
    ]),
  });
  */
  registerForm = this.fb.group(
    {
      email: [
        '',
        {
          validators: [Validators.required, Validators.email],
          asyncValidators: this.validatorService.emailExistsValidator(),
          // Optimize the async validator by changing updateOn property to blur or submit
          // Because the default property is change, so it will dispatch a HTTP request after every keystroke i
          updateOn: 'blur',
        },
      ],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      password: [
        '',
        [
          Validators.minLength(8),
          Validators.pattern(
            '(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}'
          ),
        ],
      ],
      confirmPassword: ['', Validators.required],
    },
    { validators: this.validatorService.passwordMatchValidator() }
  );

  errorMsg = 'Register failed...Please try again...';
  inValidRegister: boolean;

  constructor(
    private authService: AuthService,
    private router: Router,
    private fb: FormBuilder,
    private validatorService: ValidatorService
  ) {}

  ngOnInit(): void {}

  onSubmit() {
    if (this.registerForm.valid) {
      this.authService.register(this.registerForm.value).subscribe(
        (res) => {
          if (res) {
            this.router.navigate(['/login']);
          }
        },
        (err) => {
          this.inValidRegister = true;
          setTimeout(() => (this.inValidRegister = false), 6000);
          console.log(`Register failed. Error message: ${err}`);
        }
      );
    }
  }
}
