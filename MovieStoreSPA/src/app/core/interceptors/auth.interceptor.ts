import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { JwtStorageService } from '../services/jwt-storage.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private jwtService: JwtStorageService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    // Fetch the token
    const token = this.jwtService.getToken();

    if (token) {
      // Make a copy of the request
      const authReq = request.clone({
        // Set the header: `authorization: Bearer <token>`
        setHeaders: {
          'Content-Type': 'application/json',
          Accept: 'application/json',
          Authorization: `Bearer ${token}`,
        },
      });
      next.handle(authReq);
    }

    return next.handle(request);
  }
}
