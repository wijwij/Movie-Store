import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
// import 3-rd parth libraries
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { GenresComponent } from './genres/genres.component';
import { HeaderComponent } from './core/layout/header.component';
import { LoginComponent } from './auth/login/login.component';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { MovieDetailsComponent } from './movies/movie-details/movie-details.component';
import { MovieCardComponent } from './shared/components/movie-card/movie-card.component';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {
  NgbCarouselModule,
  NgbCollapseModule,
  NgbDropdownModule,
  NgbModalModule,
  NgbPaginationModule,
  NgbTabsetModule,
  NgbAlertModule,
} from '@ng-bootstrap/ng-bootstrap';
import { MovieListComponent } from './movies/movie-list/movie-list.component';
import { FavoritesComponent } from './account/favorites/favorites.component';
import { PurchasesComponent } from './account/purchases/purchases.component';
import { ProfileComponent } from './account/profile/profile.component';

// Decorators are like attribute in c#, @NgModule includes the metadata object
@NgModule({
  // Components. A component in angular should be declared inside at least one module. Each component has a view
  declarations: [
    AppComponent,
    HomeComponent,
    GenresComponent,
    HeaderComponent,
    LoginComponent,
    SignUpComponent,
    MovieDetailsComponent,
    MovieCardComponent,
    MovieListComponent,
    FavoritesComponent,
    PurchasesComponent,
    ProfileComponent,
  ],
  // Other NgModule you are using
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    NgbCarouselModule,
    NgbCollapseModule,
    NgbDropdownModule,
    NgbModalModule,
    NgbPaginationModule,
    NgbTabsetModule,
    NgbAlertModule,
    ReactiveFormsModule,
    FormsModule,
  ],
  // Dependency injection
  providers: [],
  // We can select which component needs to be started when app starts.
  bootstrap: [AppComponent],
})
export class AppModule {}
