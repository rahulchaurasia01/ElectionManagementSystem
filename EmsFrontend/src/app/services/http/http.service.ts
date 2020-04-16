import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private baseUrl : string = environment.BaseUrl;

  constructor(private http: HttpClient) { }


  post(url: string, body) : Observable<any> {
    return this.http.post(this.baseUrl + url, body);
  }

  get(url: string) : Observable<any> {
    return this.http.get(this.baseUrl+url);
  }

  put(url: string, body) : Observable<any> {
    return this.http.put(this.baseUrl+url, body);
  }

  delete(url: string) : Observable<any> {
    return this.http.delete(this.baseUrl+url);
  }


}
