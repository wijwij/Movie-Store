import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Movie } from 'src/app/shared/models/movie';
import { Observable } from 'rxjs';
import { User } from 'src/app/shared/models/user';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  userId: number;
  user: User;

  constructor(private apiService: ApiService) {}

  getFavoriteMovies(): Observable<Movie[]> {
    return this.apiService.getAll(`user/favorites`);
  }

  getPurchasedMovies(): Observable<Movie[]> {
    return this.apiService.getAll(`user/purchases`);
  }

  getReviewedMovies(): Observable<Movie[]> {
    return this.apiService.getAll(`user/reviews`);
  }

  getProfile(): Observable<User> {
    return this.apiService.getOne(`user/profile`);
  }

  checkEmailExist(email: string): Observable<boolean> {
    const query = new Map([['email', email]]);
    return this.apiService.getOne(`account/exist`, null, query).pipe(
      map((res) => {
        // console.log(
        //   `checking if ${email} has been taken.... EmailExist: ${res.emailExist}`
        // );
        return res.emailExist as boolean;
      })
    );
  }
}
