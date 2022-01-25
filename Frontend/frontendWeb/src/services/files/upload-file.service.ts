import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const endpoint = 'https://localhost:5001/api/v1/images/'

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':'application/json'
  })
};


@Injectable({
  providedIn: 'root'
})
export class UploadFileService {

  constructor(private http:HttpClient) { }


  uploadFile(file: File,id:String): Observable<any> {
    const formData = new FormData();
    formData.append("file", file);
    var res = endpoint+id;
    return this.http.post<any>(res, formData);
  }

}
