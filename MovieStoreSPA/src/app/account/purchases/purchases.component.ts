import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/core/services/user.service';
import { Movie } from 'src/app/shared/models/movie';

@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.css'],
})
export class PurchasesComponent implements OnInit {
  purchasedMovies: Movie[];
  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService
      .getPurchasedMovies()
      .subscribe((movies) => (this.purchasedMovies = movies));
  }
}
