import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/shared/models/user';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  public isMenuCollapsed = true;
  currentUser: User;
  isAuthenticated: boolean;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    // Subscribe to the currentUserSubject in the auth service
    this.authService.isAuthenticatedSubject.subscribe((auth) => {
      this.isAuthenticated = auth;
      if (this.isAuthenticated) {
        this.currentUser = this.authService.getCurrentUser();
      }
    });
  }

  logout() {
    this.authService.logout();
  }
}
