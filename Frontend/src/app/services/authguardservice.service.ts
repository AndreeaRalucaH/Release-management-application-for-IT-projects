import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { SocialAuthService, SocialUser } from 'angularx-social-login';
import { Observable, map, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthguardserviceService{

  constructor(private httpClient: HttpClient) {
  }

  // loginWithGoogle(credentials: string): Observable<any> {
  //   const header = new HttpHeaders().set('Content-type', 'application/json');
  //   return this.httpClient.post(this.path + "LoginWithGoogle", JSON.stringify(credentials), { headers: header, withCredentials: true });
  // }
}
