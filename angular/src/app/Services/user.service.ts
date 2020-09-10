import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseUrl } from '../BaseURL';
import { User } from '../Models/user';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  ControllerName: string = 'user/';

  constructor(private http: HttpClient) { }

  GetAll(): Observable<User[]> {
    return this.http.get<User[]>(BaseUrl + this.ControllerName + "GetAll");
  }


  GetById(id: number): Observable<User> {
    return this.http.get<User>(BaseUrl + this.ControllerName + id);
  }


  Create(object: User): Observable<User> {
    return this.http.post<User>(BaseUrl + this.ControllerName, object);
  }


  Delete(id: number): Observable<User> {
    return this.http.delete<User>(BaseUrl + this.ControllerName + id);
  }

  Update(id: number,object: User): Observable<User> {
    return this.http.put<User>(BaseUrl + this.ControllerName + id, object);
  }
}
