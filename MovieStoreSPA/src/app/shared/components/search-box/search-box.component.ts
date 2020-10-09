import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbTypeaheadSelectItemEvent } from '@ng-bootstrap/ng-bootstrap';
import { Observable, of } from 'rxjs';
import {
  catchError,
  debounceTime,
  distinctUntilChanged,
  map,
  switchMap,
} from 'rxjs/operators';
import { MovieService } from 'src/app/core/services/movie.service';

@Component({
  selector: 'app-search-box',
  templateUrl: './search-box.component.html',
  styleUrls: ['./search-box.component.css'],
})
export class SearchBoxComponent implements OnInit {
  // public model: string;
  constructor(private movieService: MovieService, private router: Router) {}

  ngOnInit(): void {}

  search = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      switchMap((term: string) => {
        if (term.length < 2) return of([]);

        return this.movieService.getMoviesByTitle(term).pipe(
          map((res) => {
            return res?.data;
          }),
          catchError(() => of([]))
        );
      })
    );

  onSelect(event: NgbTypeaheadSelectItemEvent, query) {
    event.preventDefault();
    this.router.navigate(['/movie/details', event.item.id]).then(() => query.value="");
  }
}
