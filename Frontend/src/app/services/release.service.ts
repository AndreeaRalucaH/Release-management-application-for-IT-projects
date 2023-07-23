import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Release } from 'app/models/release';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReleaseService {

  constructor(private http: HttpClient) { }

  readonly releaseUrl = environment.baseUrl + "release";

  createRelease(rel: Release): Observable<Release>{
    return this.http.post<Release>(this.releaseUrl, rel);
  }

  getAllReleases(): Observable<Release[]>{
    return this.http.get<Release[]>(this.releaseUrl);
  }

  getOneReleaseById(id: number): Observable<Release>{
    return this.http.get<Release>(this.releaseUrl + "/" + id);
  }

  updateRelease(id: number, rel: Release): Observable<Release>{
    return this.http.put<Release>(this.releaseUrl + "/" + id,rel);
  }

  deleteRelease(id: number){
    return this.http.delete(this.releaseUrl + "/" + id);
  }
}
