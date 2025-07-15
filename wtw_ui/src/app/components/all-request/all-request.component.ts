import { Component, OnInit, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';

import { RequestService } from '../../service/remote/request.service';
import { RequestsEntity } from '../../models/requestsEntity';
import { RequestCardComponent } from '../request-card/request-card.component';
import { FilterRequestComponent } from '../filter-request/filter-request.component';
import { RequestFilterDto } from '../../models/dtos/request.dto';

@Component({
  selector: 'app-all-request',
  standalone: true,
  imports: [
    CommonModule,
    RequestCardComponent,
    FilterRequestComponent
  ],
  templateUrl: './all-request.component.html',
  styleUrl: './all-request.component.scss'
})
export class AllRequestComponent implements OnInit {

  requests: RequestsEntity[] = [];
  filteredRequests: RequestsEntity[] = [];
  loading: boolean = false;
  error: string | null = null;
  
  constructor(
    private _requestService: RequestService,
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
    
    this._requestService.getAllRequests().subscribe({
      next: (data) => {
        this.requests = data || [];
        this.filteredRequests = [...this.requests];
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Error loading requests: ' + (err.message || 'Unknown error');
        this.loading = false;
        this.requests = [];
        this.filteredRequests = [];
      }
    });
  }

  refreshRequests(): void {
    this.loadAllRequests();
  }

  trackByRequestId(index: number, request: RequestsEntity): string {
    return request.reqId;
  }

  onFiltersChanged(filter: RequestFilterDto): void {
    // If filter is empty, show all requests
    if (!filter || Object.keys(filter).length === 0) {
      this.filteredRequests = [...this.requests];
      return;
    }

    // Call backend filtered requests
    this._requestService.getFilteredRequests(filter).subscribe({
      next: (data) => {
        this.filteredRequests = data || [];
      },
      error: (err) => {
        console.error('Error filtering requests:', err);
        this.filteredRequests = [...this.requests]; // Fallback to all requests
      }
    });
  }

  onFiltersCleared(): void {
    this.filteredRequests = [...this.requests];
  }
}
