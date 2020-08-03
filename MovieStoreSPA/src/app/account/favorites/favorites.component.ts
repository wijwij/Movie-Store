import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { Movie } from 'src/app/shared/models/movie';
@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.css'],
})
export class FavoritesComponent implements OnInit {
  favoriteMovies: Movie[];
  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService
      .getFavoriteMovies()
      .subscribe((movies) => (this.favoriteMovies = movies));
  }
}
