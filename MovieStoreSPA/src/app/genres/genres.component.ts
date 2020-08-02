import { Component, OnInit } from '@angular/core';
import { GenreService } from './../core/services/genre.service';
import { Genre } from 'src/app/shared/models/genre';

@Component({
  selector: 'app-genres',
  templateUrl: './genres.component.html',
  styleUrls: ['./genres.component.css'],
})
export class GenresComponent implements OnInit {
  genres: Genre[];
  constructor(private genreService: GenreService) {}

  /**
   * Component Life cycle Hooks
   */
  ngOnInit(): void {
    // initialize the data fetched from web api, etc
    this.genreService.getAllGenres().subscribe((gs) => {
      this.genres = gs;
      // console.log(this.genres);
    });
  }
}
