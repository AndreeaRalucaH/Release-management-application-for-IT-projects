import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Detaliirelease } from 'app/models/detaliirelease';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DetaliireleaseService {

  constructor(private http: HttpClient) { }

  readonly detaliiRelUrl = environment.baseUrl + "detaliirelease";
  readonly detaliiRelDenUrl = environment.baseUrl + "detaliirelease/search";
  readonly detaliiRelMatchDatesUrl = environment.baseUrl + "detaliirelease/searchDate";

  getAllDetaliiRel(): Observable<Detaliirelease[]>{
    return this.http.get<Detaliirelease[]>(this.detaliiRelUrl);
  }

  getOneDetaliiRelById(id: number): Observable<Detaliirelease>{
    return this.http.get<Detaliirelease>(this.detaliiRelUrl + "/" + id);
  }

  getDetaliiRelByDen(denumire: string): Observable<Detaliirelease[]>{
    return this.http.get<Detaliirelease[]>(this.detaliiRelDenUrl + "/" + denumire);
  }

  getMatchDate(data: string): Observable<Detaliirelease[]>{
    return this.http.get<Detaliirelease[]>(this.detaliiRelMatchDatesUrl + "/" + data);
  }
}
