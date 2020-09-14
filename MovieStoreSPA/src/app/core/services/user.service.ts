import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Movie } from 'src/app/shared/models/movie';
import { Favorite } from 'src/app/shared/models/favorite';
import { Purchase } from 'src/app/shared/models/purchase';
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
