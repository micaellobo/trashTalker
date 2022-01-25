import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {container} from 'src/models/container';
import {recycleBin} from 'src/models/recycleBin';

const endpoint = 'https://localhost:5001/api/v1/recyclebin/'
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class RecyclerBinService {
  constructor(private http: HttpClient) {
  }

  showRecycleBins(): Observable<recycleBin[]> {
    return this.http.get<recycleBin[]>(endpoint + 'all');
  }

  showRecycleBin(id: String): Observable<any> {
    return this.http.get<any>(endpoint + id);
  }

  disableRecycleBin(id: String): Observable<any> {
    return this.http.delete<any[]>(endpoint + id, httpOptions);
  }

  addRecycleBin(rb: recycleBin): Observable<any> {
    return this.http.post<recycleBin>(endpoint, JSON.stringify(rb), httpOptions);
  }

  updateRecycleBin(rb: recycleBin): Observable<any> {
    return this.http.put<recycleBin>(endpoint, JSON.stringify(rb), httpOptions);
  }

  activeRecBin(id: string): Observable<any> {
    console.log(id)
    return this.http.put<recycleBin>(`${endpoint}active/${id}`, JSON.stringify({}), httpOptions);
  }
}
