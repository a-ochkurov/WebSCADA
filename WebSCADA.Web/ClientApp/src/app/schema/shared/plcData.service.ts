import { Injectable, Inject } from '@angular/core';
import { catchError, retry } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { throwError, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
  })
export class PlcDataService {

    private appUrl: string;
    private apiUrl: string;

    private httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json; charset=utf-8'
      })
    };
    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
      this.appUrl = baseUrl;
      this.apiUrl = environment.apiModbusData;
     }

    getdata(data: PLCData[]) {
      console.log(JSON.stringify(data));
      return this.http.post<PLCData[]>(this.appUrl + this.apiUrl, data).pipe(catchError(this.errorHandler));
    }

    setdata(data: PLCData) {
      console.log("SET DATA:" + JSON.stringify(data));
      return this.http.put<PLCData>(this.appUrl + this.apiUrl, data).pipe(catchError(this.errorHandler));
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
