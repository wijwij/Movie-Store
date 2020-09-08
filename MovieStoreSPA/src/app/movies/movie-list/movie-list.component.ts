import { Component, OnInit } from '@angular/core';
import { Movie } from '../../shared/models/movie';
import { MovieService } from 'src/app/core/services/movie.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css'],
})
export class MovieListComponent implements OnInit {
  movies: Movie[];
  genreId: number;
  constructor(
    private movieService: MovieService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // Get genre id from URL though using ActivatedRoute object
    this.route.paramMap.subscribe((params) => {
      // cast to number
      this.genreId = +params.get(`id`);
      // send request
      this.movieService.getMoviesByGenre(this.genreId).subscribe((movies) => {
        this.movies = movies;
        // console.log(`${this.genreId}`);
        // console.table(this.movies);
      });
    });
  }
}
