import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const endpoint = 'https://localhost:5001/api/v1/picking/'

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class PickingService {

  constructor(private http:HttpClient) { }

  showPickings():Observable<any[]>{
    return this.http.get<any[]>(endpoint);
  }
}
