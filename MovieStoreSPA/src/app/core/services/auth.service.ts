import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { SignUp } from 'src/app/shared/models/signup';
import { User } from 'src/app/shared/models/user';
import { Observable, BehaviorSubject } from 'rxjs';
import { Login } from 'src/app/shared/models/login';
import { JwtStorageService } from 'src/app/core/services/jwt-storage.service';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  // A BehaviorSubject that broadcasts the current user
  public currentUserSubject = new BehaviorSubject<User>({} as User);
  // A BehaviorSubject that broadcasts if the user is authenticated
  public isAuthenticatedSubject = new BehaviorSubject<boolean>(false);
  // private user: User;

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
        // call jwt-storage service to store the token (json body inside the response) in the local storage
        this.jwtStorage.saveToken(res.token);
        // decode the token
        // this.user = this.decodeToken(res.token);
        // publish user info
        this.publishUserInfo();
        return true;
      })
      // Migrate catch failed http response error to the api service.
    );
  }

  logout() {
    this.jwtStorage.destroyToken();
    // Publish new current user state
    this.currentUserSubject.next({} as User);
    // Publish new auth status to false
    this.isAuthenticatedSubject.next(false);
  }

  decodeToken(encodedToken: string): User {
    // decode the token
    const helperService = new JwtHelperService();
    if (!encodedToken || helperService.isTokenExpired(encodedToken)) {
      this.logout();
      return null;
    }
    const decodedToken = helperService.decodeToken(encodedToken);
    return decodedToken;
  }

  publishUserInfo() {
    // Publish current user's state
    if (this.jwtStorage.getToken()) {
      const token = this.jwtStorage.getToken();
      const decodedToken = this.decodeToken(token);

      this.currentUserSubject.next(decodedToken);
      this.isAuthenticatedSubject.next(true);
    }
  }

  getCurrentUser(): User {
    return this.currentUserSubject.value;
  }
}
