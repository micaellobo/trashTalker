import {Injectable} from '@angular/core';
import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private user: any;

  constructor(private jwtHelper: JwtHelperService) {
    this.getCurrentUser();
  }

  clear(): void {
    localStorage.removeItem('user');
  }

  isAuthenticated(): boolean {
    this.getCurrentUser();
    return this.user?.token != null;
  }

  getCurrentUser(): any {
    this.user = JSON.parse(localStorage.getItem('user') || '{}');
    return this.user;
  }

  isSessionExpired(): boolean {
    this.getCurrentUser();
    return this.isAuthenticated() && this.jwtHelper.isTokenExpired(this.user?.token)
  }

  isManager(): boolean {
    this.getCurrentUser();
    return this.user?.role === "MANAGER"
  }


  isAdmin(): boolean {
    this.getCurrentUser();
    return this.user?.role === "ADMIN"
  }

  isSessionValid(): boolean {
    return this.isAuthenticated() && this.isManager() && !this.isSessionExpired()
  }

  isAdminOrManager(): boolean {
    return (this.isAuthenticated() && !this.isSessionExpired()) && (this.isAdmin() || this.isManager())
  }
}
