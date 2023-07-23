import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BrvDetails } from 'app/models/brv-details';
import { BrvResult } from 'app/models/brv-result';
import { DocumentsDetails } from 'app/models/documents-details';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AddDocumentsService {

  constructor(private http: HttpClient) { }

  readonly docUrl = environment.baseUrl + "documents/docs";
  readonly excelUrl = environment.baseUrl + "documents";

  createBrvDoc(brvDetails: BrvDetails): Observable<BrvResult>{
    return this.http.post<BrvResult>(this.excelUrl, brvDetails);
  }

  uploadTests(file: File, idImpulse: string): Observable<DocumentsDetails>{
    const formData = new FormData();
    formData.append('file',file);

    return this.http.post<DocumentsDetails>(this.docUrl + "/" + idImpulse, formData);
  }
}
