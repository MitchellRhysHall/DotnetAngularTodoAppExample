import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient) {
    this.isAuthenticated();
  }

  register(email: string, password: string) {
    return this.http.post('/api/user/register', {email, password});
  }

  login(email: string, password: string) {
    return this.http.post('/api/user/login', {email, password});
  }

  logout() {
    return this.http.post('/api/user/logout', {}, { withCredentials: true })
  }

  isAuthenticated() {
    return this.http.get('/api/user/status')
  }
}
