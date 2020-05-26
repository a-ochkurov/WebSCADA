import { Injectable, Inject } from '@angular/core';
import { catchError, retry } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { throwError, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
  })
export class LogService {

    private appUrl: string;
    private apiUrl: string;

    private httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json; charset=utf-8'
      })
    };
    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
      this.appUrl = baseUrl;
      this.apiUrl = environment.apiLogs;
     }

    getLogs() {
      return this.http.get<Log[]>(this.appUrl + this.apiUrl).pipe(catchError(this.errorHandler));
    }


    errorHandler(error) {
      let errorMessage = '';
      if (error.error instanceof ErrorEvent) {
        // Get client-side error
        errorMessage = error.error.message;
      } else {
        // Get server-side error
        errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
      }
      console.log(errorMessage);
      return throwError(errorMessage);
    }
}
