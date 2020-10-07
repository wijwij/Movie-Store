import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { MovieService } from 'src/app/core/services/movie.service';
import { PagedMovie } from 'src/app/shared/models/pagedMovie';

@Component({
  selector: 'app-movie-search',
  templateUrl: './movie-search.component.html',
  styleUrls: ['./movie-search.component.css'],
})
export class MovieSearchComponent implements OnInit {
  queryTitle: string;
  pagedMovies: PagedMovie;
  hasResult: boolean;
  isLoading = true;
  pageIndex: number;
  pageSize: number;

  constructor(
    private route: ActivatedRoute,
    private movieService: MovieService
  ) {
    this.pageSize = 20;
  }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe((params) => {
      this.queryTitle = params.get('title');
      this.getPagedResult(this.queryTitle, this.pageSize).subscribe((res) => {
        this.isLoading = false;
        if (res) {
          this.pagedMovies = res;
          this.hasResult = true;
        } else this.hasResult = false;
      });
    });
  }

  onPageChange(event: PageEvent): void {
    this.isLoading = true;
    this.getPagedResult(
      this.queryTitle,
      event.pageSize,
      event.pageIndex
    ).subscribe((res) => {
      this.isLoading = false;
      if (res) {
        this.pagedMovies = res;
        this.hasResult = true;
      } else this.hasResult = false;
    });
  }

  private getPagedResult(
    title: string,
    pageSize: number,
    pageIndex: number = 0
  ): Observable<PagedMovie> {
    return this.movieService.getMoviesByTitle(title, pageIndex + 1, pageSize);
  }
}
