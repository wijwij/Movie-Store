import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './auth/login/login.component';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { MovieDetailsComponent } from './movies/movie-details/movie-details.component';
import { MovieListComponent } from './movies/movie-list/movie-list.component';
import { FavoritesComponent } from './account/favorites/favorites.component';
import { PurchasesComponent } from './account/purchases/purchases.component';
import { ProfileComponent } from './account/profile/profile.component';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'signup',
    component: SignUpComponent,
  },
  {
    path: 'movie/details/:id',
    component: MovieDetailsComponent,
  },
  {
    path: 'movies/genres/:id',
    component: MovieListComponent,
  },
  {
    path: 'user',
    canActivateChild: [AuthGuard],
    children: [
      {
        path: 'favorite',
        component: FavoritesComponent,
      },
      {
        path: 'purchase',
        component: PurchasesComponent,
      },
      {
        path: 'profile',
        component: ProfileComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
