import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EmailDetails } from 'app/models/email-details';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmailService {

  constructor(private http: HttpClient) { }

  readonly emailUrl = environment.baseUrl + "email/createEmail";
  readonly emailStartUrl = environment.baseUrl + "email/start/createEmail";

  postCreateEmail(mail: EmailDetails): Observable<boolean>{
    return this.http.post<boolean>(this.emailUrl, mail);
  }

  postStartEmail(mail: EmailDetails): Observable<boolean>{
    return this.http.post<boolean>(this.emailStartUrl, mail);
  }
}
