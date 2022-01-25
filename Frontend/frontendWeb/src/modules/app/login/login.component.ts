import {nullSafeIsEquivalent} from '@angular/compiler/src/output/output_ast';
import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {RequestLogin} from 'src/models/requestLogin';
import {AlertService} from 'src/services/login/alert/alert.service';
import {AuthService} from 'src/services/login/auth/auth.service';
import {LoginService} from 'src/services/login/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public requestLogin!: RequestLogin;
  private messageError!: string;

  constructor(
    private loginService: LoginService,
    private alertService: AlertService,
    private authService: AuthService,
    private router: Router,
  ) {
  }

  ngOnInit(): void {
    this.requestLogin = new RequestLogin();
  }

  doLogin(): void {
    this.loginService.authenticate(this.requestLogin).subscribe(
      (data) => {
        this.router.navigate(['home']);
      },
      (httpError) => {
        try {
          this.alertService.error(Object.values(httpError.error.errors).join(" and "), "");
        } catch (e) {
          if (typeof httpError.error === 'string' || httpError.error instanceof String) {
            this.alertService.error(httpError.error, "");
          } else {
            this.alertService.error(Object.values(httpError.error).join(" and "), "");
          }
        }
      })
  }

}
