import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ObservableLike } from 'rxjs';
import { employee } from 'src/models/employee';

const endpoint = 'https://localhost:5001/api/v1/user/'

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})

export class EmployeeService {

  constructor(private http:HttpClient) { }

  showEmployees():Observable<any[]>{
    return this.http.get<any[]>(endpoint);
  }

  disableEmployee(id:String):Observable<any>{
    return this.http.delete<any[]>(endpoint+id,httpOptions);
  }

  addEmployee(emp:employee):Observable<any>{
    return this.http.post<employee>(endpoint,JSON.stringify(emp),httpOptions);
  }

  updateEmployee(id:String,emp:employee):Observable<any>{
      return this.http.put<employee>(endpoint+id,JSON.stringify(emp),httpOptions);
  }
}
