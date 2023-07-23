import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Medii } from 'app/models/medii';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MediiService {
  
  constructor(private http: HttpClient) { }

  readonly mediiUrl = environment.baseUrl + "medii";
  readonly mediiDenUrl = environment.baseUrl + "medii/search";

  createMediu(mediu: Medii): Observable<Medii>{
    return this.http.post<Medii>(this.mediiUrl, mediu);
  }

  getAllMedii(): Observable<Medii[]>{
    return this.http.get<Medii[]>(this.mediiUrl);
  }

  getOneMediuById(id: number): Observable<Medii>{
    return this.http.get<Medii>(this.mediiUrl + "/" + id);
  }

  getOneMediuByDen(denumire: string): Observable<Medii>{
    return this.http.get<Medii>(this.mediiDenUrl + "/" + denumire);
  }

  updateMediu(id: number, mediu: Medii): Observable<Medii>{
    return this.http.put<Medii>(this.mediiUrl + "/" + id,mediu);
  }

  deleteMediu(id: number){
    return this.http.delete(this.mediiUrl + "/" + id);
  }
}
