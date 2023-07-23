import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Utilizatori } from 'app/models/utilizatori';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UtilizatoriService {

  constructor(private http: HttpClient) { }

  readonly userUrl = environment.baseUrl + "utilizatori";
  readonly userDenUrl = environment.baseUrl + "utilizatori/search";

  createUser(user: Utilizatori): Observable<Utilizatori>{
    return this.http.post<Utilizatori>(this.userUrl, user);
  }

  getAllUsers(): Observable<Utilizatori[]>{
    return this.http.get<Utilizatori[]>(this.userUrl);
  }

  getOneUserById(id: number): Observable<Utilizatori>{
    return this.http.get<Utilizatori>(this.userUrl + "/" + id);
  }

  getOneUserByDen(denumire: string): Observable<Utilizatori>{
    return this.http.get<Utilizatori>(this.userDenUrl + "/" + denumire);
  }

  updateUser(id: number, user: Utilizatori): Observable<Utilizatori>{
    return this.http.put<Utilizatori>(this.userUrl + "/" + id,user);
  }

  deleteUser(id: number){
    return this.http.delete(this.userUrl + "/" + id);
  }
}
