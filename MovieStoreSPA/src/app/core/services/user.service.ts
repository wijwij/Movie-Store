import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Movie } from 'src/app/shared/models/movie';
import { Favorite } from 'src/app/shared/models/favorite';
import { Purchase } from 'src/app/shared/models/purchase';
import { Review } from 'src/app/shared/models/review';
import { User } from 'src/app/shared/models/user';

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

  favoriteMovie(model: Favorite): Observable<boolean> {
    return this.apiService.create('user/favorite', model);
  }

  removeFavorite(movieId: number): Observable<boolean> {
    return this.apiService.delete('user/unfavorite', movieId).pipe(
      map(() => {
        return true;
      })
    );
  }

  purchaseMovie(movieId: number, price: number): Observable<Purchase> {
    return this.apiService.create('user/purchase', {
      movieId: movieId,
      price: price,
    });
  }

  getReview(movieId: number): Observable<Review> {
    return this.apiService.getOne('user/review', `${movieId}`, null);
  }

  createReview(
    movieId: number,
    reviewText: string,
    rating: number
  ): Observable<Review> {
    return this.apiService.create('user/leave/review', {
      MovieId: movieId,
      ReviewText: reviewText,
      Rating: rating,
    });
  }

  updateReview(
    movieId: number,
    reviewText: string,
    rating: number
  ): Observable<Review> {
    return this.apiService.update('user/update/review', {
      MovieId: movieId,
      ReviewText: reviewText,
      Rating: rating,
    });
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
