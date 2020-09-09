import { Injectable } from '@angular/core';
import {
  CanActivateChild,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  Router,
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivateChild {
  constructor(private authService: AuthService, private router: Router) {}

  canActivateChild(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> {
    // BUG. the `isAuthenticatedSubject` is a subject, so convert it to observalbe at first.
    return this.authService.isAuthenticatedSubject.asObservable().pipe(
      map((auth) => {
        if (auth) return true;
        else {
          // console.log(`The return url is ${state.url} got from RouterStateSanpshot`);

          // Set return url
          this.router.navigate(['/login'], {
            queryParams: { returnUrl: state.url },
          });
          return false;
        }
      }),
      catchError(() => {
        this.router.navigate(['/login']);
        return of(false);
      })
    );
  }
}
