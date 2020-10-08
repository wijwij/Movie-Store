import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import {
  catchError,
  debounceTime,
  distinctUntilChanged,
  map,
  switchMap,
} from 'rxjs/operators';
import { MovieService } from 'src/app/core/services/movie.service';
import { Movie } from '../../models/movie';

@Component({
  selector: 'app-search-box',
  templateUrl: './search-box.component.html',
  styleUrls: ['./search-box.component.css'],
})
export class SearchBoxComponent implements OnInit {
  public model: any;
  constructor(private movieService: MovieService) {}

  ngOnInit(): void {}

  search = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      switchMap((term) => {
        if (term.length < 2) return of([]);
        console.log(`service is going to trigger: ${term}`);
        return this.movieService.getMoviesByTitle(`${term}`).pipe(
          map((res) => {
            console.log(`hit the service and get result back: ${res?.data}`);
            console.table(res.data);
            // Bug return an Observable of array for Typeahead to display the result
            return res?.data.forEach((movie) => movie.title);
          }),
          catchError(() => of([]))
        );
      })
    );
}
