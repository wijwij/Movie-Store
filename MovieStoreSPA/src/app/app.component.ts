import { Component } from '@angular/core';
import { AuthService } from './core/services/auth.service';
import { RealtimeNotificationService } from './core/realtime/realtime-notification.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'MovieStoreSPA';
  notification: string;

  constructor(private authService: AuthService, private notificationService: RealtimeNotificationService) {}

  ngOnInit() {
    this.authService.publishUserInfo();
    this.notificationService.message$.subscribe((msg) => {
      this.notification = msg;
    })
  }

  public close() {
    this.notification = null;
  }
}
