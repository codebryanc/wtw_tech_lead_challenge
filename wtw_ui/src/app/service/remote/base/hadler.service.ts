import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable,  of } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { HandlerUtilService } from './handler-util.service';
import { ToolService } from '../../common/tool.service';
import { LoadingService } from '../../common/loading.service';
import { ResultEntity } from '../../../models/resultEntity';

@Injectable({
  providedIn: 'root'
})
export class HandlerService {
  
  // Constructor
  constructor(private _http: HttpClient,
    private _toolService: ToolService,
    private _loadingService: LoadingService,
    private _handlerUtilService: HandlerUtilService ) {
  }

  // Methods
  public handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // log
      this._toolService.instrumentation(`${operation} failed: ${error.message}`);

      // Error
      if(error.error) {
        this._toolService.instrumentation(" failed:" + JSON.stringify(error.error));
      }

      // toggle Modal loading
      this._loadingService.toggle(false, "handleError");
      
      return of(result as T);
    };
  }
  
  // GET
  public baseGETObject(url: string, method: string) : Observable<any> {

    return this._http.get<any>(url)
    .pipe(
      catchError(this.handleError<any>(method))
    );
  }

  public baseGETArray(url: string, method: string) : Observable<any[]> {

    return this._http.get<any[]>(url)
      .pipe(
        catchError(this.handleError<any[]>(method))
      );
  }

  public baseGETObjectRequest(request: any, url: string, method: string) : Observable<any> {
    if(this._toolService.isExists(request)){

      return this._http.get<any>(url)
        .pipe(
          catchError(this.handleError<any>(method))
        );
    }
    else
      return null;
  }

  public baseGETArrayRequest(request: any, url: string, method: string) : Observable<any[]> {
    if(this._toolService.isExists(request)){

      return this._http.get<any[]>(url)
        .pipe(
          catchError(this.handleError<any[]>(method))
        );
    }
    else
      return null;
  }

  // POST
  public basePOST(request: any, url: string, method: string) : Observable<any>{
    if(this._toolService.isExists(request)){

      let body = JSON.stringify(request);

      return this._http.post<any>(url, body)
        .pipe(
          catchError(this.handleError<any>(method))
        );
    }
    else
      return null;
  }

  // PUT
  public basePUT(request: any, url: string, method: string) : Observable<ResultEntity> {
    if(this._toolService.isExists(request)){
      
      let body = JSON.stringify(request);
      
      return this._http.put<ResultEntity>(url, body)
        .pipe(
          catchError(this.handleError<ResultEntity>(method))
        );
    }
    else
      return null;
  }

  // DELETE
  public baseDELETEWithBody(request: any, url: string, method: string) : Observable<any> {
    if(this._toolService.isExists(request)){
      
      let body = JSON.stringify(request);

      return this._http.delete<any>(url, { body })
        .pipe(
          catchError(this.handleError<any>(method))
        );
    }
    else {
      return null;
    }
  }

  public baseDELETEWithoutBody(url: string, method: string) : Observable<any> {
    return this._http.delete<any>(url)
      .pipe(
        catchError(this.handleError<any>(method))
      );
  }

  // [Functions]
}