import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Impulse } from 'app/models/impulse';
import { ImpulseCreate } from 'app/models/impulse-create';
import { ImpulseResult } from 'app/models/impulse-result';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImpulseService {

  constructor(private http: HttpClient) { }

  readonly impulseUrl = environment.baseUrl + "impulse";
  readonly impulseCreateUrl = environment.baseUrl + "impulsecreatechg/create";

  createImpulse(impulse: Impulse): Observable<Impulse>{
    return this.http.post<Impulse>(this.impulseUrl, impulse);
  }

  getAllImpulse(): Observable<Impulse[]>{
    return this.http.get<Impulse[]>(this.impulseUrl);
  }

  getOneImpulseById(id: number): Observable<Impulse>{
    return this.http.get<Impulse>(this.impulseUrl + "/" + id);
  }

  updateImpulse(id: number, impulse: Impulse): Observable<Impulse>{
    return this.http.put<Impulse>(this.impulseUrl + "/" + id,impulse);
  }

  deleteImpulse(id: number){
    return this.http.delete(this.impulseUrl + "/" + id);
  }

  createImpulseChg(impulse: ImpulseCreate): Observable<ImpulseResult> {
    return this.http.post<ImpulseResult>(this.impulseCreateUrl, impulse);
  }
}
