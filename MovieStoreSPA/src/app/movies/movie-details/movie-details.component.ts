import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Movie } from 'src/app/shared/models/movie';
import { MovieService } from 'src/app/core/services/movie.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from 'src/app/core/services/user.service';
import { AuthService } from 'src/app/core/services/auth.service';
import { Review } from 'src/app/shared/models/review';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css'],
})
export class MovieDetailsComponent implements OnInit {
  movieId: number;
  movie: Movie;
  isAuthenticated: boolean;
  review: Review;
  isReviewedBefore: boolean;

  constructor(
    private route: ActivatedRoute,
    private movieService: MovieService,
    private userService: UserService,
    private modalService: NgbModal,
    private authService: AuthService,
    private router: Router
  ) {
    this.review = {
      reviewText: '',
      rating: 0,
      movieId: this.movieId,
      userId: 0,
    };
    // The default value of isReviewedBefore is undefined
    this.isReviewedBefore = false;
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe((r) => {
      this.movieId = +r.get('id');
      this.movieService.getMovieDetailById(this.movieId).subscribe((movie) => {
        this.movie = movie;
      });
    });

    this.authService.isAuthenticatedSubject.subscribe((auth) => {
      this.isAuthenticated = auth;
    });
  }

  toggleFavorite(movieId: number): void {
    if (!this.isAuthorizedAction()) return;
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

  purchaseMovie(movieId: number, price: number): void {
    if (!this.isAuthorizedAction()) return;
    this.userService.purchaseMovie(movieId, price).subscribe(
      () => {
        this.movie.isPurchasedByUser = !this.movie.isPurchasedByUser;
      },
      () => {
        console.log(`Failed to purchase the movie`);
      }
    );
  }

  isAuthorizedAction(): boolean {
    if (!this.isAuthenticated) {
      this.router.navigate(['/login'], {
        queryParams: { returnUrl: `movie/details/${this.movieId}` },
      });
      return false;
    }
    return true;
  }

  openReview(content) {
    if (!this.isAuthorizedAction()) return;

    // Fetch the review if it exists
    this.userService.getReview(this.movieId).subscribe((res) => {
      if (res) {
        this.review = { ...res };
        this.isReviewedBefore = true;
      }

      this.modalService.open(content, {
        size: 'lg',
        centered: true,
        scrollable: true,
      });
    });
  }

  submitReview() {
    if (this.isReviewedBefore) {
      // console.log(`updating the review....`);
      this.userService
        .updateReview(this.movieId, this.review.reviewText, this.review.rating)
        .subscribe((res) => {
          this.review = { ...res };
          this.isReviewedBefore = true;
        });
    } else {
      // console.log(`creating the review....`);
      this.userService
        .createReview(this.movieId, this.review.reviewText, this.review.rating)
        .subscribe((res) => {
          this.review = { ...res };
          this.isReviewedBefore = true;
        });
    }
  }
}
