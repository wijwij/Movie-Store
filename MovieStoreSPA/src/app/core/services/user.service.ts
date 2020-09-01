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
    // hard-coded user id, later will be replaced by getting authentication from JWT
    this.userId = 1889;
    return this.apiService.getAll(`user/favorites/${this.userId}`);
  }

  getPurchasedMovies(): Observable<Movie[]> {
    this.userId = 1889;
    return this.apiService.getAll(`user/purchases/${this.userId}`);
  }

  getReviewedMovies(): Observable<Movie[]> {
    this.userId = 1889;
    return this.apiService.getAll(`user/reviews/${this.userId}`);
  }

  getProfile(): Observable<User> {
    this.userId = 1889;
    return this.apiService.getOne(`user/profile/${this.userId}`);
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
