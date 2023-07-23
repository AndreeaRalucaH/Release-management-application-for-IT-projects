import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Aplicatii } from 'app/models/aplicatii';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AplicatiiService {

  constructor(private http: HttpClient) { }

  readonly aplicatiiUrl = environment.baseUrl + "aplicatii";
  readonly aplicatiiDenUrl = environment.baseUrl + "aplicatii/search";

  createApp(app: Aplicatii): Observable<Aplicatii>{
    return this.http.post<Aplicatii>(this.aplicatiiUrl, app);
  }

  getAllApps(): Observable<Aplicatii[]>{
    return this.http.get<Aplicatii[]>(this.aplicatiiUrl);
  }

  getOneAppById(id: number): Observable<Aplicatii>{
    return this.http.get<Aplicatii>(this.aplicatiiUrl + "/" + id);
  }

  getOneAppByDen(denumire: string): Observable<Aplicatii>{
    return this.http.get<Aplicatii>(this.aplicatiiDenUrl + "/" + denumire);
  }

  updateApp(id: number, app: Aplicatii): Observable<Aplicatii>{
    return this.http.put<Aplicatii>(this.aplicatiiUrl + "/" + id,app);
  }

  deleteApp(id: number){
    return this.http.delete(this.aplicatiiUrl + "/" + id);
  }
}
