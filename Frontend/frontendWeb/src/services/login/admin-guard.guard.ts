import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {Observable} from 'rxjs';
import {AuthService} from "./auth/auth.service";
import {AlertService} from "./alert/alert.service";

@Injectable({
  providedIn: 'root'
})
export class AdminGuardGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router, private alert: AlertService) {
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if (!this.authService.isAdmin()) {
      this.alert.info("Necessário efetuar login!", "Login");
      this.router.navigate(['']);
      this.authService.clear();
      return false;
    }
    return true;
  }

}
