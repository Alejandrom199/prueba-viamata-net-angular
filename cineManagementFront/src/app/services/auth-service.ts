import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isLoggedIn = false;

  constructor(private router: Router) {}

  fakeLogin(username: string, password: string): void {
    this.isLoggedIn = true;
    localStorage.setItem('fakeAuth', 'true');
    console.log('Login falso exitoso con:', username);
  }

  logout(): void {
    this.isLoggedIn = false;
    localStorage.removeItem('fakeAuth');
    localStorage.clear();
    this.router.navigate(['/login']);
  }

  checkAuth(): boolean {
    return this.isLoggedIn || localStorage.getItem('fakeAuth') === 'true';
  }
}
