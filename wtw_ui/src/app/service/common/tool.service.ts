import { Injectable } from '@angular/core';

import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ToolService {

  private _router: Router = new Router();

  constructor() {}

  // Functions
  isExists(element: any) {
    return element != 'undefined' && element != undefined && element != null ? true : false;
  }

  instrumentation(value: any) {
    console.log(value);
  }

  isExistsLength(object: any) {
    var result = false;

    if(this.isExists(object)) {
      if(JSON.stringify(object) != "{}" && JSON.stringify(object).length > 0) {
        result = true;
      }
    }

    return result;
  }

  isExistsJSON(element: any) {
    let emptyJsonElement = '{}';
    return this.isExists(element) && element != emptyJsonElement;
  }
  
  // Routing
  navigateTo(url: string) : void {
    this._router.navigateByUrl('/' + url);
  }
}