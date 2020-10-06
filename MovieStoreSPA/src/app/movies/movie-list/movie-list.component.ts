import { Component, OnInit } from '@angular/core';
import { Movie } from '../../shared/models/movie';
import { MovieService } from 'src/app/core/services/movie.service';
import { ActivatedRoute } from '@angular/router';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css'],
})
export class MovieListComponent implements OnInit {
  movies: Movie[];
  genreId: number;
  slicedMovies: Movie[];
  pageSize: number;

  constructor(
    private movieService: MovieService,
    private route: ActivatedRoute
  ) {
    this.pageSize = 10;
  }

  ngOnInit(): void {
    // Get genre id from URL though using ActivatedRoute object
    this.route.paramMap.subscribe((params) => {
      // cast to number
      this.genreId = +params.get(`id`);
      // send request
      this.movieService.getMoviesByGenre(this.genreId).subscribe((movies) => {
        this.movies = movies;
        this.onPageChange({
          pageIndex: 0,
          pageSize: this.pageSize,
          length: this.movies?.length,
        });
      });
    });
  }

  onPageChange(event: PageEvent): void {
    // console.log(event);
    this.slicedMovies = this.movies.slice(
      event.pageIndex * event.pageSize,
      (event.pageIndex + 1) * event.pageSize
    );
  }
}
