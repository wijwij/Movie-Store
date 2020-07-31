import { Movie } from './../../shared/models/movie';
import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class MovieService {
  constructor(private apiService: ApiService) {}
  getTopMovies(): Observable<Movie[]> {
    return this.apiService.getAll('movies/toprevenue');
  }
  getMoviesByGenre(genreId: number): Observable<Movie[]> {
    return this.apiService.getAll(`genres/movies/${genreId}`);
  }
}
