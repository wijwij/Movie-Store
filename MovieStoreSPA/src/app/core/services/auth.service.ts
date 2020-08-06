import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { SignUp } from 'src/app/shared/models/signup';
import { User } from 'src/app/shared/models/user';
import { Observable } from 'rxjs';
import { Login } from 'src/app/shared/models/login';
import { JwtStorageService } from 'src/app/core/services/jwt-storage.service';
import { map } from 'rxjs/operators';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private apiService: ApiService,
    private jwtStorage: JwtStorageService
  ) {}
  register(model: SignUp): Observable<User> {
    return this.apiService.create('account/register', model);
  }

  login(model: Login): Observable<boolean> {
    return this.apiService.create('account/login', model).pipe(
      map((res) => {
        if (res) {
          // call jwt-storage to store the token in the local storage
          this.jwtStorage.saveToken(res.token);
          // decode the token and populate user info
          return true;
        } else return false;
      })
    );
  }
}
