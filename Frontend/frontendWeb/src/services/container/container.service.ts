import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { container } from 'src/models/container';

const endpoint = 'https://localhost:5001/api/v1/container/'
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class ContainerService {

  constructor(private http:HttpClient) { }

  showContainers():Observable<any[]>{
    return this.http.get<container[]>(endpoint);
  }

  getContainersOnAlert():Observable<container[]>{
    return this.http.get<container[]>(endpoint+"containersOnAlert");
  }
}
