import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
/**
 * A service to store, get and delete token.
 * ToDo [question difference between local storage(HTML 5) and session storage(HTML 5)].
 */
export class JwtStorageService {
  getToken(): string {
    return localStorage.getItem('token');
  }

  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  destroyToken() {
    localStorage.removeItem('token');
  }
}
