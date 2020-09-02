import { Injectable } from '@angular/core';
import {
  AbstractControl,
  ValidatorFn,
  ValidationErrors,
  FormGroup,
} from '@angular/forms';
import { Observable } from 'rxjs';
import { UserService } from './user.service';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class ValidatorService {
  constructor(private userService: UserService) {}

  /**
   * Customize async validator function to check if the email has been taken.
   * That function takes an Angular control object and returns either null if the control value is valid or a validation error object.
   */
  emailExistsValidator(): ValidatorFn {
    // console.log('ping from email exists validation....');
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      return this.userService
        .checkEmailExist(control.value)
        .pipe(map((emailExist) => (emailExist ? { emailExist: true } : null)));
    };
  }

  /**
   * Customize sync cross-field validation function to check if password and confirmed password are equal.
   */
  passwordMatchValidator(): ValidatorFn {
    // console.log('ping from password matching validation...');
    return (control: FormGroup): ValidationErrors | null => {
      const password = control.get('password').value;
      const confirmPassword = control.get('confirmPassword').value;
      return password === confirmPassword ? null : { unmatched: true };
    };
  }
}
