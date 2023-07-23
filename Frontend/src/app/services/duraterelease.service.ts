import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Duraterelease } from 'app/models/duraterelease';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DuratereleaseService {

  constructor(private http: HttpClient) { }

  readonly durateRelUrl = environment.baseUrl + "duraterelease";

  createDurata(durata: Duraterelease): Observable<Duraterelease>{
    return this.http.post<Duraterelease>(this.durateRelUrl, durata);
  }

  getAllApps(): Observable<Duraterelease[]>{
    return this.http.get<Duraterelease[]>(this.durateRelUrl);
  }

  getOneDurataById(id: number): Observable<Duraterelease>{
    return this.http.get<Duraterelease>(this.durateRelUrl + "/" + id);
  }

  updateDurata(id: number, durata: Duraterelease): Observable<Duraterelease>{
    return this.http.put<Duraterelease>(this.durateRelUrl + "/" + id,durata);
  }

  deleteDurata(id: number){
    return this.http.delete(this.durateRelUrl + "/" + id);
  }
}
