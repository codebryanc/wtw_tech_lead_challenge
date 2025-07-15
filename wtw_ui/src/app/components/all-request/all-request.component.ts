import { Component, OnInit, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';

import { RequestService } from '../../service/remote/request.service';
import { RequestsEntity } from '../../models/requestsEntity';
import { RequestCardComponent } from '../request-card/request-card.component';

@Component({
  selector: 'app-all-request',
  standalone: true,
  imports: [
    CommonModule,
    RequestCardComponent
  ],
  templateUrl: './all-request.component.html',
  styleUrl: './all-request.component.scss'
})
export class AllRequestComponent implements OnInit {

  requests: RequestsEntity[] = [];
  loading: boolean = false;
  error: string | null = null;

  constructor(
    private requestService: RequestService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.loadAllRequests();
    }
  }

  loadAllRequests(): void {
    this.loading = true;
    this.error = null;
    
    this.requestService.getAllRequests().subscribe({
      next: (data) => {
        this.requests = data || [];
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Error loading requests: ' + (err.message || 'Unknown error');
        this.loading = false;
        this.requests = [];
      }
    });
  }

  refreshRequests(): void {
    this.loadAllRequests();
  }

  trackByRequestId(index: number, request: RequestsEntity): string {
    return request.reqId;
  }
}
