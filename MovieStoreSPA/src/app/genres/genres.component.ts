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
   * Page Life cycle Hooks
   */
  ngOnInit(): void {
    // initialize any data, call the api etc
    this.genreService.getAllGenres().subscribe((gs) => {
      console.log('genre init method');
      this.genres = gs;
      console.log(this.genres);
    });
  }
}
