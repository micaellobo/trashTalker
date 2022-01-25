import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';

const endpoint = 'https://localhost:5001/api/v1/routes/';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class RouteService {

  constructor(private http: HttpClient) {
  }

  createAutomaticRoute(route: any): Observable<any> {
    return this.http.post<any>(endpoint + "auto", JSON.stringify(route), httpOptions)
  }

  createManualRoute(route: any): Observable<any> {
    return this.http.post<any>(endpoint, JSON.stringify(route), httpOptions);
  }

  confirmeRoute(id: string): Observable<any> {
    return this.http.post<any>(`${endpoint}auto/${id}`, JSON.stringify({}), httpOptions);
  }

  cancelRoute(id: string): Observable<any> {
    return this.http.delete<any>(`${endpoint}auto/${id}`, httpOptions);
  }

  getAllRoutes(): Observable<any> {
    return this.http.get<any>(endpoint)
  }
}
