import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
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
  getAll(endpoint: string): Observable<any[]> {
    // don't work with JSON directly, instead working with strongly typed object

    // make a http request, you need to subscribe the response data.
    // Observable can be finite (one response) or infinite (multiple responses)
    return this.http.get(`${environment.apiUrl}${endpoint}`).pipe(
      map((res) => res as any[]),
      catchError(this.handleError)
    );
    // map: select, filter: where
  }
  getOne(endpoint: string): Observable<any> {
    return this.http.get(`${environment.apiUrl}${endpoint}`).pipe(
      map((res) => res as any),
      catchError(this.handleError)
    );
  }
  create(endpoint: string, model: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}${endpoint}`, model).pipe(
      map((res) => res as any),
      catchError(this.handleError)
    );
  }
  update(endpoint: string, model: any): Observable<any> {
    return this.http
      .put(`${environment.apiUrl}${endpoint}`, model)
      .pipe(catchError((error) => throwError(error.error)));
  }
  delete(endpoint: string, id: number): Observable<void> {
    return this.http.delete(`${environment.apiUrl}${endpoint}/${id}`).pipe(
      map((response) => {
        response;
      }),
      catchError(this.handleError)
    );
  }

  // Any failed http response will be wrapped in the HttpErrorResponse
  private handleError(err: HttpErrorResponse) {
    if (err.error instanceof ErrorEvent) {
      console.log(`An error occured from client side: ${err.error}.`);
    } else {
      console.log(
        `Server side returns ${err.status} code and message "${err.message}".`
      );
    }
    // Invoke Error call back of the subscriber
    return throwError(err.error);
  }
}
