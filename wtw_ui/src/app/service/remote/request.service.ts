import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { HandlerService } from './base/hadler.service';
import { ResultEntity } from '../../models/resultEntity';
import { RequestsEntity } from '../../models/requestsEntity';
import { RequestFilterDto, RequestCreateDto } from '../../models/dtos/request.dto';
import { environment } from '../../../environment/environment';

@Injectable({
  providedIn: 'root',
})
export class RequestService {

  private requestController: string = 'Request';
  
  constructor(private _handlerService: HandlerService) {}

  public getAllRequests(): Observable<RequestsEntity[]> {
    const url = `${environment.aspNet}${this.requestController}/GetAll`;
    return this._handlerService.baseGETArray(url, 'GetAllRequests');
  }

  public getFilteredRequests(filter: RequestFilterDto): Observable<RequestsEntity[]> {
    const queryParams = new URLSearchParams();

    if (filter.rtyId)
    { 
      queryParams.append('rtyId', filter.rtyId); 
    }
    
    if (filter.resId)
    { 
      queryParams.append('resId', filter.resId); 
    }
    
    if (filter.createdFrom)
    { 
      queryParams.append('createdFrom', filter.createdFrom.toISOString()); 
    }
    
    if (filter.createdTo)
    { 
      queryParams.append('createdTo', filter.createdTo.toISOString()); 
    }
    
    if (filter.data)
    { 
      queryParams.append('data', filter.data); 
    }
    
    const url = `${environment.aspNet}${this.requestController}/GetFiltered?${queryParams.toString()}`;

    return this._handlerService.baseGETArray(url, 'GetFilteredRequests');
  }

  public getRequestById(id: string): Observable<RequestsEntity> {
    
    const url = `${environment.aspNet}${this.requestController}/${id}`;

    return this._handlerService.baseGETObject(url, 'GetRequestById');
  }

  public createRequest(requestDto: RequestCreateDto): Observable<RequestsEntity> {
    
    const url = `${environment.aspNet}${this.requestController}/Create`;

    return this._handlerService.basePOST(requestDto, url, 'CreateRequest');
  }

  public deleteRequest(id: string): Observable<ResultEntity> {
    const url = `${environment.aspNet}${this.requestController}/${id}`;
    
    return this._handlerService.baseDELETEWithoutBody(url, 'DeleteRequest');
  }

  public searchByJsonProperty(propertyName: string, value: string): Observable<RequestsEntity[]> {
    const queryParams = new URLSearchParams();
    
    queryParams.append('propertyName', propertyName);
    queryParams.append('value', value);
    
    const url = `${environment.aspNet}${this.requestController}/SearchByProperty?${queryParams.toString()}`;
    
    return this._handlerService.baseGETArray(url, 'SearchByJsonProperty');
  }
}