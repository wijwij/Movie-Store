import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { SignUp } from 'src/app/shared/models/signup';
import { User } from 'src/app/shared/models/user';
import { Observable } from 'rxjs';
import { Login } from 'src/app/shared/models/login';
import { JwtStorageService } from 'src/app/core/services/jwt-storage.service';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
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
          // call jwt-storage service to store the token (json body inside the response) in the local storage
          this.jwtStorage.saveToken(res.token);
          // decode the token and populate user info
          this.populateUserInfo();
          return true;
        } else return false;
      })
    );
  }

  populateUserInfo() {
    if (this.jwtStorage.getToken()) {
      const encodedToken = this.jwtStorage.getToken();
      const helperService = new JwtHelperService();
      const decodedToken = helperService.decodeToken(encodedToken);
    }
  }
}
