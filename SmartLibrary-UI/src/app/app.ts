import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, Router } from '@angular/router';
import { AuthService } from './services/auth.service';
import { NotificationService } from './services/notification.service';
import { AsyncPipe, NgIf, NgStyle } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, AsyncPipe, NgIf, NgStyle],
  templateUrl: './app.html'
})
export class App {
  constructor(
    public authService: AuthService, 
    public notificationService: NotificationService,
    private router: Router
  ) {}

  logout() {
    this.authService.logout();
    this.notificationService.show('Ви успішно вийшли з системи', 'success');
    this.router.navigate(['/login']); // Перекидаємо на логін
  }
}