import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MovieService } from 'src/app/core/services/movie.service';
import { PagedMovie } from 'src/app/shared/models/pagedMovie';

@Component({
  selector: 'app-movie-search',
  templateUrl: './movie-search.component.html',
  styleUrls: ['./movie-search.component.css'],
})
export class MovieSearchComponent implements OnInit {
  queryTitle: string;
  movies: PagedMovie;
  hasResult: boolean;

  constructor(
    private route: ActivatedRoute,
    private movieService: MovieService
  ) {}

  ngOnInit(): void {
    this.route.queryParamMap.subscribe((params) => {
      this.queryTitle = params.get('title');
      this.movieService.getMoviesByTitle(this.queryTitle).subscribe((res) => {
        if (res) {
          this.movies = res;
          this.hasResult = true;
        } else this.hasResult = false;
      });
    });
  }
}
