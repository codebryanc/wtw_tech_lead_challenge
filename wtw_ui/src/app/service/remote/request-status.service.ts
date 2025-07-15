import { Injectable } from '@angular/core';

import { RequestStatusEntity } from '../../models/requestStatusEntity';

@Injectable({
  providedIn: 'root',
})
export class RequestStatusService {

  constructor() {}

  getRequestStatus() {
    const requests: RequestStatusEntity[] = [
        { resId: "9B484C2A-3D94-4C40-BC12-E61F3A8B0302", name: "Approved" },
        { resId: "580BB21A-9624-48AE-B21B-34C4FF480551", name: "Pending" },
        { resId: "2307877F-BE56-4756-819E-DDF647AFEE85", name: "Rejected" }
    ];

    return requests;
  }

}