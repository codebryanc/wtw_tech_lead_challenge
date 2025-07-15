import { Injectable } from '@angular/core';

import { ToolService } from '../../common/tool.service';

@Injectable({
  providedIn: 'root'
})
export class HandlerUtilService {  
    // Constructor
    constructor(private _toolService: ToolService) {
    }

    // [Functions]
    public setparam(param: string, value: string) : string {
        var result: string = '';

        if(this._toolService.isExistsLength(value)) {
            result = `${param}=${value}&`;
        }

        return result;
    }

    public removeTheLastOne(endPoint: string) : string {
        return endPoint.substring(0, endPoint.length - 1);
    }
}