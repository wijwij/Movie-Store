import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { Movie } from 'src/app/shared/models/movie';
import { PagedMovie } from 'src/app/shared/models/pagedMovie';

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
  getMovieDetailById(movieId: number): Observable<Movie> {
    return this.apiService.getOne(`movies/details`, `${movieId}`);
  }
  getMoviesByTitle(
    title: string,
    pageIndex?: number,
    pageSize?: number
  ): Observable<PagedMovie> {
    var query = new Map<string, string>();

    query = query.set('title', title);
    if (pageIndex) query = query.set('pageIndex', `${pageIndex}`);
    if (pageSize) query = query.set('pageSize', `${pageSize}`);

    return this.apiService.getOne('movies', null, query);
  }
}
