import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JiraIssue } from 'app/models/Jira/jira-issue';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JiraService {

  constructor(private http: HttpClient) { }

  readonly jiraUrl = environment.baseUrl + "jira/issue/";

  getAllIssues(issues: string): Observable<JiraIssue[]>{
    return this.http.get<JiraIssue[]>(this.jiraUrl + issues);
  }
}
