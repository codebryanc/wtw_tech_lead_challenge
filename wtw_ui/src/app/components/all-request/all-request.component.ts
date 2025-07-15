import { Component, OnInit, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { RequestService } from '../../service/remote/request.service';
import { RequestsEntity } from '../../models/requestsEntity';
import { RequestCardComponent } from '../request-card/request-card.component';
import { RequestTypeService } from '../../service/remote/request-type.service';
import { RequestStatusService } from '../../service/remote/request-status.service';
import { RequestStatusEntity } from '../../models/requestStatusEntity';
import { RequestTypeEntity } from '../../models/requestTypeEntity';
import { RequestFilterDto } from '../../models/dtos/request.dto';

@Component({
  selector: 'app-all-request',
  standalone: true,
  imports: [
    CommonModule,
    RequestCardComponent,
    FormsModule
  ],
  templateUrl: './all-request.component.html',
  styleUrl: './all-request.component.scss'
})
export class AllRequestComponent implements OnInit {

  requests: RequestsEntity[] = [];
  filteredRequests: RequestsEntity[] = [];
  loading: boolean = false;
  error: string | null = null;
  
  // Filter properties for UI only
  statusFilter: string = '';
  typeFilter: string = '';
  searchText: string = '';
  
  // Available options for filters
  availableStatuses: RequestStatusEntity[] = [];
  availableTypes: RequestTypeEntity[] = [];

  constructor(
    private _requestService: RequestService,
    private _requestTypeService : RequestTypeService,
    private _requestStatusService : RequestStatusService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.loadAllRequests();
      this.getFilterOptions();
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

  private getFilterOptions(): void {
    this.availableStatuses = this._requestStatusService.getRequestStatus();
    this.availableTypes = this._requestTypeService.getRequestTypes();
  }

  onFilterChange(): void {
    // Check if searchText has minimum 3 characters when present
    const hasValidSearch = !this.searchText || this.searchText.length >= 3;
    
    // If no filters are applied or search text is invalid, show all requests
    if ((!this.statusFilter && !this.typeFilter && !this.searchText) || !hasValidSearch) {
      this.filteredRequests = [...this.requests];
      return;
    }

    // Build filter object for backend
    const filter: RequestFilterDto = {};

    // Map selected values to IDs
    if (this.statusFilter) {
      const selectedStatus = this.availableStatuses.find(s => s.resId === this.statusFilter);
      if (selectedStatus) {
        filter.RequestStatusId = selectedStatus.resId;
      }
    }

    if (this.typeFilter) {
      const selectedType = this.availableTypes.find(t => t.rtyId === this.typeFilter);
      if (selectedType) {
        filter.RequestTypeId = selectedType.rtyId;
      }
    }

    // For search text, use JsonProperty and JsonValue (only if 3+ characters)
    if (this.searchText && this.searchText.length >= 3) {
      filter.JsonProperty = 'data'; // Search in data property
      filter.JsonValue = this.searchText;
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

  clearFilters(): void {
    this.statusFilter = '';
    this.typeFilter = '';
    this.searchText = '';
    this.filteredRequests = [...this.requests];
  }
}
