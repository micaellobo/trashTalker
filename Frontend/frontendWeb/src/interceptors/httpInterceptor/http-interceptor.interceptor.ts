import {Injectable} from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse
} from '@angular/common/http';
import {map, Observable} from 'rxjs';
import {JwtHelperService} from '@auth0/angular-jwt';
import {AuthService} from 'src/services/login/auth/auth.service';
import {AlertService} from 'src/services/login/alert/alert.service';
import {Router} from '@angular/router';

@Injectable()
export class HttpInterceptorInterceptor implements HttpInterceptor {

  constructor(private jwtHelper: JwtHelperService, private authService: AuthService, private alertService: AlertService, private router: Router) {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    //Caso seja o login, lidar com o response para definir o header
    if (request.url === `https://localhost:5001/api/v1/login/`) {
      return next.handle(request).pipe(map((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          const token = event.headers.get("Authorization");
          if (token) {
            let {unique_name: username, role} = this.jwtHelper.decodeToken(token);
            localStorage.setItem('user', JSON.stringify({username, role, token}));
          }
        }
        return event;
      }))
    }

    if (this.authService.isAdminOrManager()) {
      request = request.clone({
        setHeaders: {
          'Authorization': `Bearer ${this.authService?.getCurrentUser().token}`,
        }
      });
    } else {
      this.alertService.info("A sua sessão expirou, por favor faça login novamente!", "Login");
      this.router.navigate(['']);
      this.authService.clear();
    }
    return next.handle(request);
  }
}
