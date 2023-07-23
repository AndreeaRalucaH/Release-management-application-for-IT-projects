import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Status } from 'app/models/status';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StatusService {

  constructor(private http: HttpClient) { }

  readonly statusUrl = environment.baseUrl + "status";
  readonly statusDenUrl = environment.baseUrl + "status/search";

  createStatus(status: Status): Observable<Status>{
    return this.http.post<Status>(this.statusUrl, status);
  }

  getAllStatus(): Observable<Status[]>{
    return this.http.get<Status[]>(this.statusUrl);
  }

  getOneStatusById(id: number): Observable<Status>{
    return this.http.get<Status>(this.statusUrl + "/" + id);
  }

  getOneStatusByDen(denumire: string): Observable<Status>{
    return this.http.get<Status>(this.statusDenUrl + "/" + denumire);
  }

  updateStatus(id: number, status: Status): Observable<Status>{
    return this.http.put<Status>(this.statusUrl + "/" + id,status);
  }

  deleteStatus(id: number){
    return this.http.delete(this.statusUrl + "/" + id);
  }
}
