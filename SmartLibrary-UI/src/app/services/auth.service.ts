import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap, BehaviorSubject } from 'rxjs';
import { LoginResponse, UserPayload } from '../models/auth.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'http://localhost:5222/api/auth'; 
  
  
  public isLoggedIn$ = new BehaviorSubject<boolean>(this.hasToken());

  constructor(private http: HttpClient) {}

  register(userData: any) {
    
    return this.http.post(`${this.apiUrl}/register`, userData, { responseType: 'text' }); 
  }

  login(credentials: any) {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, credentials).pipe(
      tap(res => {
        localStorage.setItem('token', res.token);
        this.isLoggedIn$.next(true); 
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.isLoggedIn$.next(false);
  }

  hasToken(): boolean {
    if (typeof window === 'undefined') return false;
    return !!localStorage.getItem('token');
  }

  // Декодуємо токен, щоб дістати роль (Admin/Reader)
  getUserRole(): string | null {
    if (typeof window === 'undefined') return null;
    const token = localStorage.getItem('token');
    if (!token) return null;
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || payload.role;
    } catch (e) {
      return null;
    }
  }
}