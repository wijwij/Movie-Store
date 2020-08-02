import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Movie } from 'src/app/shared/models/movie';
import { MovieService } from 'src/app/core/services/movie.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

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
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((r) => {
      this.movieId = +r.get('id');
      this.movieService.getMovieDetailById(this.movieId).subscribe((movie) => {
        this.movie = movie;
        console.log(this.movie);
      });
    });
  }

  leaveReview(content) {
    this.modalService.open(content, { size: 'lg' });
  }
}
