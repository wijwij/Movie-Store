import { Component, OnInit, Input } from '@angular/core';
import { Movie } from './../../../shared/models/movie';

@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.css'],
})
export class MovieCardComponent implements OnInit {
  /**
   * ToDo [review - two way binding]
   * two ways to share data between components
   * 1. sending data from parent to child component @input decorator
   * 2. emitting data from child to parent component @output deorator
   */
  @Input() movie: Movie;
  constructor() {}

  ngOnInit(): void {}
}
