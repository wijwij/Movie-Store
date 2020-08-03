import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { SignUp } from 'src/app/shared/models/signup';
import { User } from 'src/app/shared/models/user';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private apiService: ApiService) {}
  register(model: SignUp): Observable<User> {
    return this.apiService.create('account/register', model);
  }
}
