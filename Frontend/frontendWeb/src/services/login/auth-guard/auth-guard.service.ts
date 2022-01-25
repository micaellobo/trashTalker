import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Observable} from 'rxjs';
import {AlertService} from '../alert/alert.service';
import {AuthService} from '../auth/auth.service';

@Injectable({
  providedIn: 'root'
})

export class AuthGuardService implements CanActivate {

  constructor(private authService: AuthService, private router: Router, private alert: AlertService) {
  }

  canActivate(): boolean {
    if (this.authService.isAdmin()) {
      this.router.navigate(["admin"])
      return true;
    }

    if (!this.authService.isAuthenticated()) {
      this.alert.info("Necessário efetuar login!", "Login");
      this.router.navigate(['']);
      this.authService.clear();
      return false;
    }

    if (this.authService.isSessionExpired()) {
      this.alert.info("A sua sessão expirou, por favor faça login novamente!", "Login");
      this.router.navigate(['']);
      this.authService.clear();
      return false;
    }

    if (!this.authService.isManager()) {
      this.alert.info("Não tem permissão para aceder!", "Login");
      this.router.navigate(['']);
      this.authService.clear();
      return false;
    }

    return true;
  }

  canCreateManager(): boolean {
    if (!this.authService.isAdmin()) {
      this.alert.info("Não tem permissão para criar gestor!", "Admin");
      return false;
    }
    return true;
  }


}
