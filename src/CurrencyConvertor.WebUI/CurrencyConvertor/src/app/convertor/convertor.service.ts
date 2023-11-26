import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { ConvertResponse } from '../dto/convert-response.model';
import { ConvertRequest } from '../dto/convert-request.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ConvertorService {

  constructor(private http: HttpClient) { }

  convert(request: ConvertRequest): Observable<ConvertResponse> {
    var url = environment.apiUrl + "convertor/convert/";
    return this.http.post<ConvertResponse>(url, request);
  }
}
