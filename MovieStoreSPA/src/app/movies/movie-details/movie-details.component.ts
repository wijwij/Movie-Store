import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Movie } from 'src/app/shared/models/movie';
import { MovieService } from 'src/app/core/services/movie.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css'],
})
export class MovieDetailsComponent implements OnInit {
  movieId: number;
  movie: Movie;

  constructor(
    private route: ActivatedRoute,
    private movieService: MovieService,
    private userService: UserService,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((r) => {
      this.movieId = +r.get('id');
      this.movieService.getMovieDetailById(this.movieId).subscribe((movie) => {
        this.movie = movie;
      });
    });
  }

  toggleFavorite(movieId: number): void {
    if (!this.movie.isFavoriteByUser) {
      // ToDo [question? Whether to pass user id along with the model]
      var model = { movieId: movieId, userId: 0 };

      this.userService.favoriteMovie(model).subscribe({
        next: () => {
          this.movie.isFavoriteByUser = !this.movie.isFavoriteByUser;
          // console.log(this.movie);
        },
        error: () => {
          console.log(`Failed to favorite the movie...`);
        },
      });
    } else {
      this.userService.removeFavorite(movieId).subscribe({
        next: () => {
          this.movie.isFavoriteByUser = !this.movie.isFavoriteByUser;
          // console.log(this.movie);
        },
        error: () => {
          console.log(`Failed to unlike the movie...`);
        },
      });
    }
  }

  leaveReview(content) {
    this.modalService.open(content, { size: 'lg' });
  }
}
