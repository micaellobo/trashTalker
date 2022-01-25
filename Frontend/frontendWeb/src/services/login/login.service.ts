import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { RequestLogin } from 'src/models/requestLogin';
import { ResponseLogin } from 'src/models/responseLogin';
import { AuthService } from './auth/auth.service';

const endpoint = 'https://localhost:5001/api/v1/login/'

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private httpClient:HttpClient, private authService:AuthService) { }

  authenticate(requestLogin: RequestLogin): Observable<ResponseLogin> {
      return this.httpClient.post<ResponseLogin>(
        endpoint, 
        requestLogin
      );
  }

}
