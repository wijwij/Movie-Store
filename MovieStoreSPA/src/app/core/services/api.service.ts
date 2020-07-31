import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

// A service is a TypeScript class that has @Injectable decorator, will be used in DI
@Injectable({
  // Can be injected into any module
  providedIn: 'root',
})
export class ApiService {
  // Injecting HttpClient
  constructor(protected http: HttpClient) {}
  // ToDo [question: why use Observable]
  getAll(path: string): Observable<any[]> {
    // don't work with JSON directly, instead working with strongly typed object

    // make a http request, you need to subscribe the response data.
    // Observable can be finite (one response) or infinite (multiple responses)
    return this.http
      .get(`${environment.apiUrl}${path}`)
      .pipe(map((res) => res as any[]));
    // map: select, filter: where
  }
  getOne(path: string, id: number): Observable<any> {
    return this.http
      .get(`${environment.apiUrl}${path}/${id}`)
      .pipe(map((res) => res as any));
  }
  create(path: string, model: any): Observable<any> {
    return this.http
      .post(`${environment.apiUrl}${path}`, model)
      .pipe(map((res) => res as any));
  }
  update() {}
  delete() {}
}
