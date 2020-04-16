import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private httpService: HttpService) { }

  login(body : any) {
    return this.httpService.post("Admin/Login", body);
  }

  register(body: any) {
    return this.httpService.post("Admin/Registration", body);
  }


}
