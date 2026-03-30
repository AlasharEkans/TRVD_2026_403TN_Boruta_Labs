import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export interface ToastMessage {
  text: string;
  type: 'success' | 'error';
}

@Injectable({ providedIn: 'root' })
export class NotificationService {
  public toast$ = new BehaviorSubject<ToastMessage | null>(null);

  show(text: string, type: 'success' | 'error' = 'success') {
    this.toast$.next({ text, type });
    // Автоматично ховаємо через 3 секунди
    setTimeout(() => this.toast$.next(null), 3000);
  }
}