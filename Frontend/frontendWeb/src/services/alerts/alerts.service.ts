import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { alert } from 'src/models/alert';

const endpoint = 'https://localhost:5001/api/v1/alert/'
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class AlertsService {
  constructor(private http:HttpClient) { }

  showAlerts():Observable<any[]>{
    return this.http.get<any[]>(endpoint);
  }

  resolveAlert(id:String):Observable<any[]>{
    return this.http.delete<any[]>(endpoint+id,httpOptions);
  }
}
