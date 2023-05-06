import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { CookieDTO, LoginDTO } from '../models/note.module';
import { ResponseModel } from '../models/tools.module';

@Injectable({
  providedIn: 'root'
})
export class NoteService {

  constructor(public http: HttpClient) { }

  login(loginDTO: LoginDTO): Observable<CookieDTO> {
    return this.http.post<ResponseModel<CookieDTO>>("/api/data/login", loginDTO).pipe(
      map((response: ResponseModel<CookieDTO>) => { return response.data})
    )
  }
}
