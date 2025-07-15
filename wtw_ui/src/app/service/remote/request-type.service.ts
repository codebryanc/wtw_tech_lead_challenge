import { Injectable } from '@angular/core';

import { RequestTypeEntity } from '../../models/requestTypeEntity';

@Injectable({
  providedIn: 'root',
})
export class RequestTypeService {

  constructor() {}

  getRequestTypes() {
    const requests: RequestTypeEntity[] = [
        { rtyId: "BB1DF24E-51B2-4D41-8FB8-738B64AE27F6", name: "Loan" },
        { rtyId: "F2348456-A1E0-4EF1-B777-4651B392D14D", name: "Permission" },
        { rtyId: "1ED88739-26A8-4F62-842C-5F8019F5B791", name: "Vacation" }
    ];

    return requests;
  }

}